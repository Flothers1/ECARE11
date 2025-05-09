﻿using CountryData.Standard;
using ECARE.Constants;
using ECARE.Interface;
using ECARE.Models;
using ECARE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
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

        [Authorize(Roles = AuthorizationConstants.LabAdmin) ]
        public async Task<IActionResult> Index()
        {
            var user = await _usersService.GetUserAfterLogin();
            if (user == null)
            {
                // Handle unauthenticated user (e.g., redirect to login)
                return Challenge();
            }

            List<ServiceRequestIndexViewModel> filteredData;


            var serviceRequests = await _context.ServiceRequests
             .AsNoTracking().
             Include(sr => sr.PatientRegistrations)
             .Include(sr => sr.LabBranch)
             .ThenInclude(lb => lb.Lab)
             .OrderByDescending(sr => sr.Date)
             .Select(sr => new ServiceRequestIndexViewModel
             {
                 Service_RequestId = sr.Service_RequestId,
                 Date = sr.Date,
                 PatientName = sr.PatientRegistrations.PatientName,
                 CoPaymentPercentage = sr.CoPaymentPercentage,
                 RequiredTests = sr.RequiredTests,
                 LabId = sr.LabBranch.Lab.Id,
                 Lab = sr.LabBranch.Lab.LabName,
                 LabBranch = sr.LabBranch.LabBranchName,
                 EVoucherPDF = sr.EVoucherPDF,
                 NationalId = sr.PatientRegistrations.NationalID,
                 Invoice = sr.Invoice,
                 Payment = sr.Payment,
                 PatientRegistrationsId = sr.PatientRegistrations.PatientRegistrationsId,
                 Email = sr.PatientRegistrations.Email,
                 PhoneNumber1 = sr.PatientRegistrations.PhoneNumber1,
                 IsDeleted = sr.IsDeleted,
                 IsVerified = sr.IsVerified,
                 LabBranchId = sr.LabBranchId,
                 OTP = sr.OTP,
                 OTPExpiration = sr.OTPExpiration

             }).ToListAsync();
            filteredData =  serviceRequests.Where(sr => sr.LabId == user.LabId && sr.IsDeleted == null).ToList();
            ViewBag.UserLabId = user.LabId;
            return View(filteredData);

        }
        //TODO:
        public IActionResult GetPatientDetails(int id)
        {
            var patient = _context.PatientRegistrations.Include(p => p.CareProgram)
                .Where(p => p.PatientRegistrationsId == id)
                .Select(p => new
                {
                    MobileNumber = p.PhoneNumber1,
                    ProgramName = p.CareProgram.Name,
                    ProgramSponsor = p.CareProgram.SponsorCompany,
                    p.NationalID,
                    p.MembershipNumber,

                })
                .FirstOrDefault();

            if (patient == null)
            {
                return NotFound();
            }

            return Json(patient);
        }
        [HttpGet]
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
            var indications = new List<string>
    {
        "COPD",
        "Ovarian Cancer",
        "Breast Cancer",
        "Prostate Cancer",
        "NA"
    };
            var ageGroups = new List<string>
    {
        "18-36", "37-46", "47-56", "57-66", "67-76", "77-86", "87-95"
    };
            var carePrograms = _context.CarePrograms.ToList();
            ViewBag.AgeGroups = new SelectList(ageGroups);

            ViewBag.Indications = new SelectList(indications);

            ViewBag.CarePorgrams = new SelectList(carePrograms, "Id", "Name");

            ViewBag.Governments = new SelectList(regions);
            ViewBag.TodayDate = DateTime.Today.ToString("yyyy-MM-dd"); 

            return View();
        }

        [HttpPost]
        [Authorize(Roles = AuthorizationConstants.Admin)]
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
            var countryHelper = new CountryHelper();
            var regionsByCountry = countryHelper.GetRegionByCountryCode("EG");

            List<string> regions = new List<string>();
            foreach (var region in regionsByCountry)
            {
                if (region == null) { continue; }

                regions.Add(region.Name);
            }
            var indications = new List<string>
    {
        "COPD",
        "Ovarian Cancer",
        "Breast Cancer",
        "Prostate Cancer",
        "NA"
    };
            var ageGroups = new List<string>
    {
        "18-36", "37-46", "47-56", "57-66", "67-76", "77-86", "87-95"
    };
            var carePrograms = _context.CarePrograms.ToList();
            ViewBag.Governments = new SelectList(regions);

            ViewBag.AgeGroups = new SelectList(ageGroups);

            ViewBag.Indications = new SelectList(indications);

            ViewBag.CarePorgrams = new SelectList(carePrograms, "Id", "Name");

            return View(patient);
        }


        public async Task<IActionResult> Indexadmin()
        {
            var patients = await _context.PatientRegistrations
                            .AsNoTracking()
                           .Include(p => p.CareProgram)
                           .Include(p => p.ServiceRequests)
                           .ThenInclude(s => s.LabBranch)
                           .ThenInclude(l => l.Lab).
                           OrderByDescending(p => p.RegistrationDate)
                           .Select(p => new PatientRegistrationIndexViewModel
                           {
                               RegistrationDate = p.RegistrationDate,
                               PatientName = p.PatientName,
                               NationalID = p.NationalID,
                               CopyOfIDOrPassportPathFront = p.CopyOfIDOrPassportPathFront,
                               CopyOfIDOrPassportPathBack = p.CopyOfIDOrPassportPathBack,
                               Gender = p.Gender,
                               Age = p.Age,
                               AgeGroup = p.AgeGroup,
                               PhoneNumber1 = p.PhoneNumber1,
                               PhoneNumber2 = p.PhoneNumber2,
                               Email = p.Email,
                               Government = p.Government,
                               Territory = p.Territory,
                               CaregiverName = p.CaregiverName,
                               CaregiverPhone = p.CaregiverPhone,
                               ProgramName = p.CareProgram.Name,
                               MembershipNumber = p.MembershipNumber,
                               Indication = p.Indication,
                               Comment = p.Comment,
                               StartDateOfMedication = p.StartDateOfMedication,
                               HealthcareProvider = p.HealthcareProvider,
                               HealthcareProviderAddress = p.HealthcareProviderAddress,
                               HCPGovernment = p.HCPGovernment,
                               HCPTerritory = p.HCPTerritory,
                               Prescription = p.Prescription,
                               TestDocument1 = p.TestDocument1,
                               TestDocument2 = p.TestDocument2,
                           }).ToListAsync();
                           ;
            return View(patients);
        }

        public async Task<IActionResult> ServiceRequests()
        {

            var serviceRequests = await _context.ServiceRequests
               .AsNoTracking().
               Include(sr => sr.PatientRegistrations)
               .Include(sr => sr.LabBranch)
               .ThenInclude(lb => lb.Lab)
               .OrderByDescending(sr => sr.Date)
               .Select(sr => new ServiceRequestIndexViewModel
               {
                   Service_RequestId =sr.Service_RequestId,
                   Date = sr.Date,
                  PatientName = sr.PatientRegistrations.PatientName,
                  CoPaymentPercentage = sr.CoPaymentPercentage,
                  RequiredTests = sr.RequiredTests,
                  Lab = sr.LabBranch.Lab.LabName,
                  LabBranch = sr.LabBranch.LabBranchName,
                  EVoucherPDF = sr.EVoucherPDF,
                  NationalId = sr.PatientRegistrations.NationalID,
                  Invoice = sr.Invoice,
                  Payment = sr.Payment
               }).ToListAsync();
            return View(serviceRequests);
        }




        [HttpGet]
        public IActionResult VerifyOTP(int id)
        {
            return View(id);
        }


        [HttpPost]
        public IActionResult VerifyOTP(int patientId, string otpCode , int serviceRequestId)
        {
            
            var serviceRequest = _context.ServiceRequests
                 .FirstOrDefault(sr => sr.PatientRegistrationsId == patientId && sr.Service_RequestId == serviceRequestId);
            if (serviceRequest == null)
            {
                return RedirectToAction("Index", new { message = "Phone number not registered!" });
            }

            if (serviceRequest.OTP != otpCode)
            {
                ViewBag.PatientId = patientId;
                ViewBag.ServiceRequestid = serviceRequestId;
                ViewBag.Error = "Invalid or expired OTP";
                ViewData["ErrorMessage"] = "Incorrect OTP!";
                return View();
            }

            if (serviceRequest.OTPExpiration < SD.TimeInEgypt)
            {
                ViewData["ErrorMessage"] = "The verification code is incorrect. Please try again.";
                return View(patientId);
            }

            serviceRequest.IsVerified = true;
            _context.SaveChanges();

            return RedirectToAction("Index", new { message = "Verification successful!" });
        }



        [HttpPost]
        public async Task<IActionResult> SendOTP(int patientId, int serviceRequestId)
        {
            string apiUrl = "https://app.community-ads.com/SendSMSAPI/api/SMSSender/SendSMS";

            var patient = await _context.PatientRegistrations
                .FirstOrDefaultAsync(p => p.PatientRegistrationsId == patientId);
            if (patient == null || string.IsNullOrEmpty(patient.PhoneNumber1))
            {
                return RedirectToAction("Index", new { message = "Patient is not registered or does not have a phone number!" });
            }
            var latestRequest = await _context.ServiceRequests
        .FirstOrDefaultAsync(sr => sr.Service_RequestId == serviceRequestId && sr.PatientRegistrationsId == patientId);


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
                latestRequest.OTPExpiration = SD.TimeInEgypt.AddMinutes(20);
                _context.SaveChanges();

                return RedirectToAction("VerifyOTP", new { patientId = patientId, serviceRequestid = serviceRequestId }); 
            }

            return RedirectToAction("Index", new { message = "An error occurred during sending!" });
        }


        [HttpPost]
        public IActionResult SoftDeletePatient(int patientId)
        {
            var serviceRequest = _context.ServiceRequests.FirstOrDefault(p => p.Service_RequestId == patientId);
            if (serviceRequest == null)
            {
                return NotFound();
            }
            if(serviceRequest.IsVerified == true)
            {

                serviceRequest.IsDeleted = true;
                serviceRequest.RequestClosedDate = SD.TimeInEgypt;
                _context.SaveChanges();
            }
            else
            {

                TempData["ErrorMessage"] = "Patient verification is required to complete this request.";
            }


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClosedRequests()
        {
            var user = await _usersService.GetUserAfterLogin();

            List<Service_Request> filteredClosedRequests;

            var closedRequests = _context.ServiceRequests
                                         .AsNoTracking()
                                         .Include(s => s.PatientRegistrations)
                                         .Include(s => s.LabBranch)
                                         .ThenInclude(l => l.Lab)
                                         .Where(s => s.IsDeleted == true)
                                         .OrderByDescending(s => s.Date);

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



        public async Task<IActionResult> SendVerificationCode(int id, int serviceRequestId)
        {
            var patient = await _context.PatientRegistrations
                .FirstOrDefaultAsync(p => p.PatientRegistrationsId == id);

            if (patient == null || string.IsNullOrEmpty(patient.Email))
            {
                return NotFound("Patient not found or email is missing.");
            }

            var latestRequest = await _context.ServiceRequests
                .FirstOrDefaultAsync(sr => sr.Service_RequestId == serviceRequestId && sr.PatientRegistrationsId == id);

            if (latestRequest == null)
            {
                return NotFound("Service request not found.");
            }

            var verificationCode = new Random().Next(100000, 999999).ToString();

            latestRequest.OTP = verificationCode;
            latestRequest.OTPExpiration = SD.TimeInEgypt.AddMinutes(20);
            await _context.SaveChangesAsync();

            var subject = "Your Verification Code";
            var body = $"Your verification code is: <b>{verificationCode}</b>";
            await SendEmailAsync(patient.Email, subject, body);

            return RedirectToAction("VerifyOTP", new { patientId = id, serviceRequestid = serviceRequestId });
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
                    smtpClient.Credentials = new NetworkCredential("ecare@innovaxcess.com", "Ecare@1324");
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
