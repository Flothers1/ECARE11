using ECARE.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ECARE.ViewModel
{
    public class PharmacyServiceRequestViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Request Date")]
        public DateTime Date { get; set; } 
        [Display(Name = "Payment Status")]
        public bool Payment { get; set; }
        public bool? IsDeleted { get; set; }
        public IFormFile EVoucherPDF { get; set; }
        [MaxLength(100)]
        public string? OTP { get; set; }
        public DateTime OTPExpiration { get; set; }
        [Display(Name = "Verification Status")]
        public bool? IsVerified { get; set; }
        [Display(Name = "Select Care Program")]
        public int SelectedCareProgramId { get; set; }
        [ValidateNever]
        public SelectList CareProgramOptions { get; set; }
        [Display(Name = "Select Patient")]
        public int PatientRegistrationsId { get; set; }
        [ValidateNever]
        public SelectList PatientRegistrationOptions { get; set; }
        [Display(Name = "Select Pharmacy")]
        public int pharmacyId { get; set; }
        [ValidateNever]
        public SelectList PharmaciesOptions { get; set; }
        [Display(Name = "Select Pharmacy Branch")]
        public int PharmacyBranchId { get; set; }


    }
}
