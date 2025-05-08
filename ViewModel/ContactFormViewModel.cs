using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace ECARE.ViewModel
{
    public class ContactFormViewModel
    {
        [Display(Name = "الاسم")]
        public string Name { get; set; }
        [Display(Name = "رقم الموبيل")]
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Must be exactly 11 digits.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Must be exactly 11 numeric digits.")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "صوره الروشته")]
        public IFormFile Prescription { get; set; }
        [Required]
        [Display(Name = "وجه البطاقه - صورة الرقم القومي")]
        public IFormFile NationalIDPhotoFront { get; set; }
        [Display(Name = "ظهر البطاقه - صورة الرقم القومي")]
        public IFormFile? NationalIDPhotoBack { get; set; }
        [Required]
        [Display(Name = "PSA صورة تحليل")]
        public IFormFile PSATestImage { get; set; }
        [Required]
        [Display(Name = "صورة المسح الذري")]
        public IFormFile AtomicScanningImage { get; set; }
    }
}
