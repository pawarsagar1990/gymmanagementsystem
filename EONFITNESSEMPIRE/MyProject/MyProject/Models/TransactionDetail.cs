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
    
    public partial class TransactionDetail
    {
        public int TransactionDetailsID { get; set; }
        public Nullable<int> MemberRegistration_PK_ID { get; set; }
        public Nullable<System.DateTime> PackageStartDate { get; set; }
        public Nullable<System.DateTime> PackageEndDate { get; set; }
        public Nullable<int> PackageDetails_PK_ID { get; set; }
        public Nullable<System.DateTime> Payment_Date { get; set; }
        public string MobileNumber { get; set; }
        public string PaymentType { get; set; }
        public Nullable<decimal> Paid_Amount { get; set; }
        public Nullable<decimal> Due_Amount { get; set; }
        public Nullable<System.DateTime> NextPaymentDate { get; set; }
        public Nullable<decimal> Advance_Amount { get; set; }
        public Nullable<decimal> TotalPackagePayment { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<decimal> Discount_Amount { get; set; }
        public string PaymentMode { get; set; }
    
        public virtual MemberRegistration MemberRegistration { get; set; }
        public virtual PackageDetail PackageDetail { get; set; }
    }
}
