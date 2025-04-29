using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ECARE.ViewModel
{
    public class PharmacyServiceRequestIndexViewModel
    {
        // Pharmacy service request
        public int Id { get; set; }
        [Display(Name = "Request Date")]
        public DateTime Date { get; set; } = DateTime.UtcNow.AddHours(2);
        [Display (Name = "EVoucher PDF")]
        public string EVoucherPDF { get; set; }
        public int PharmacyBranchId { get; set; }
        [Display(Name = "Pharmacy branch")]

        public string PharmacyBranch { get; set; }
        // patient registration
        public int PatientRegistrationsId { get; set; }
        [Display (Name="Patient Name")]
        public string PatientName { get; set; }
        [Display (Name= "National ID")]
        public string NationalId { get; set; }

        //care program
        public int pharmacyId { get; set; }
        [Display(Name = "Pharmacy")]
        public string Pharmacy { get; set; }
        [Display (Name = "Price After Discount")]
        public decimal PriceAfterDiscount { get; set; }
        [Display(Name = "Medication Name")]

        public string MedicationName { get; set; }


    }
}
