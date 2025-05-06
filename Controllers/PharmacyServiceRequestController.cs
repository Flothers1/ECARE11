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
            var requests = _context.PharmacyServiceRequests.AsNoTracking()
                .Include(r => r.CareProgram)
                .Include(r => r.PharmacyBranch)
                .ThenInclude(pb => pb.Pharmacy)
                .Include(r => r.PatientRegistrations)
                .OrderByDescending(r => r.Date)
                .Select(psr => new PharmacyServiceRequestIndexViewModel
                {
                    Date = psr.Date,
                    PatientName = psr.PatientRegistrations.PatientName,
                    NationalId = psr.PatientRegistrations.NationalID,
                    PriceAfterDiscount = psr.CareProgram.PriceAfterDiscount,
                    MedicationName = psr.CareProgram.MedicationName,
                    Pharmacy = psr.PharmacyBranch.Pharmacy.Name,
                    PharmacyBranch = psr.PharmacyBranch.Name,
                    EVoucherPDF = psr.EVoucherPDF,
                    SignedEVoucherPDF = psr.SignedEVoucher,
                }).ToList();

            return View(requests);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var pharmacySRViewModel = new PharmacyServiceRequestViewModel();
            pharmacySRViewModel.CareProgramOptions = new SelectList(_context.CarePrograms, "Id", "Name");
            pharmacySRViewModel.PharmaciesOptions = new SelectList(_context.Pharmacies, "Id", "Name");
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
                Id = model.Id,
                PharmacyBranchId = model.PharmacyBranchId,
                
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
            model.CareProgramOptions = new SelectList(_context.CarePrograms, "Id", "Name");
            model.PharmaciesOptions = new SelectList(_context.Pharmacies, "Id", "Name");
            model.PatientRegistrationOptions = new SelectList(_context.PatientRegistrations, "PatientRegistrationsId", "PatientName");
            return View(model);

        }

        [HttpGet]
        public async Task<JsonResult> GetPatients(int CareProgramId)
        {
            var patients = await _context.PatientRegistrations.AsNoTracking()
                .Where(pr => pr.CareProgramId == CareProgramId)
                .OrderBy(pr => pr.PatientName)    // or whatever property holds the name
                .Select(pr => new {
                    id = pr.PatientRegistrationsId,
                    PatientName = pr.PatientName
                })
                .ToListAsync();

            return Json(patients);
        }
        [HttpGet]
        public async Task<JsonResult> GetBranches(int pharmacyId)
        {
            var branches = await _context.pharmacyBranches.AsNoTracking()
                .Where(pb => pb.PharmacyId == pharmacyId)
                .OrderBy(pb => pb.Name)
                .Select(pb => new {
                    id = pb.Id,
                    name = pb.Name
                })
                .ToListAsync();

            return Json(branches);
        }

        public IActionResult GetPatientPharmacyRequests(int patientId)
        {
            var requests = _context.PharmacyServiceRequests.AsNoTracking()
                .Include(r => r.CareProgram)
                .Include(r => r.PharmacyBranch)
                    .ThenInclude(pb => pb.Pharmacy)
                .Include(r => r.PatientRegistrations)
                .Where(r => r.PatientRegistrations.PatientRegistrationsId == patientId)
                .OrderByDescending(r => r.Date)
                .Select(psr => new PharmacyServiceRequestIndexViewModel
                {
                    Date = psr.Date,
                    NationalId = psr.PatientRegistrations.NationalID,
                    Pharmacy = psr.PharmacyBranch.Pharmacy.Name,
                    PharmacyBranch = psr.PharmacyBranch.Name,
                    MedicationName = psr.CareProgram.MedicationName,
                    PriceAfterDiscount = psr.CareProgram.PriceAfterDiscount,
                    EVoucherPDF = psr.EVoucherPDF,
                    SignedEVoucherPDF = psr.SignedEVoucher
                }).ToList();

            return PartialView("_PatientPharmacyRequests", requests);
        }
    }
}
