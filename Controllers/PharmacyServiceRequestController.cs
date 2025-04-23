using ECARE.Interface.FileStorage;
using ECARE.Models;
using ECARE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECARE.Controllers
{
    public class PharmacyServiceRequestController : Controller
    {
        private readonly ECAREContext _context;
        private readonly IFileStorageService _fileStorageService;

        public PharmacyServiceRequestController(ECAREContext context, IFileStorageService fileStorageService)
        {
            this._context = context;
            this._fileStorageService = fileStorageService;
        }
        public IActionResult Index()
        {
            var requests = _context.PharmacyServiceRequests
                .Include(r => r.CareProgram)
                .Include(r => r.PatientRegistrations)
                .Select(r => new PharmacyServiceRequestViewModel
                {
                    Id = r.Id,
                    Date = r.Date,
                    Payment = r.Payment,
                    IsVerified = r.IsVerified,
                }).ToList();

            return View(requests);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var pharmacySRViewModel = new PharmacyServiceRequestViewModel();
            pharmacySRViewModel.CareProgramOptions = new SelectList(_context.CarePrograms, "Id", "Name");
            pharmacySRViewModel.PatientRegistrationOptions = new SelectList(_context.PatientRegistrations, "PatientRegistrationsId", "PatientName");

            return View(pharmacySRViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PharmacyServiceRequestViewModel model)
        {
            if (ModelState.IsValid)
            {

            var pharmacyServiceRequest = new PharmacyServiceRequest
            {
                OTP = model.OTP,
                CareProgramId = model.SelectedCareProgramId,
                IsDeleted = model.IsDeleted,
                IsVerified = model.IsVerified,
                OTPExpiration= model.OTPExpiration,
                PRId = model.PatientRegistrationsId,
                Payment = model.Payment,
                Date = model.Date,
                Id = model.Id            
            };
            if (model.EVoucherPDF != null)
            {
                var fileUrl = await _fileStorageService.SaveFile("pharmacyUploads", model.EVoucherPDF);
                pharmacyServiceRequest.EVoucherPDF = fileUrl;
            }
            else
            {
                ModelState.AddModelError("", "Please upload the e voucher ");
                return View(model);
            }
            _context.PharmacyServiceRequests.Add(pharmacyServiceRequest);
            await _context.SaveChangesAsync();

            model.CareProgramOptions = new SelectList(_context.CarePrograms, "Id", "Name", pharmacyServiceRequest.CareProgramId);
            model.PatientRegistrationOptions = new SelectList(_context.PatientRegistrations, "PatientRegistrationsId", "PatientName", pharmacyServiceRequest.PRId);

                //notification 

                return RedirectToAction("Indexadmin", "PatientRegistrations");
            }
            return View(model);

        }

        [HttpGet]
        public JsonResult GetPatients(int careProgramId)
        {
            var Patients = _context.PatientRegistrations
                .Where(b => b.CareProgramId == careProgramId)
                .Select(b => new { b.PatientRegistrationsId, b.PatientName })
                .ToList();

            return Json(Patients);
        }
    }
}
