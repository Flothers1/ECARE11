using ECARE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System;
using ECARE.Models;
using ECARE.Interface.FileStorage;
using ECARE.Services.FileStorage;
using System.Text;

namespace ECARE.Controllers
{
    public class FormController : Controller
    {
        private readonly ECAREContext _context;
        private readonly IFileStorageService _fileStorageService;

        public FormController( ECAREContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            this._fileStorageService = fileStorageService;
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("registration")]
        public IActionResult Submit()
=> View(new ContactFormViewModel());

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Submit(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var prescriptionUrl = await _fileStorageService.SaveFile("RegistrationForm", model.Prescription);
            var frontIdUrl = await _fileStorageService.SaveFile("RegistrationForm", model.NationalIDPhotoFront);
         
            string backIdUrl = model.NationalIDPhotoBack ==  null ? "" :  await _fileStorageService.SaveFile("RegistrationForm", model.NationalIDPhotoBack);
            
            var psaTestUrl = await _fileStorageService.SaveFile("RegistrationForm", model.PSATestImage);
            var atomicScanningImage = await _fileStorageService.SaveFile("RegistrationForm", model.AtomicScanningImage);

  

            var registration = new RegistrationForm
            {
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Prescription = prescriptionUrl,
                NationalIDPhotoFront = frontIdUrl,
                NationalIDPhotoBack = backIdUrl,
                PSATestImage = psaTestUrl,
                AtomicScanningImage = atomicScanningImage
            };
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var prescriptionLink = $"{baseUrl}{registration.Prescription}";
            var frontIdLink = $"{baseUrl}{registration.NationalIDPhotoFront}";
            string backIdLink =string.IsNullOrEmpty(registration.NationalIDPhotoBack) ? "" : $"{baseUrl}{registration.NationalIDPhotoBack}";
            var psaTestLink = $"{baseUrl}{registration.PSATestImage}";
            var atomicScanningLink = $"{baseUrl}{registration.AtomicScanningImage}";
            _context.Add(registration);
             await _context.SaveChangesAsync();

            var sb = new StringBuilder();
            sb.AppendLine($"<p><strong>الاسم:</strong> {registration.Name}</p>");
            sb.AppendLine($"<p><strong>رقم الموبيل:</strong> {registration.PhoneNumber}</p>");

            // Single‑file fields:
            sb.AppendLine($"<p><strong>الروشتة:</strong> <a href=\"{prescriptionLink}\">عرض</a></p>");
            sb.AppendLine($"<p><strong>الوجه الأمامي للبطاقة:</strong>  <a href=\"{frontIdLink}\">عرض</a></p>");
            if (!string.IsNullOrEmpty(backIdLink)){
                sb.AppendLine($"<p><strong>الوجه الخلفي للبطاقة:</strong>   <a href=\"{backIdLink}\">عرض</a></p>");
            }
            sb.AppendLine($"<p><strong>صورة تحليل PSA:</strong> <a href=\"{psaTestLink}\">عرض</a></p>");
            sb.AppendLine($"<p><strong>صورة المسح الذري:</strong> <a href=\"{atomicScanningLink}\">عرض</a></p>");

      
            // Reuse your existing helper, passing the attachments
            await SendEmailAsync(
                toEmail: "Contact.center2@innovaxcess.com",
                //toEmail: "moohamedali571@gmail.com",
                subject: "نموذج التسجيل",
                body:sb.ToString()
                 );
            return RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
            => View();
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
