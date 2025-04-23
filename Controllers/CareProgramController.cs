using ECARE.Models;
using ECARE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECARE.Controllers
{
    public class CareProgramController : Controller
    {
        private readonly ECAREContext _context;

        public CareProgramController(ECAREContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var viewModel = new CareProgramViewModel
            {
                PharmacyOptions = new MultiSelectList(_context.Pharmacies, "Id", "Name"),

                DistributorOptions = new MultiSelectList(_context.Distributors, "Id", "Name")
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CareProgramViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var careProgram = new CareProgram
                {
                    Name = viewModel.Name,
                    StartDate = viewModel.StartDate,
                    ProductManager = viewModel.ProductManager,
                    MedicationName = viewModel.MedicationName,
                    MedicationPackSize = viewModel.MedicationPackSize,
                    MedicationPackConsumptionDuration = viewModel.MedicationPackConsumptionDuration,
                    OriginalPrice = viewModel.OriginalPrice,
                    PriceAfterDiscount = viewModel.PriceAfterDiscount,
                    HCPList = viewModel.HCPList?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>()

                };
                var pharmacies = await _context.Pharmacies.Where(p => viewModel.SelectedPharmacyIds.Contains(p.Id)).ToListAsync();
                // Add relationships
                careProgram.Pharmacies = await _context.Pharmacies
                .Where(p => viewModel.SelectedPharmacyIds.Contains(p.Id))
                    .ToListAsync();

                careProgram.Distributors = await _context.Distributors
                    .Where(d => viewModel.SelectedDistributorIds.Contains(d.Id))
                    .ToListAsync();

                _context.Add(careProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdowns if validation fails
            viewModel.PharmacyOptions = new MultiSelectList(_context.Pharmacies, "Id", "Name");
            viewModel.DistributorOptions  = new MultiSelectList(_context.Distributors, "Id", "Name");

            return View(viewModel);
        }
    }
}
