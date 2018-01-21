//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Confirm_MemberRegistration
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public string MemberName { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public Nullable<System.DateTime> DOB { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public Nullable<System.DateTime> DOJ { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public string City { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public string State { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public string ZipCode { get; set; }
       
        public string Employe { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public string EmergencyContactName { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public string EmergencyContactNumber { get; set; }
        [Required(ErrorMessage = "Field can't empty")]
        public string MobileNumber { get; set; }
        public Nullable<int> Package_ID { get; set; }
        public Nullable<int> Payment_Type_ID { get; set; }
        public string Installment_Method { get; set; }
        public string ProfilePic { get; set; }
        public string ConfirmFlag { get; set; }
        public string Flag { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual PackageDetail PackageDetail { get; set; }
        public virtual PaymentDetail PaymentDetail { get; set; }
    }
}
