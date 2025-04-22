using ECARE.Models;
using ECARE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECARE.Controllers
{
    public class PharmacyServiceRequestController : Controller
    {
        private readonly ECAREContext _context;

        public PharmacyServiceRequestController(ECAREContext context)
        {
            this._context = context;
        }
    //    [HttpGet]
    //    public IActionResult Create()
    //    {
    //        var pharmacySRViewModel = new PharmacySericeRequestViewModel();
    //        pharmacySRViewModel.PharmacyOptions = new List<SelectListItem>
    //        {
    //           new SelectListItem { Value = "Pharmacy1", Text = "Pharmacy One" },
    //           new SelectListItem { Value = "Pharmacy2", Text = "Pharmacy Two" },
    //           new SelectListItem { Value = "Pharmacy3", Text = "Pharmacy Three" }
    //        };
    //        pharmacySRViewModel.DistributorOptions = new List<SelectListItem>
    //        {
    //           new SelectListItem { Value = "Distributor1", Text = "Distributor Alpha" },
    //           new SelectListItem { Value = "Distributor2", Text = "Distributor Beta" }
    //        };

    //        return View(pharmacySRViewModel);
    //    }
    //    [HttpPost]
    //    public IActionResult Create(PharmacySericeRequestViewModel model)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            // Repopulate dropdown options if validation fails
    //            model.PharmacyOptions = /* Fetch or rebuild PharmacyOptions */;
    //            model.DistributorOptions = /* Fetch or rebuild DistributorOptions */
    //            return View(model);
    //        }

    //        // Save selected values: model.Pharmacies and model.Distributors
    //        return RedirectToAction("Success");
    //    }

    }
}
