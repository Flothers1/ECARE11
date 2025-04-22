using System.ComponentModel.DataAnnotations;

namespace ECARE.Models
{
    public class PharmacyBranch
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public ICollection <ApplicationUser>? ApplicationUsers { get; set; }
        public int  PharmacyId { get; set; }
        public Pharmacy? Pharmacy { get; set; }

    }
}
