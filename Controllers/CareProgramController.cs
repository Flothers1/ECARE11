using ECARE.Constants;
using ECARE.Models;
using ECARE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECARE.Controllers
{
    [Authorize(Roles = AuthorizationConstants.Admin)]
    public class CareProgramController : Controller
    {
        private readonly ECAREContext _context;

        public CareProgramController(ECAREContext context)
        {
            this._context = context;
        }
        //TODO: ADD DropDown list for Sponsors
        public async Task<IActionResult> Index()
        {
            var carePrograms = await _context.CarePrograms.Include(cp => cp.Pharmacies)
                .Include(cp => cp.Distributors)
                .ToListAsync();

            var pharmacyList = await _context.Pharmacies
                .ToListAsync();


            var distributorList = await _context.Distributors
                .ToListAsync();
            var vm = carePrograms.Select( p => new CareProgramViewModel
            {
                Id = p.Id,
                Name = p.Name,
                StartDate = p.StartDate,
                ProductManager = p.ProductManager,
                SponsorCompany = p.SponsorCompany,
                MedicationName = p.MedicationName,
                MedicationPackSize = p.MedicationPackSize,
                MedicationPackConsumptionDuration = p.MedicationPackConsumptionDuration,

                OriginalPrice = p.OriginalPrice,
                PriceAfterDiscount = p.PriceAfterDiscount,

                // Flatten HCPList into a CSV string
                HCPList = string.Join(", ", p.HCPList),

                // Pass the IDs of the related entities so the UI can mark them as selected
                SelectedPharmacyIds = p.Pharmacies.Select(ph => ph.Id).ToList(),
                SelectedDistributorIds = p.Distributors.Select(d => d.Id).ToList(),

                // Provide the full set of options, with the current ones pre-selected
                PharmacyOptions = new MultiSelectList(pharmacyList, "Id", "Name", p.Pharmacies.Select(ph => ph.Id)),
                DistributorOptions = new MultiSelectList(distributorList, "Id", "Name", p.Distributors.Select(d => d.Id))
            }).ToList();
            return View(vm);
        }

        public IActionResult Create()
        {
            var medications = new List<string> { "Erleada", "NA" };
            var sponsors = new List<string>
            {
                "Janssen", "AstraZeneca", "BMS", "EVA", "Pfizer", "Sanofi",
                "Roche", "Novartis", "GSK", "Buyer", "Takeda",
                "Boehringer Ingelheim", "Apex Pharma", "GYPTO Pharma",
                "Astellas", "Parkville", "Orchidia"
            };
            var viewModel = new CareProgramViewModel
            {
                PharmacyOptions = new MultiSelectList(_context.Pharmacies, "Id", "Name"),

                DistributorOptions = new MultiSelectList(_context.Distributors, "Id", "Name"),
                MedicationOptions = new MultiSelectList( medications ),
                SponsorOptions = sponsors.Select(s => new SelectListItem
                {
                    Value = s,
                    Text = s 
                }).ToList()
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
                    SponsorCompany = viewModel.SponsorCompany,
                    MedicationName = viewModel.MedicationName,
                    MedicationPackSize = viewModel.MedicationPackSize,
                    MedicationPackConsumptionDuration = viewModel.MedicationPackConsumptionDuration,
                    OriginalPrice = viewModel.OriginalPrice,
                    PriceAfterDiscount = viewModel.PriceAfterDiscount,
                    HCPList = viewModel.HCPList?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>()

                };
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
            var sponsors = new List<string>
            {
                "Janssen", "AstraZeneca", "BMS", "EVA", "Pfizer", "Sanofi",
                "Roche", "Novartis", "GSK", "Buyer", "Takeda",
                "Boehringer Ingelheim", "Apex Pharma", "GYPTO Pharma",
                "Astellas", "Parkville", "Orchidia"
            };
            // Repopulate dropdowns if validation fails
            viewModel.PharmacyOptions = new MultiSelectList(_context.Pharmacies, "Id", "Name");
            viewModel.DistributorOptions  = new MultiSelectList(_context.Distributors, "Id", "Name");
            viewModel.MedicationOptions = new MultiSelectList(new List<string> { "Erleada", "NA" });
            viewModel.SponsorOptions = sponsors.Select(s => new SelectListItem
            {
                Value = s,
                Text = s
            }).ToList();

            return View(viewModel);
        }
    }
}
