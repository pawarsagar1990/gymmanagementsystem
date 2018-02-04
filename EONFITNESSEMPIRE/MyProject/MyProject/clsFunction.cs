using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyProject.Models;
using System.Data;

namespace MyProject
{
    public class clsFunction
    {
        DbGymEntities db = new DbGymEntities();
        public IEnumerable<PackageDetail> PkgListAdmin()
        {
            try
            {
                List<PackageDetail> pd = db.PackageDetails.ToList();
                return pd;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<MemberRegistration> MemberListData()
        {
            string status = "YES";
            List<MemberRegistration> mr = db.MemberRegistrations.Where(m => m.Flag.Equals(status)).OrderByDescending(m=>m.ID).ToList();
            //List<MemberRegistration> mr = db.MemberRegistrations.ToList();
            return mr;
        }
        public IEnumerable<Confirm_MemberRegistration> VerifictionRemainList()
        {            
            string FlagNO = "WATING";
            try
            { 
                List<Confirm_MemberRegistration> NotConfirm = db.Confirm_MemberRegistration.Where(m => m.ConfirmFlag.Equals(FlagNO)).ToList();
                HttpContext.Current.Session["WatingList"] = NotConfirm.Count();
                return NotConfirm;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int RenewMemberCount()
        {
            int count = 0;
            DateTime today = DateTime.Today;
            List<TransactionDetail> td = db.TransactionDetails.ToList();
            if(td != null)
            {
                foreach (TransactionDetail item in td)
                {
                    DateTime dt = (DateTime)item.PackageEndDate;
                    DateTime EndDateEarlier = dt.AddDays(-5);
                    if (EndDateEarlier == today)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public IEnumerable<MemberRegistration> RenewListNotification()
        {
            try
            {
                int count = 0;
                string status = "YES";
                DateTime today = DateTime.Today;
                List<MemberRegistration> lstdata = new List<MemberRegistration>();
                List<MemberRegistration> mr = db.MemberRegistrations.Where(m => m.Flag.Equals(status)).ToList();
                //List<TransactionDetail> data = db.TransactionDetails.ToList();
               foreach(MemberRegistration item in mr)
                {
                    if(item != null)
                    {
                        int id = item.ID;
                        // int TransactionID = data.
                        TransactionDetail data = db.TransactionDetails.Where(m => m.MemberRegistration_PK_ID == id).OrderByDescending(m=> m.TransactionDetailsID).FirstOrDefault();
                        if(data != null)
                        {
                            DateTime pkgEndDate = (DateTime)data.PackageEndDate;
                            if (data.PackageEndDate.Value.Subtract(DateTime.Now).TotalDays <= 5 || (data.NextPaymentDate != null ? data.NextPaymentDate.Value.Subtract(DateTime.Now).TotalDays <= 5 : false))
                            {
                                int? DataID = data.MemberRegistration_PK_ID;
                                MemberRegistration lstmr = db.MemberRegistrations.Where(m => m.ID == DataID).FirstOrDefault();
                                lstdata.Add(lstmr);
                                count++;
                            }
                        }
                    }
                }
                HttpContext.Current.Session["RenewMemberCount"] = count;
                //mr.TransactionDetails.Count > 0 ? mr.TransactionDetails.LastOrDefault().PackageDetails_PK_ID : 0;
                return lstdata;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int RemainsDays(int id)
        {
            int days = 0;
            try
            {
                MemberRegistration mr = db.MemberRegistrations.Where(m => m.ID.Equals(id)).FirstOrDefault();
                if (mr != null)
                {
                    if (mr.TransactionDetails.Count > 0)
                    {
                        int pdid = Convert.ToInt32(mr.TransactionDetails.LastOrDefault().PackageDetails_PK_ID);
                        PackageDetail pd = db.PackageDetails.Where(m => m.ID.Equals(pdid)).FirstOrDefault();
                        int month = Convert.ToInt32(pd.NumberOfMonth);
                        DateTime DOJ = (DateTime)mr.DOJ;
                        DateTime EndDate = DOJ.AddMonths(month);
                        DateTime TodayDate = DateTime.Now;
                        TimeSpan ResultDate = (EndDate - TodayDate);
                        days = Convert.ToInt32(ResultDate.TotalDays);
                    }                    
                }
                return days;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public  AdminHome()
        //{
        //    string data = "";
        //    int Data = 0;
        //    try
        //    {
               
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public string Roles()
        {
            string role = "";
            try
            {
                int id = Convert.ToInt32(HttpContext.Current.Session["ID"]);
                UserLogin us = db.UserLogins.Where(m => m.ID.Equals(id)).FirstOrDefault();
                if (us != null)
                {
                    role = us.UserRole;
                }
                return role;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int TotalCurrentMonthJoining()
        {
            int CurrentMonthJoining = 0;
            string status = "YES";
            try
            {
                List<MemberRegistration> mr = db.MemberRegistrations.Where(m => m.Flag.Equals(status)).ToList();
                HttpContext.Current.Session["TotalMember"] = mr.Count();
                // Pending Registration 
                // Staff
                foreach (MemberRegistration item in mr)
                {
                    if (item.DOJ != null)
                    {
                        DateTime DOJ = (DateTime)item.DOJ;
                    
                        string JoningMonth = (DOJ.Month).ToString();
                        string CurrentMonth = DateTime.Now.Month.ToString();
                        if (JoningMonth == CurrentMonth)
                        {
                            CurrentMonthJoining++;
                        }
                    }
                }
                return (CurrentMonthJoining);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public IEnumerable<MemberRegistration> ListJoinCurrentMonth()
        {
            var StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            List<MemberRegistration> lst = db.MemberRegistrations
                .Where(m => m.DOJ >= StartDate)
                .Where(m => m.DOJ <= EndDate)
                .OrderByDescending(m => m.ID).ToList();

            //string CurrentMonth = DateTime.Now.Month.ToString();
            //string CurrentYear = DateTime.Now.Year.ToString();
            //foreach(var item in lst)
            //{
            //    string ListMonth = item.DOJ;
            //}
            
           // List<MemberRegistration> lst = db.MemberRegistrations.Where(m => m.DOJ.Equals(CurrentMonth)).ToList();
            return lst;
        }
        public string MemberRgistrationConfirm(int id)
        {
            string result = "Registration Successfully Done!!!";
            try
            {
                Confirm_MemberRegistration cmr = db.Confirm_MemberRegistration.Where(m => m.ID.Equals(id)).FirstOrDefault();
                //DataTable dt = new DataTable();
                //dt.Rows.Add(cmr);
                MemberRegistration mr = new MemberRegistration();
                mr.MemberName = cmr.MemberName;
                mr.Email = cmr.Email;
                mr.Password = cmr.Password;
                mr.DOJ = cmr.DOJ;
                mr.DOB = cmr.DOB;
                mr.Gender = cmr.Gender;
                mr.Address = cmr.Address;
                mr.City = cmr.City;
                mr.State = cmr.State;
                //  mr.Employe = cmr.Employe;
                mr.ZipCode = cmr.ZipCode;
                mr.EmergencyContactName = cmr.EmergencyContactName;
                mr.EmergencyContactNumber = cmr.EmergencyContactNumber;
                mr.MobileNumber = cmr.MobileNumber;
                mr.Package_ID = cmr.Package_ID;
                mr.Payment_Type_ID = cmr.Payment_Type_ID;
                mr.Installment_Method = cmr.Installment_Method;
                mr.ProfilePic = cmr.ProfilePic;
                mr.Flag = cmr.Flag;
                mr.CreatedOn = cmr.CreatedOn;
                mr.CreatedBy = cmr.CreatedBy;
                string email = mr.Email;
                string number = mr.MobileNumber;
                db.MemberRegistrations.Add(mr);
                db.SaveChanges();
                MemberRegistration data = db.MemberRegistrations.Where(m => m.Email.Equals(email) && m.MobileNumber.Equals(number)).FirstOrDefault();
                
                UserLogin us = new UserLogin();
                us.MemberRegistration_PK_ID = data.ID;
                us.Email = data.Email;
                us.Password = data.Password;
                us.Mobile = data.MobileNumber;
                us.UserRole = "U";
                us.Flag = data.Flag;
                us.CreatedOn = DateTime.Now;
                us.CreatedBy = "";
                db.UserLogins.Add(us);
                db.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}