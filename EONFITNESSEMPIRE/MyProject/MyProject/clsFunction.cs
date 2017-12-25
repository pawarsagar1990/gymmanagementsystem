using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyProject.Models;

namespace MyProject
{
    public class clsFunction
    {
        DbGymEntities db = new DbGymEntities();
        public IEnumerable<MemberRegistration> MemberListData()
        {
            List<MemberRegistration> mr = db.MemberRegistrations.ToList();
            return mr;
        }
        public IEnumerable<Confirm_MemberRegistration> VerifictionRemainList()
        {            
            string FlagNO = "WATING";
            try
            { 
                List<Confirm_MemberRegistration> NotConfirm = db.Confirm_MemberRegistration.Where(m => m.ConfirmFlag.Equals(FlagNO)).ToList();
                return NotConfirm;
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
                    int pdid = Convert.ToInt32(mr.Package_ID);
                    PackageDetail pd = db.PackageDetails.Where(m => m.ID.Equals(pdid)).FirstOrDefault();
                    int month = Convert.ToInt32(pd.NumberOfMonth);
                    DateTime DOJ = (DateTime)mr.DOJ;
                    DateTime EndDate = DOJ.AddMonths(month);
                    DateTime TodayDate = DateTime.Now;
                    TimeSpan ResultDate = (EndDate - TodayDate);
                    days = Convert.ToInt32(ResultDate.TotalDays);
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
                    DateTime DOJ = (DateTime)item.DOJ;
                    string JoningMonth = (DOJ.Month).ToString();
                    string CurrentMonth = DateTime.Now.Month.ToString();
                    if (JoningMonth == CurrentMonth)
                    {
                        CurrentMonthJoining++;
                    }
                }
                return (CurrentMonthJoining);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}