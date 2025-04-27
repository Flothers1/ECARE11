using System.ComponentModel.DataAnnotations;

namespace ECARE.ViewModel
{
    public class PharmacyIndexViewModel
    {
        // Identifiers
        public int PharmacyServiceRequestId { get; set; }
        public int PatientRegistrationsId { get; set; }
        public int PharmacyId { get; set; }
        // From PatientRegistrations
        public string PatientName { get; set; }
        public string PhoneNumber1 { get; set; }
        public string NationalID { get; set; }
        public string Email { get; set; }

        // From CareProgram
        public string MedicationName { get; set; }
        public decimal PriceAfterDiscount { get; set; }

        // From Pharmacy/PharmacyBranch
        public string PharmacyName { get; set; }
        public string BranchName { get; set; }

        // From PharmacyServiceRequest
        public bool Payment { get; set; }
        public string EVoucherPDF { get; set; }
        [MaxLength(100)]
        public string? OTP { get; set; }
        public DateTime OTPExpiration { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsDeleted { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime RequestDate { get; set; }

    }
}
