﻿using ECARE.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECARE.ViewModel
{
    public class ServiceRequestViewModel
    {
        public int Service_RequestId { get; set; }
        [Required(ErrorMessage = "E-voucher PDF file is required")]
        [Display(Name = "E-Voucher PDF")]
        public IFormFile EVoucherPDFFile { get; set; }

        public string? RequiredTests { get; set; }

        [Display(Name = "Owner Type")]
        public string? OwnerType { get; set; }
        public DateTime? Date { get; set; } = Constants.SD.TimeInEgypt;
        [Display(Name = "CoPayment Percentage")]

        public double? CoPaymentPercentage { get; set; }

        public int LabBranchId { get; set; }
        [Display(Name = "Lab Branch")]
        //TODO: check this 
        public LabBranch? LabBranch { get; set; }
        public bool Payment { get; set; }

        public bool? IsDeleted { get; set; }
        public string? OTP { get; set; }
        public DateTime OTPExpiration { get; set; }
        public bool? IsVerified { get; set; }
        [Display(Name = "Patient Name")]

        public int PatientRegistrationsId { get; set; }
        
        public IFormFile? Invoice { get; set; }
    }
}
