using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace ECARE.ViewModel
{
    public class ContactFormViewModel
    {
        [Display(Name = "الاسم")]
        public string Name { get; set; }
        [Display(Name = "رقم الموبيل")]
        public int PhoneNumber { get; set; }
        [Display(Name = "صوره الروشته")]
        public IFormFile Prescription { get; set; }
        [Display(Name = "وجه البطاقه - صورة الرقم القومي")]
        public IFormFile NationalIDPhotoFront { get; set; }
        [Display(Name = "ظهر البطاقه - صورة الرقم القومي")]
        public IFormFile NationalIDPhotoBack { get; set; }
        [Display(Name = "PSA صورة تحليل")]
        public IFormFile PSATestImage { get; set; }
        [Display(Name = "صورة المسح الذري")]
        public IFormFile AtomicScanningImage { get; set; }
    }
}
