using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECARE.Models
{
    public class PharmacyServiceRequest
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow.AddHours(2);
        public bool Payment { get; set; }
        public bool? IsDeleted { get; set; }
        public string EVoucherPDF { get; set; }
        [MaxLength(100)]
        public string? OTP { get; set; }
        public DateTime OTPExpiration { get; set; }
        public bool? IsVerified { get; set; }
        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]

        public Program? Program { get; set; }
        public int PRId { get; set; }

        [ForeignKey("PRId")]

        public PatientRegistrations? PatientRegistrations { get; set; }

    }
}
