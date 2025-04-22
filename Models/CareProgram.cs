using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECARE.Models
{
    public class CareProgram
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; } 
        [MaxLength(100)]
        [Required]
        public string ProductManager { get; set; }
        [MaxLength(255)]
        [Required]
        public string MedicationName { get; set; }
        [Required]
        [MaxLength(255)]
        public string MedicationPackSize { get; set; }
        [Required]
        public int MedicationPackConsumptionDuration { get; set; }
        [Required]
        [Precision(18,3)]
        public decimal OriginalPrice { get; set; }
        [Precision(18, 3)]
        [Required]
        public decimal PriceAfterDiscount { get; set; }
        [Required]

        public List<string> HCPList { get; set; } = new List<string>();
        [Required]

        public ICollection<Pharmacy> Pharmacies { get; set; } = new HashSet<Pharmacy>();
        [Required]

        public ICollection<Distributor> Distributors { get; set; } = new HashSet<Distributor>();
        public ICollection<PharmacyServiceRequest> PharmaciesServiceRequests { get; set; }
            = new HashSet<PharmacyServiceRequest>();
        public ICollection<PatientRegistrations> PatientRegistrations { get; set; }
    }
}
