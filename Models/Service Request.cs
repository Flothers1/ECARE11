using System.ComponentModel.DataAnnotations;

namespace ECARE.Models
{
    public class Service_Request
    {
        [Key]
        public int Service_RequestId { get; set; }
        [Display(Name = "E-Voucher PDF")]

        public string? EVoucherPDF { get; set; }

        [Display(Name = "Required Tests")]
        public string? RequiredTests { get; set; }

        [Display(Name = "Owner Type")]
        public string? OwnerType { get; set; }
        public DateTime? Date { get; set; } = Constants.SD.TimeInEgypt;
        public DateTime? RequestClosedDate { get; set; }
        [Display(Name = "CoPayment Percentage")]

        public double? CoPaymentPercentage { get; set; }
        public bool Payment { get; set; }
        public bool? IsDeleted { get; set; }
        public string? OTP { get; set; }
        public DateTime OTPExpiration { get; set; }
        public bool? IsVerified { get; set; }

        public string? Invoice { get; set; }
        public int LabBranchId { get; set; }
        [Display(Name = "Lab Branch")]

        public LabBranch? LabBranch { get; set; }
        public int PatientRegistrationsId { get; set; }
        public PatientRegistrations? PatientRegistrations { get; set; }

    }


}

