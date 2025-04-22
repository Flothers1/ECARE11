using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECARE.Models
{
    public class Program
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; } 
        [MaxLength(100)]
        public string ProductManager { get; set; }
        [MaxLength(255)]

        public string MedicationName { get; set; }
        [MaxLength(255)]
        public string MedicationPackSize { get; set; }
        public int MedicationPackConsumptionDuration { get; set; }
        [Precision(18,3)]
        public decimal OriginalPrice { get; set; }
        [Precision(18, 3)]

        public decimal PriceAfterDiscount { get; set; }
        public List<string> HCPList { get; set; } = new List<string>();

        public ICollection<Pharmacy> Pharmacies { get; set; } = new HashSet<Pharmacy>();
        public ICollection<Distributor> Distributors { get; set; } = new HashSet<Distributor>();
        public ICollection<PharmacyServiceRequest> PharmaciesServiceRequests { get; set; }
            = new HashSet<PharmacyServiceRequest>();
        public ICollection<PatientRegistrations> PatientRegistrations { get; set; }
        = new HashSet<PatientRegistrations>();
    }
}
