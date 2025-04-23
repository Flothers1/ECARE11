using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ECARE.ViewModel
{
    public class CareProgramViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Product manager is required")]
        [StringLength(100, ErrorMessage = "Product manager cannot exceed 100 characters")]
        [Display(Name = "Product Manager")]
        public string ProductManager { get; set; }


        [Required(ErrorMessage = "Medication name is required")]
        [StringLength(255, ErrorMessage = "Medication name cannot exceed 255 characters")]
        [Display(Name = "Medication Name")]
        public string MedicationName { get; set; }

        [StringLength(255, ErrorMessage = "Pack size cannot exceed 255 characters")]
        [Display(Name = "Pack Size")]
        public string MedicationPackSize { get; set; }

        [Required(ErrorMessage = "Consumption duration is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive number")]
        [Display(Name = "Consumption Duration (days)")]
        public int MedicationPackConsumptionDuration { get; set; }

        [Required(ErrorMessage = "Original price is required")]
        [Range(0.001, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Original Price")]
        public decimal OriginalPrice { get; set; }

        [Required(ErrorMessage = "Discounted price is required")]
        [Range(0.001, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Price After Discount")]
        public decimal PriceAfterDiscount { get; set; }

        [Display(Name = "HCP List (comma-separated)")]
        public string HCPList { get; set; }
        [Display(Name = "Pharmacies")]
        public List<int> SelectedPharmacyIds { get; set; } = new List<int>();
        [ValidateNever]
        public MultiSelectList PharmacyOptions { get; set; }

        [Display(Name = "Distributors")]
        public List<int> SelectedDistributorIds { get; set; } = new List<int>();
        [ValidateNever]

        public MultiSelectList DistributorOptions { get; set; }
    }
}