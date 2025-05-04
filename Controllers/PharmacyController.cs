using Azure;
using ECARE.Constants;
using ECARE.Interface;
using ECARE.Interface.FileStorage;
using ECARE.Models;
using ECARE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace ECARE.Controllers
{
    [Authorize(Roles =AuthorizationConstants.PharmacyAdmin)]
    public class PharmacyController : Controller
    {
        private readonly ECAREContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly HttpClient _httpClient;
        private readonly IUsersService _usersService;

        public PharmacyController(ECAREContext context, IFileStorageService fileStorageService,
            HttpClient httpClient, IUsersService usersService)
        {
            this._context = context;
            this._fileStorageService = fileStorageService;
            this._httpClient = httpClient;
            this._usersService = usersService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _usersService.GetUserAfterLogin();
            if (user == null)
            {
                return Challenge();
            }
            var data =await _context.PharmacyServiceRequests
             .Include(psr => psr.PatientRegistrations)
             .Include(psr => psr.CareProgram)
             .ThenInclude(cp =>cp.Distributors)
             .Include(psr => psr.PharmacyBranch)
             .ThenInclude(pb => pb.Pharmacy)
             .Select(psr => new PharmacyIndexViewModel
      {
          PatientRegistrationsId = psr.PatientRegistrations.PatientRegistrationsId,
          PharmacyServiceRequestId = psr.Id,
          PatientName = psr.PatientRegistrations!.PatientName,
          PhoneNumber1 = psr.PatientRegistrations.PhoneNumber1,
          NationalID = psr.PatientRegistrations.NationalID,
          Email = psr.PatientRegistrations.Email!,
          MedicationName = psr.CareProgram!.MedicationName,
          PriceAfterDiscount = psr.CareProgram.PriceAfterDiscount,
          PharmacyName = psr.PharmacyBranch.Pharmacy.Name,
          BranchName = psr.PharmacyBranch.Name,
          Distributer = psr.CareProgram != null
           ? string.Join(", ", psr.CareProgram.Distributors.Select(d => d.Name))
           : string.Empty,
          Payment = psr.Payment,
          RequestDate = psr.Date,
          IsDeleted = psr.IsDeleted,
          IsVerified = psr.IsVerified,
          OTP = psr.OTP,
          OTPExpiration = psr.OTPExpiration,
          EVoucherPDF = psr.EVoucherPDF,
          SignedEVoucher = psr.SignedEVoucher,
          PharmacyId = psr.PharmacyBranch.PharmacyId
             }).ToListAsync();
            var filteredData = data.Where(psr => psr.PharmacyId == user.PharmacyId && psr.IsDeleted == null).ToList();
            return View(filteredData);
        }
        public async Task<IActionResult> ClosedPharmacyServiceRequests()
        {
            var user = await _usersService.GetUserAfterLogin();


            var closedRequests =await  _context.PharmacyServiceRequests
            .Include(psr => psr.PatientRegistrations)
            .Include(psr => psr.CareProgram)
            .ThenInclude(cp => cp.Distributors)
            .Include(psr => psr.PharmacyBranch)
            .ThenInclude(pb => pb.Pharmacy)
            .Where(s => s.IsDeleted == true)
            .Select(psr => new PharmacyIndexViewModel
            {
                PatientRegistrationsId = psr.PatientRegistrations.PatientRegistrationsId,
                PharmacyServiceRequestId = psr.Id,
                PatientName = psr.PatientRegistrations!.PatientName,
                PhoneNumber1 = psr.PatientRegistrations.PhoneNumber1,
                NationalID = psr.PatientRegistrations.NationalID,
                Email = psr.PatientRegistrations.Email!,
                MedicationName = psr.CareProgram!.MedicationName,
                PriceAfterDiscount = psr.CareProgram.PriceAfterDiscount,
                PharmacyName = psr.PharmacyBranch.Pharmacy.Name,
                Distributer = psr.CareProgram != null
                  ? string.Join(", ", psr.CareProgram.Distributors.Select(d => d.Name))
                  : string.Empty,
                BranchName = psr.PharmacyBranch.Name,
                Payment = psr.Payment,
                RequestDate = psr.Date,
                RequestClosedDate = psr.RequestClosedDate.Value,
                IsDeleted = psr.IsDeleted,
                IsVerified = psr.IsVerified,
                OTP = psr.OTP,
                OTPExpiration = psr.OTPExpiration,
                EVoucherPDF = psr.EVoucherPDF,
                SignedEVoucher = psr.SignedEVoucher,
                PharmacyId = psr.PharmacyBranch.PharmacyId
            }).ToListAsync();
            List<PharmacyIndexViewModel> filteredClosedRequests;

            if ((await _usersService.GetUserRole(user)).Contains(AuthorizationConstants.Admin))
            {
                filteredClosedRequests = closedRequests;
            }
            else if ((await _usersService.GetUserRole(user)).Contains(AuthorizationConstants.PharmacyAdmin))
            {
                filteredClosedRequests = closedRequests.Where(s =>
                                s.PharmacyId == user.PharmacyId).ToList();
            }
            else
            {
                filteredClosedRequests = new List<PharmacyIndexViewModel> { };
            }
            return View(filteredClosedRequests);
        }
        [HttpPost]
        public async Task<IActionResult> UploadSignedEVoucher(int serviceRequestId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return RedirectToAction("Index", "Pharmacy");
            }
            try
            {
                var pharmacyServiceRequest = await _context.PharmacyServiceRequests
                    .FirstOrDefaultAsync(sr => sr.Id == serviceRequestId);
                if (pharmacyServiceRequest == null)
                {
                    return RedirectToAction("Index", "Pharmacy");
                }
                var filePath = await _fileStorageService.SaveFile("PSRSignedEVoucher", file);
                pharmacyServiceRequest.SignedEVoucher = filePath;

                _context.Update(pharmacyServiceRequest);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Pharmacy");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Pharmacy");
            }
        }

        [HttpPost]
        public IActionResult SoftDeletePharmacyServiceRequest(int pharmacyServiceRequestId)
        {
           
            var request = _context.PharmacyServiceRequests
                .FirstOrDefault(p => p.Id == pharmacyServiceRequestId);

            if (request == null) return NotFound();

            if (request.IsVerified == true)
            {
                request.IsDeleted = true;
                request.RequestClosedDate = DateTime.UtcNow.AddHours(2);
                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Patient verification is required to complete this request.";
            }
            return RedirectToAction("Index");
        }


        // Add these new actions to your controller
        [HttpGet]
        public IActionResult VerifyPharmacyOTP(int id)
        {
            return View(id);
        }

        [HttpPost]
        public IActionResult VerifyPharmacyOTP(int patientId, string otpCode, int pharmacyServiceRequestId)
        {
            var pharmacyRequest = _context.PharmacyServiceRequests
                .FirstOrDefault(psr => psr.PRId == patientId && psr.Id == pharmacyServiceRequestId);

            if (pharmacyRequest == null)
            {
                return RedirectToAction("Index", new { message = "Phone number not registered!" });
            }
            if (pharmacyRequest.OTP != otpCode)
            {
                ViewBag.PatientId = patientId;
                ViewBag.PharmacyServiceRequestId = pharmacyServiceRequestId;
                ViewData["ErrorMessage"] = "Incorrect OTP!";
                return View();
            }

            if (pharmacyRequest.OTPExpiration < DateTime.Now)
            {
                ViewData["ErrorMessage"] = "Expired OTP!";
                return View(patientId);
            }

            pharmacyRequest.IsVerified = true;
            _context.SaveChanges();

            return RedirectToAction("Index", new { message = "Verification successful!" });
        }

        [HttpPost]
        public async Task<IActionResult> SendPharmacyOTP(int patientId, int pharmacyServiceRequestId)
        {
            string apiUrl = "https://app.community-ads.com/SendSMSAPI/api/SMSSender/SendSMS";

            var patient = await _context.PatientRegistrations
                .FirstOrDefaultAsync(p => p.PatientRegistrationsId == patientId);

            var pharmacyRequest = await _context.PharmacyServiceRequests
                .FirstOrDefaultAsync(psr => psr.Id == pharmacyServiceRequestId && psr.PRId == patientId);

            if (patient == null || pharmacyRequest == null)
            {
                return RedirectToAction("PharmacyIndex", new { message = "Invalid request!" });
            }

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
                pharmacyRequest.OTP = otpCode;
                pharmacyRequest.OTPExpiration = DateTime.Now.AddMinutes(20);
                await _context.SaveChangesAsync();

                return RedirectToAction("VerifyPharmacyOTP", new
                {
                    patientId = patientId,
                    pharmacyServiceRequestId = pharmacyServiceRequestId
                });
            }

            return RedirectToAction("PharmacyIndex", new { message = "Error sending OTP" });
        }

        private async Task<bool> SendSMS(string phoneNumber, string otpCode)
        {
            //Your existing SMS sending implementation
            try
            {
                string apiUrl = "https://app.community-ads.com/SendSMSAPI/api/SMSSender/SendSMS";
                var requestData = new
                {
                    UserName = "InnovaxcessAPI",
                    Password = "Oh$|&L-bON",
                    SMSText = $"Your OTP Code: {otpCode}",
                    SMSLang = "E",
                    SMSSender = "Innovaxcess",
                    SMSReceiver = phoneNumber,
                    SMSID = Guid.NewGuid().ToString(),
                };

                var content = new StringContent(JsonSerializer.Serialize(requestData),
                    Encoding.UTF8,
                "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IActionResult> SendVerificationCode(int patientId, int pharmacyServiceRequestId)
        {
            var patient = await _context.PatientRegistrations
               .FirstOrDefaultAsync(p => p.PatientRegistrationsId == patientId);

            var pharmacyRequest = await _context.PharmacyServiceRequests
                .FirstOrDefaultAsync(psr => psr.Id == pharmacyServiceRequestId && psr.PRId == patientId);

            if (patient == null || pharmacyRequest == null)
            {
                return NotFound("Patient or Service Request not found.");
            }

            var verificationCode = new Random().Next(100000, 999999).ToString();

            pharmacyRequest.OTP = verificationCode;
            pharmacyRequest.OTPExpiration = DateTime.Now.AddMinutes(20);
            await _context.SaveChangesAsync();

            var subject = "Your Verification Code";
            var body = $"Your verification code is: <b>{verificationCode}</b>";
            await SendEmailAsync(patient.Email, subject, body);

            return RedirectToAction("VerifyPharmacyOTP", new { patientId = patientId, serviceRequestid = pharmacyServiceRequestId });
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
