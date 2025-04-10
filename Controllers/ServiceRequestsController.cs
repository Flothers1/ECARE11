using ECARE.Interface.FileStorage;
using ECARE.Models;
using ECARE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace ECARE.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly ECAREContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFileStorageService fileStorageService;

        public ServiceRequestsController(ECAREContext context , IWebHostEnvironment hostingEnvironment, IFileStorageService fileStorageService)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            this.fileStorageService = fileStorageService;
        }


        [HttpGet]
        public JsonResult GetLabBranches(int labId)
        {
            var branches = _context.LabBranch
                .Where(b => b.LabId == labId)
                .Select(b => new { b.Id, b.LabBranchName })
                .ToList();

            return Json(branches);
        }


        // GET: ServiceRequest/Create
        public async Task<IActionResult> Create()
        {
            var serviceRequestViewModel = new ServiceRequestViewModel();
            ViewBag.Labs = new SelectList(await _context.Lab.ToListAsync(), "Id", "LabName");
            ViewBag.Patients = new SelectList(await _context.PatientRegistrations.ToListAsync(), "PatientRegistrationsId", "PatientName");
            return View(serviceRequestViewModel);
        }

        // POST: ServiceRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequestViewModel serviceRequestViewModel)
        {
            Service_Request service_Request = new Service_Request();
            ViewBag.Labs = new SelectList(await _context.Lab.ToListAsync(), "Id", "LabName");
            //ViewBag.Patients = new SelectList(await _context.PatientRegistrations.ToListAsync(), "PatientRegistrationsId", "PatientName");
            ViewBag.Patients = new SelectList(await _context.PatientRegistrations.ToListAsync(), "PatientRegistrationsId", "PatientName", service_Request.PatientRegistrationsId);

            if (ModelState.IsValid)
            {
                var fileUrl =  await fileStorageService.SaveFile("uploads", serviceRequestViewModel.EVoucherPDFFile);
                 service_Request = new Service_Request
                {
                   OTP = serviceRequestViewModel.OTP,
                   OwnerType = serviceRequestViewModel.OwnerType,
                   CoPaymentPercentage = serviceRequestViewModel.CoPaymentPercentage,
                   Date = serviceRequestViewModel.Date,
                   EVoucherPDF = fileUrl,
                   IsDeleted = serviceRequestViewModel.IsDeleted,
                   IsVerified = serviceRequestViewModel.IsVerified,
                   LabBranch = serviceRequestViewModel.LabBranch,
                   LabBranchId = serviceRequestViewModel.LabBranchId,
                   OTPExpiration = serviceRequestViewModel.OTPExpiration,
                   PatientRegistrationsId = serviceRequestViewModel.PatientRegistrationsId,
                   Payment =serviceRequestViewModel.Payment,
                   RequiredTests = serviceRequestViewModel.RequiredTests,
                   Service_RequestId = serviceRequestViewModel.Service_RequestId
                };
                _context.ServiceRequests.Add(service_Request);
                await _context.SaveChangesAsync();
                return RedirectToAction("Indexadmin", "PatientRegistrations");
            }

            return View(serviceRequestViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> UploadInvoice(int serviceRequestId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return RedirectToAction("ServiceRequests", "PatientRegistrations");
            }
            try
            {
                var serviceRequest = await _context.ServiceRequests
                    .FirstOrDefaultAsync(sr => sr.Service_RequestId == serviceRequestId);
                if (serviceRequest == null)
                {
                    return RedirectToAction("ServiceRequests", "PatientRegistrations");
                }
                var filePath = await fileStorageService.SaveFile("invoices", file);
                serviceRequest.Invoice = filePath;

                _context.Update(serviceRequest);
                await _context.SaveChangesAsync();

                return RedirectToAction("ServiceRequests", "PatientRegistrations");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ServiceRequests", "PatientRegistrations");
            }
        }
        // POST: ServiceRequest/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Service_Request serviceRequest)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.ServiceRequests.Add(serviceRequest);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Indexadmin", "PatientRegistrations");
        //    }

        //    ViewBag.Patients = new SelectList(await _context.PatientRegistrations.ToListAsync(), "PatientRegistrationsId", "PatientName", serviceRequest.PatientRegistrationsId);
        //    return View(serviceRequest);
        //}


    }
}
