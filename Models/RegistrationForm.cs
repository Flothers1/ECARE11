using System.ComponentModel.DataAnnotations;

namespace ECARE.Models
{
    public class RegistrationForm
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Must be exactly 11 digits.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Must be exactly 11 numeric digits.")]
        public string PhoneNumber { get; set; }
        [MaxLength(500)]
        public string Prescription { get; set; }
        [MaxLength(500)]
        public string NationalIDPhotoFront { get; set; }
        [MaxLength(500)]
        public string NationalIDPhotoBack { get; set; }
        [MaxLength(500)]
        public string PSATestImage { get; set; }
        [MaxLength(500)]
        public string AtomicScanningImage { get; set; }
    }
}
