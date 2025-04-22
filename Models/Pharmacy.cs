using System.ComponentModel.DataAnnotations;

namespace ECARE.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<PharmacyBranch> PharmacyBranches { get; set; }
            = new HashSet<PharmacyBranch>();

    }
}
