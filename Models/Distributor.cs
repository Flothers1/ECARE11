using System.ComponentModel.DataAnnotations;

namespace ECARE.Models
{
    public class Distributor
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
