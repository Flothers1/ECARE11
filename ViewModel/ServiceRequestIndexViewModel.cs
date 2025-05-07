using ECARE.Models;
using System.ComponentModel.DataAnnotations;

namespace ECARE.ViewModel
{
    public class ServiceRequestIndexViewModel
    {
        public int Service_RequestId { get; set; }
        [Required(ErrorMessage = "E-voucher PDF file is required")]
        [Display(Name = "E-Voucher PDF")]

        public string EVoucherPDF { get; set; }

        public string? RequiredTests { get; set; }

        [Display(Name = "Owner Type")]
        public string? OwnerType { get; set; }
        public DateTime? Date { get; set; }
        [Display(Name = "CoPayment Percentage")]

        public double? CoPaymentPercentage { get; set; }

        public int LabBranchId { get; set; }
        [Display(Name = "Lab Branch")]

        public string LabBranch { get; set; }
        public bool Payment { get; set; }
        [Display(Name = "Patient Name")]

        public bool? IsDeleted { get; set; }
        public string? OTP { get; set; }
        public DateTime OTPExpiration { get; set; }
        public bool? IsVerified { get; set; }
        public int PatientRegistrationsId { get; set; }
        public string PatientName { get; set; }
        public int LabId { get; set; }
        public string Lab { get; set; }
        public string Invoice { get; set; }
        //Patient registration
        public string NationalId { get; set; }
        public string PhoneNumber1 { get; set; }
        public string Email { get; set; }

    }
}
