using ECARE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System;

namespace ECARE.Controllers
{
    public class FormController : Controller
    {
        private readonly HttpClient _httpClient;

        public FormController(HttpClient httpClient)
        {
            this._httpClient = httpClient;
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

            // Build a simple body in Arabic
            var body = $@" Name :{model.Name} 
                        /  PhoneNumber : {model.PhoneNumber}";

            //p     Collect all files into one list
            var attachments = new List<IFormFile>
        {
            model.Prescription,
            model.NationalIDPhotoFront,
            model.NationalIDPhotoBack,
            model.PSATestImage,
            model.AtomicScanningImage
        };

             // Reuse your existing helper, passing the attachments
            await SendEmailAsync(
               // toEmail: "Contact.center2@innovaxcess.com",
                toEmail: "moohamedali571@gmail.com",
                subject: "نموذج التسجيل",
                body,
                attachments
                 );
            return RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
            => View();
        private async Task SendEmailAsync(string toEmail, string subject, string body, List<IFormFile> attachments = null)
        {
            try
            {
                using (var smtpClient = new SmtpClient("webhosting2036.is.cc", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("ifreeze@flothers.com", "Flothers@19");
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    ServicePointManager.ServerCertificateValidationCallback =
                        (s, certificate, chain, sslPolicyErrors) => true;

                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("ifreeze@flothers.com");
                        mailMessage.To.Add(toEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;
                        if (attachments != null)
                        {
                            foreach (var file in attachments)
                            {
                                if (file != null && file.Length > 0)
                                {
                                    //using var ms = new MemoryStream();
                                    //await file.CopyToAsync(ms);
                                    //ms.Position = 0;

                                    //var attachment = new Attachment(ms, file.FileName, file.ContentType);

                                    var stream = file.OpenReadStream();
                                    var attachment = new Attachment(stream, file.FileName);
                                    //mailMessage.Attachments.Add(attachment);
                                    mailMessage.Attachments.Add(attachment);
                                }
                            }
                        }
                        await smtpClient.SendMailAsync(mailMessage);

                    }

                    // Attach each non-null, non-empty IFormFile

                    Console.WriteLine("Email sent successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
      
    }
}
