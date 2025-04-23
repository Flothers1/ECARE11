using ECARE.Interface.FileStorage;
using ECARE.Models;
using ECARE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ECARE.Controllers
{
    public class PharmacyController : Controller
    {
        private readonly ECAREContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly HttpClient _httpClient;

        public PharmacyController(ECAREContext context, IFileStorageService fileStorageService, HttpClient httpClient)
        {
            this._context = context;
            this._fileStorageService = fileStorageService;
            this._httpClient = httpClient;
        }
        public IActionResult Index()
        {
            var data = _context.PharmacyServiceRequests
             .Include(psr => psr.PatientRegistrations)
             .Include(psr => psr.CareProgram)
                 .ThenInclude(cp => cp.Pharmacies)
             .Include(psr => psr.PharmacyBranch)
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
          PharmacyName = psr.CareProgram.Pharmacies.Where(p => p.Id == psr.PharmacyBranch.PharmacyId).FirstOrDefault()!.Name,
          BranchName = psr.PharmacyBranch.Name,
          Payment = psr.Payment,
          RequestDate = psr.Date,
          IsDeleted = psr.IsDeleted,
          IsVerified = psr.IsVerified,
          OTP = psr.OTP,
          OTPExpiration = psr.OTPExpiration,
          EVoucherPDF = psr.EVoucherPDF,
          }).ToList();

            return View(data);
        }
        [HttpPost]
        public IActionResult SoftDeletePatient(int serviceRequestId)
        {
            var request = _context.PharmacyServiceRequests
                .FirstOrDefault(p => p.Id == serviceRequestId);

            if (request == null) return NotFound();

            request.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // PharmacyController.cs
        [HttpGet]
        public IActionResult VerifyOTP(int pharmacyServiceRequestId)
        {
            return View(pharmacyServiceRequestId);
        }

        [HttpPost]
        public IActionResult VerifyOTP(int pharmacyServiceRequestId, string otpCode)
        {
            var request = _context.PharmacyServiceRequests
                .Include(psr => psr.PatientRegistrations)
                .FirstOrDefault(psr => psr.Id == pharmacyServiceRequestId);

            if (request == null)
            {
                return RedirectToAction("Index", new { message = "Invalid request!" });
            }

            if (request.OTP != otpCode)
            {
                ViewBag.Error = "Invalid or expired OTP";
                ViewData["ErrorMessage"] = "Incorrect OTP!";
                return View(pharmacyServiceRequestId);
            }

            if (request.OTPExpiration < DateTime.Now)
            {
                ViewData["ErrorMessage"] = "The verification code has expired. Please request a new one.";
                return View(pharmacyServiceRequestId);
            }

            request.IsVerified = true;
            _context.SaveChanges();

            return RedirectToAction("Index", new { message = "Verification successful!" });
        }

        [HttpPost]
        public async Task<IActionResult> SendOTP(int pharmacyServiceRequestId)
        {
            var request = await _context.PharmacyServiceRequests
                .Include(psr => psr.PatientRegistrations)
                .FirstOrDefaultAsync(psr => psr.Id == pharmacyServiceRequestId);

            if (request?.PatientRegistrations == null || string.IsNullOrEmpty(request.PatientRegistrations.PhoneNumber1))
            {
                return RedirectToAction("Index", new { message = "Invalid patient or missing phone number!" });
            }

            var otpCode = new Random().Next(100000, 999999).ToString();

            // SMS sending logic (keep your existing implementation)
            var sendResult = await SendSMS(
                request.PatientRegistrations.PhoneNumber1,
                otpCode
            );

            if (sendResult)
            {
                request.OTP = otpCode;
                request.OTPExpiration = DateTime.Now.AddMinutes(20);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("VerifyOTP", new { pharmacyServiceRequestId = pharmacyServiceRequestId });
        }

        private async Task<bool> SendSMS(string phoneNumber, string otpCode)
        {
            // Your existing SMS sending implementation
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
    }
}
