using Microsoft.AspNetCore.Identity;

namespace ECARE.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? LabId { get; set; }
        public int? PharmacyId { get; set; }
        public int? LabBranchId { get; set; }
        public LabBranch? LabBranch { get; set; }
        public int? PharmacyBranchId { get; set; }
        public PharmacyBranch? PharmacyBranch { get; set; }
    }
}
