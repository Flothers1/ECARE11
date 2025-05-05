using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECARE.Models
{
    public class PharmacyServiceRequest
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; } = Constants.SD.TimeInEgypt;
        public DateTime? RequestClosedDate { get; set; }
        public bool Payment { get; set; }
        public bool? IsDeleted { get; set; }
        [MaxLength(500)]
        public string EVoucherPDF { get; set; }
        [MaxLength(500)]
        public string? SignedEVoucher { get; set; }
        [MaxLength(100)]
        public string? OTP { get; set; }
        public DateTime OTPExpiration { get; set; }
        public bool? IsVerified { get; set; }

        public int CareProgramId { get; set; }
        [ForeignKey("CareProgramId")]

        public CareProgram? CareProgram { get; set; }
        public int PRId { get; set; }

        [ForeignKey("PRId")]
        public PatientRegistrations? PatientRegistrations { get; set; }
        public int PharmacyBranchId { get; set; }
        [ForeignKey("PharmacyBranchId")]
        public PharmacyBranch PharmacyBranch { get; set; }


    }
}
