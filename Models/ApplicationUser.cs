using Microsoft.AspNetCore.Identity;

namespace ECARE.Models
{
    public class ApplicationUser : IdentityUser
    {
       public int? LabBranchId { get; set; }
       public int? LabId { get; set; }
        public LabBranch? LabBranch { get; set; }
    }
}
