using ECARE.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECARE.ViewModel
{
    public class PatientRegistrationIndexViewModel
    {
        public int PatientRegistrationsId { get; set; }
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; } = Constants.SD.TimeInEgypt;
        [Display(Name = "Patient Name")]

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain letters only")]
        [Required]
        public string PatientName { get; set; } = string.Empty;
        [Display(Name = "National ID")]
        [RegularExpression(@"^\d+$", ErrorMessage = "National ID must contain numbers only")]
        public string NationalID { get; set; }

        [Display(Name = "Copy Of ID/Passport(Front)")]
        public string CopyOfIDOrPassportPathFront { get; set; }

        [Display(Name = "Copy Of ID/Passport(Back)")]
        public string CopyOfIDOrPassportPathBack { get; set; }
        public string Gender { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Age must contain numbers only")]

        public int Age { get; set; }
        [Display(Name ="Age Group")]

        public string AgeGroup { get; set; }
        [Display(Name ="Phone Number1")]
        [RegularExpression(@"^\d{10,}$", ErrorMessage = "PhoneNumber1 must be at least 10 digits and contain numbers only")]
        public string PhoneNumber1 { get; set; }

        [Display(Name ="Phone Number2")]
        [RegularExpression(@"^\d{10,}$", ErrorMessage = "PhoneNumber2 must be at least 10 digits and contain numbers only")]
        public string PhoneNumber2 { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        public string Government { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Patient Territory Name must contain letters only")]

        public string Territory { get; set; }
        [Display(Name = "Caregiver Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Caregiver Name must contain letters only")]

        public string? CaregiverName { get; set; }
        [Display(Name = "Caregiver Phone")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Caregiver Phone must contain numbers only")]

        public string? CaregiverPhone { get; set; }

        [Display(Name = "Membership Number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Patient Membership Number must contain numbers only")]
        public string MembershipNumber { get; set; }
        public string Indication { get; set; }
        public string PatientAddress { get; set; }
        public string? Comment { get; set; }
        [Display(Name = "Start Date Of Medication")]
        public DateTime StartDateOfMedication { get; set; }
        [Display(Name = "HealthCare Provider")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "HealthcareProvider must contain letters only")]
        public string HealthcareProvider { get; set; }
        [Display(Name = "HealthCare Provider Address")]
        public string HealthcareProviderAddress { get; set; }
        [Display(Name = "HCP Government")]

        public string HCPGovernment { get; set; }
        [Display(Name = "HCP Territory")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "HCPTerritory must contain letters only")]

        public string HCPTerritory { get; set; }

        public string? Prescription { get; set; }
        public string TestDocument1 { get; set; }
        public string? TestDocument2 { get; set; }
        public string? Invoice1 { get; set; }
        public int CareProgramId { get; set; }
        [Display(Name ="Program Name")]
        public string  ProgramName { get; set; }
    }
}
