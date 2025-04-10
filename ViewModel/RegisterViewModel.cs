using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECARE.ViewModel
{
    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int? LabId { get; set; }
        public int? LabBranchId { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Labs { get; set; }
    }
}
