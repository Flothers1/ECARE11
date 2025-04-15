using CountryData.Standard;
using ECARE.Constants;
using ECARE.Interface;
using ECARE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace ECARE.Controllers
{
    [Authorize]
    public class PatientRegistrationsController : Controller
    {
        private readonly ECAREContext _context;
        private readonly HttpClient _httpClient;
        private readonly IUsersService _usersService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PatientRegistrationsController(ECAREContext context, HttpClient httpClient, IUsersService usersService, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _httpClient = httpClient;
            _usersService = usersService;
            _hostingEnvironment = hostingEnvironment;
        }


        //public async Task<IActionResult> Index()
        //{
        //    var user = await _usersService.GetUserAfterLogin();
        //    List<PatientRegistrations> patients;

        //    if ((await _usersService.GetUserRole(user)).Contains("Admin"))
        //    {
        //        patients = await _context.PatientRegistrations.ToListAsync();
        //    }
        //    else
        //    {
        //        patients = await _context.PatientRegistrations
        //            .Include(p => p.ServiceRequests)
        //            .Where(p => p.IsDeleted == null &&
        //                        p.ServiceRequests.Any(l => l.LabBranchId == user.LabBranchId))
        //            .ToListAsync();
        //    }

        //    return View(patients);
        //}
        [Authorize(Roles = AuthorizationConstants.LabAdmin) ]
        public async Task<IActionResult> Index()
        {
            var user = await _usersService.GetUserAfterLogin();

            List<PatientRegistrations> filteredPatients;

            var patients = _context.PatientRegistrations
                            .Include(p => p.ServiceRequests)
                            .ThenInclude(s => s.LabBranch)
                            .ThenInclude(l => l.Lab);


            if ((await _usersService.GetUserRole(user)).Contains("Admin"))
            {
                filteredPatients = await patients.ToListAsync();
            }
            else if((await _usersService.GetUserRole(user)).Contains("LabAdmin"))
            {
                filteredPatients = await patients.Where(p => p.ServiceRequests.Any(sr => sr.LabBranch.LabId == user.LabId)).ToListAsync();
            }
            else
            {
                filteredPatients = await patients.Where(p => 
                                p.ServiceRequests.Any(l => l.LabBranchId == user.LabBranchId)).ToListAsync();
            }

            ViewBag.UserLabId = user.LabId;
            return View(filteredPatients);

        }

        public IActionResult GetPatientDetails(int id)
        {
            var patient = _context.PatientRegistrations
                .Where(p => p.PatientRegistrationsId == id)
                .Select(p => new
                {
                    MobileNumber = p.PhoneNumber1,
                    ProgramName = p.ProgramName,
                    ProgramSponsor = p.ProgramSponsor,
                    NationalID = p.NationalID,
                    MembershipNumber = p.MembershipNumber
                })
                .FirstOrDefault();

            if (patient == null)
            {
                return NotFound();
            }

            return Json(patient);
        }

        public IActionResult Create()
        {
            
    var countryHelper = new CountryHelper();
            var regionsByCountry = countryHelper.GetRegionByCountryCode("EG");

            List<string> regions = new List<string>();
            foreach (var region in regionsByCountry)
            {
                if(region == null) { continue; }

                regions.Add(region.Name);
            }
            var medications = new List<string> { "Erleada", "NA"};
            var indications = new List<string>
    {
        "COPD",
        "Ovarian Cancer",
        "Breast Cancer",
        "Prostate Cancer",
        "NA"
    };
            var sponsors = new List<string>
    {
        "Janssen", "AstraZeneca", "BMS", "EVA", "Pfizer", "Sanofi",
        "Roche", "Novartis", "GSK", "Buyer", "Takeda",
        "Boehringer Ingelheim", "Apex Pharma", "GYPTO Pharma",
        "Astellas", "Parkville", "Orchidia"
    };
            var ageGroups = new List<string>
    {
        "18-36", "37-46", "47-56", "57-66", "67-76", "77-86", "87-95"
    };
            ViewBag.AgeGroups = new SelectList(ageGroups);

            ViewBag.Sponsors = new SelectList(sponsors);

            ViewBag.Indications = new SelectList(indications);

            ViewBag.Medications = new SelectList(medications, "Erleada");

            ViewBag.Governorates = new SelectList(regions);
            ViewBag.TodayDate = DateTime.Today.ToString("yyyy-MM-dd"); 

            return View();
        }

        [HttpPost]
        //[Authorize(Roles = AuthorizationConstants.Admin)]
        public async Task<IActionResult> Create(PatientRegistrations patient)
        {
           
            ModelState.Remove("TestDocument1");
            ModelState.Remove("TestDocument2");
            ModelState.Remove("PrescriptionPath");
            ModelState.Remove("CopyOfIDOrPassportPathBack");
            ModelState.Remove("CopyOfIDOrPassportPathFront");


            if (ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (patient.CopyOfIDOrPassportFileFront != null && patient.CopyOfIDOrPassportFileFront.Length > 0)
                {
                    string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(patient.CopyOfIDOrPassportFileFront.FileName)}";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await patient.CopyOfIDOrPassportFileFront.CopyToAsync(fileStream);
                    }

                    patient.CopyOfIDOrPassportPathFront = $"/uploads/{uniqueFileName}";
                }
                if (patient.CopyOfIDOrPassportFileBack != null && patient.CopyOfIDOrPassportFileBack.Length > 0)
                {
                    string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(patient.CopyOfIDOrPassportFileBack.FileName)}";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await patient.CopyOfIDOrPassportFileBack.CopyToAsync(fileStream);
                    }

                    patient.CopyOfIDOrPassportPathBack = $"/uploads/{uniqueFileName}";
                }

                if (patient.TestDocument1File != null && patient.TestDocument1File.Length > 0)
                {
                    string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(patient.TestDocument1File.FileName)}";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await patient.TestDocument1File.CopyToAsync(fileStream);
                    }

                    patient.TestDocument1 = $"/uploads/{uniqueFileName}";
                }

                if (patient.TestDocument2File != null && patient.TestDocument2File.Length > 0)
                {
                    string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(patient.TestDocument2File.FileName)}";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await patient.TestDocument2File.CopyToAsync(fileStream);
                    }

                    patient.TestDocument2 = $"/uploads/{uniqueFileName}";
                }

                if (patient.PrescriptionFile != null && patient.PrescriptionFile.Length > 0)
                {
                    string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(patient.PrescriptionFile.FileName)}";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await patient.PrescriptionFile.CopyToAsync(fileStream);
                    }

                    patient.Prescription = $"/uploads/{uniqueFileName}";
                }

                _context.Add(patient);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Indexadmin));
            }

            return View(patient);
        }


        public async Task<IActionResult> Indexadmin()
        {
            var patients = await _context.PatientRegistrations.Include(p => p.ServiceRequests)
                           .ThenInclude(s => s.LabBranch)
                           .ThenInclude(l => l.Lab).ToListAsync();
            return View(patients);
        }

        public IActionResult ServiceRequests()
        {
            var patientRegistrations = _context.PatientRegistrations.Include(p => p.ServiceRequests)
                                                                     .ThenInclude(sr => sr.LabBranch)
                                                                     .ThenInclude(lb => lb.Lab)
                                                                     .ToList();
            return View(patientRegistrations);
        }




        [HttpGet]
        public IActionResult VerifyOTP(int id)
        {
            return View(id);
        }


        [HttpPost]
        public IActionResult VerifyOTP(int patientId, string otpCode , int serviceRequestid)
        {
            
            var serviceRequest = _context.ServiceRequests
                 .FirstOrDefault(sr => sr.PatientRegistrationsId == patientId && sr.Service_RequestId == serviceRequestid);
            if (serviceRequest == null)
            {
                return RedirectToAction("Index", new { message = "Phone number not registered!" });
            }

            if (serviceRequest.OTP != otpCode)
            {
                ViewBag.PatientId = patientId;
                ViewBag.ServiceRequestid = serviceRequestid;
                ViewBag.Error = "Invalid or expired OTP";
                ViewData["ErrorMessage"] = "Incorrect OTP!";
                return View();
            }

            if (serviceRequest.OTPExpiration < DateTime.Now)
            {
                ViewData["ErrorMessage"] = "The verification code is incorrect. Please try again.";
                return View(patientId);
            }

            serviceRequest.IsVerified = true;
            _context.SaveChanges();

            return RedirectToAction("Index", new { message = "Verification successful!" });
        }



        [HttpPost]
        public async Task<IActionResult> SendOTP(int patientId, int serviseRequstid)
        {
            string apiUrl = "https://app.community-ads.com/SendSMSAPI/api/SMSSender/SendSMS";

            var patient = await _context.PatientRegistrations
                .FirstOrDefaultAsync(p => p.PatientRegistrationsId == patientId);
            if (patient == null || string.IsNullOrEmpty(patient.PhoneNumber1))
            {
                return RedirectToAction("Index", new { message = "Patient is not registered or does not have a phone number!" });
            }
            var latestRequest = await _context.ServiceRequests
        .FirstOrDefaultAsync(sr => sr.Service_RequestId == serviseRequstid && sr.PatientRegistrationsId == patientId);


            var otpCode = new Random().Next(100000, 999999).ToString();
            var requestData = new
            {
                UserName = "InnovaxcessAPI",
                Password = "Oh$|&L-bON",
                SMSText = $"Your OTP Code: {otpCode}",
                SMSLang = "E",
                SMSSender = "Innovaxcess",
                SMSReceiver = patient.PhoneNumber1,
                SMSID = Guid.NewGuid().ToString(),
            };

            string jsonContent = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
           

            if (response.IsSuccessStatusCode)
            {
                latestRequest.OTP = otpCode;
                latestRequest.OTPExpiration = DateTime.Now.AddMinutes(20);
                _context.SaveChanges();

                return RedirectToAction("VerifyOTP", new { patientId = patientId, serviceRequestid = serviseRequstid }); 
            }

            return RedirectToAction("Index", new { message = "An error occurred during sending!" });
        }


        [HttpPost]
        public IActionResult SoftDeletePatient(int patientId)
        {
            var patient = _context.ServiceRequests.FirstOrDefault(p => p.Service_RequestId == patientId);
            if (patient == null)
            {
                return NotFound();
            }

            patient.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClosedRequests()
        {
            var user = await _usersService.GetUserAfterLogin();

            List<Service_Request> filteredClosedRequests;

            var closedRequests = _context.ServiceRequests
                                         .Include(s => s.PatientRegistrations)
                                         .Include(s => s.LabBranch)
                                         .ThenInclude(l => l.Lab)
                                         .Where(s => s.IsDeleted == true);

            if ((await _usersService.GetUserRole(user)).Contains("Admin"))
            {
                filteredClosedRequests = await closedRequests.ToListAsync();
            }
            else if ((await _usersService.GetUserRole(user)).Contains("LabAdmin"))
            {
                filteredClosedRequests = await closedRequests.Where(s =>
                                s.LabBranch.Lab.Id == user.LabId).ToListAsync();
            }
            else
            {
                filteredClosedRequests = await closedRequests.Where(s =>
                                s.LabBranchId == user.LabBranchId).ToListAsync();
            }

            return View(filteredClosedRequests);
        }



        public async Task<IActionResult> SendVerificationCode(int id, int serviceRequestid)
        {
            var patient = await _context.PatientRegistrations
                .FirstOrDefaultAsync(p => p.PatientRegistrationsId == id);

            if (patient == null || string.IsNullOrEmpty(patient.Email))
            {
                return NotFound("Patient not found or email is missing.");
            }

            var latestRequest = await _context.ServiceRequests
                .FirstOrDefaultAsync(sr => sr.Service_RequestId == serviceRequestid && sr.PatientRegistrationsId == id);

            if (latestRequest == null)
            {
                return NotFound("Service request not found.");
            }

            var verificationCode = new Random().Next(100000, 999999).ToString();

            latestRequest.OTP = verificationCode;
            latestRequest.OTPExpiration = DateTime.Now.AddMinutes(20);
            await _context.SaveChangesAsync();

            var subject = "Your Verification Code";
            var body = $"Your verification code is: <b>{verificationCode}</b>";
            await SendEmailAsync(patient.Email, subject, body);

            return RedirectToAction("VerifyOTP", new { patientId = id, serviceRequestid = serviceRequestid });
        }

        [HttpPost]
        public IActionResult TogglePayment(int serviceRequestId)
        {
            var serviceRequest = _context.ServiceRequests.Find(serviceRequestId);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            // Toggle the Payment value
            serviceRequest.Payment = !serviceRequest.Payment;
            _context.SaveChanges();

            return Ok();
        }
        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient("Mail.innovaxcess.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("ecare@innovaxcess.com", "Ecare@132@324");
                    smtpClient.EnableSsl = true; 
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    ServicePointManager.ServerCertificateValidationCallback =
                        (s, certificate, chain, sslPolicyErrors) => true;

                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("ecare@innovaxcess.com");
                        mailMessage.To.Add(toEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;

                        await smtpClient.SendMailAsync(mailMessage);
                        Console.WriteLine("Email sent successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }


    }
}
