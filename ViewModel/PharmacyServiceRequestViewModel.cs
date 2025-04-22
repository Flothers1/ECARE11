using ECARE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ECARE.ViewModel
{
    public class PharmacyServiceRequestViewModel
    {
        [MaxLength(255)]
        public string Medication { get; set; }
        [MaxLength(500)]
        public string MedicationPackSize { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow.AddHours(2);
        public bool Payment { get; set; }
        public bool? IsDeleted { get; set; }
        public IFormFile EVoucherPDF { get; set; }
        [MaxLength(100)]
        public string? OTP { get; set; }
        public DateTime OTPExpiration { get; set; }
        public bool? IsVerified { get; set; }
        public List<string> Pharmacies { get; set; }
        public List<SelectListItem> PharmacyOptions { get; set; } = new List<SelectListItem>();
        public List<string> Distributors { get; set; }
        public List<SelectListItem> DistributorOptions { get; set; } = new List<SelectListItem>();
        public int PatientRegistrationsId { get; set; }
        public PatientRegistrations? PatientRegistrations { get; set; }

    }
}
