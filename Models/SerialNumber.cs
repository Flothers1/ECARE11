using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECARE.Models
{
    public class SerialNumber
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Code { get; set; }

        public int CareProgramId { get; set; }
        [ForeignKey("CareProgramId")]
        public CareProgram CareProgram { get; set; }
    }
}
