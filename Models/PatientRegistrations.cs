using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECARE.Models
{
    public class PatientRegistrations 
    {
        
        [Key]
        public int PatientRegistrationsId { get; set; }
        [Column("[Registration Date]")]

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        [Column("[Patient Name]")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain letters only")]

        public string PatientName { get; set; } = string.Empty;
        [Column("[National ID]")]
        [RegularExpression(@"^\d+$", ErrorMessage = "National ID must contain numbers only")]

        public string NationalID { get; set; }

        [NotMapped] 
        public IFormFile CopyOfIDOrPassportFileFront { get; set; }

        [Column("CopyOfIDOrPassportFileFront")]
        public string CopyOfIDOrPassportPathFront { get; set; }
        [NotMapped]


        public IFormFile CopyOfIDOrPassportFileBack { get; set; }

        [Column("CopyOfIDOrPassportFileBack")]
        public string CopyOfIDOrPassportPathBack { get; set; }
        [Required]
        public string Gender { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Age must contain numbers only")]

        public int Age { get; set; }
        [Column("[Age Group]")]

        public string AgeGroup { get; set; }
        [Column("[Phone Number1]")]
        [RegularExpression(@"^\d{10,}$", ErrorMessage = "PhoneNumber1 must be at least 10 digits and contain numbers only")]
        public string PhoneNumber1 { get; set; }

        [Column("[Phone Number2]")]
        [RegularExpression(@"^\d{10,}$", ErrorMessage = "PhoneNumber2 must be at least 10 digits and contain numbers only")]
        public string PhoneNumber2 { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        public string Government { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Patient Territory Name must contain letters only")]

        public string Territory { get; set; }
        [Column("[Caregiver Name]")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Caregiver Name must contain letters only")]

        public string? CaregiverName { get; set; }
        [Column("[Caregiver Phone]")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Caregiver Phone must contain numbers only")]

        public string? CaregiverPhone { get; set; }

        public string ProgramName { get; set; }
        [Column("[Membership Number]")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Patient Membership Number must contain numbers only")]

        public string MembershipNumber { get; set; }
        [Column("[Program Sponsor]")]

        public string ProgramSponsor { get; set; }

        public string Medication { get; set; }

        public string Indication { get; set; }

        public string PatientAddress { get; set; }

        public string? Comment { get; set; }
        [Column("[Start Date Of Medication]")]

        public DateTime StartDateOfMedication { get; set; }
        [Column("[Healthcare Provider]")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "HealthcareProvider must contain letters only")]

        public string HealthcareProvider { get; set; }
        [Column("[Healthcare Provider Address]")]

        public string HealthcareProviderAddress { get; set; }
        [Column("[HCP Government ]")]

        public string HCPGovernment { get; set; }
        [Column("[HCP Territory ]")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "HCPTerritory must contain letters only")]

        public string HCPTerritory { get; set; }
        [NotMapped]
        public IFormFile PrescriptionFile { get; set; }

        public string? Prescription { get; set; }

        [NotMapped]
        public IFormFile TestDocument1File { get; set; }
        public string TestDocument1 { get; set; }
        [NotMapped]
        public IFormFile? TestDocument2File { get; set; }
        public string? TestDocument2 { get; set; }
        [NotMapped]
        public IFormFile? Invoice { get; set; }
        public string? Invoice1 { get; set; }

        public ICollection<Service_Request>? ServiceRequests { get; set; }
    }

}
