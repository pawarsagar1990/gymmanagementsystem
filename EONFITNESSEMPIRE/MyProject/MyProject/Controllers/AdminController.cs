using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using System.IO;
using System.Web.Security;
using PagedList;
using PagedList.Mvc;

namespace MyProject.Controllers
{
    public class AdminController : Controller
    {
        DbGymEntities db = new DbGymEntities();
        clsFunction objcls = new clsFunction();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin us)
        {
            try
            {
                UserLogin u = db.UserLogins.Where(usr => usr.Email.Equals(us.Email) && usr.Password.Equals(us.Password)).FirstOrDefault();
                if (u != null)
                {
                    FormsAuthentication.SetAuthCookie(u.Email, false);
                    Session["ID"] = u.ID;
                    int? id = u.MemberRegistration_PK_ID;
                    MemberRegistration mr = db.MemberRegistrations.Where(m => m.ID == id).FirstOrDefault();
                    Session["UserName"] = mr.MemberName;
                    Session["ProfilePic"] = mr.ProfilePic;
                    return RedirectToAction("Home", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Enter Correct Email and Password");
                    return RedirectToAction("Login", "Admin");
                }
            }
            catch (Exception)
            {
                throw;
            } 
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult Home(int? page)
        {
            int currentmonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            string dt = currentYear + "-" + currentmonth + "-1";
            int id = Convert.ToInt32(Session["ID"]);
            ViewBag.RenewMemberCount = objcls.RenewMemberCount();
            string userrole = objcls.Roles();
            try
            {
                if (userrole == "A")
                {
                    ViewBag.TotalCurrentMonthJoining = objcls.TotalCurrentMonthJoining();
                    return View((objcls.VerifictionRemainList()).ToPagedList(page ?? 1, 10));
                    //return PartialView("_MemberListForAdmin", (objcls.MemberListData()).ToPagedList(page ?? 1, 10));
                }
                if (userrole == "U")
                {
                    ViewBag.days = objcls.RemainsDays(id);
                }
                if (userrole == "U")
                {

                }
                
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize(Roles = "A")]
        public ActionResult MemberRegistration()
        {
                    try
                    {
                        if (objcls.Roles() == "A")
                        {
                            List<SelectListItem> ObjListAdmin = new List<SelectListItem>()
                            {
                             new SelectListItem { Text = "User Member", Value = "U" },
                             new SelectListItem { Text = "Employee", Value = "S" },
                             new SelectListItem { Text = "Admin", Value = "A" },
                            };
                            ViewBag.Roles = ObjListAdmin;
                        }
                        else if (objcls.Roles() == "S")
                        {
                            List<SelectListItem> ObjList = new List<SelectListItem>()
                             {
                                 new SelectListItem { Text = "User Member", Value = "U" },
                                 new SelectListItem { Text = "Employee", Value = "S" },
                             };
                            ViewBag.Roles = ObjList;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                return View(); 
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult MemberRegistration(MemberRegistration mr, HttpPostedFileBase file, FormCollection collection)
        {
            try
            {
                string path = null;
                string fname = null;
                string em = mr.Email;
                
                if (mr.Flag == "YES") { mr.Flag = "YES"; } else { mr.Flag = "NO"; }
                if (file != null && file.ContentLength > 0)
                
                    try
                    {
                        var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                        path = Path.Combine(Server.MapPath(@"~/Images/ProfilePic"), Path.GetFileName(fileName));
                        fname = Path.GetFileName(fileName);
                        file.SaveAs(path);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    mr.ProfilePic = fname;
                    DateTime dt = DateTime.Now;
                    mr.CreatedOn = dt;
                    mr.CreatedBy = "Admin";//Session["UserName"].ToString();
                    db.MemberRegistrations.Add(mr);
                    db.SaveChanges();
                    // UserLogin required data save
                    MemberRegistration userlogindata = db.MemberRegistrations.Where(m => m.Email == em).FirstOrDefault();
                if (userlogindata != null)
                    try
                    {
                        UserLogin us = new UserLogin();
                        us.MemberRegistration_PK_ID = userlogindata.ID;
                        us.Mobile = userlogindata.MobileNumber;
                        us.Email = userlogindata.Email;
                        int LoginID = Convert.ToInt32(Session["ID"]);
                        UserLogin userrole = db.UserLogins.Find(LoginID);
                        if(userrole != null)
                            try
                            {
                                if (userrole.UserRole == "A" || userrole.UserRole == "S")
                                {
                                    us.UserRole = collection["RolesForAll"].ToString();
                                }
                                else
                                {
                                    us.UserRole = "U";
                                }
                               
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        
                        us.Flag = "YES";
                        us.Password = userlogindata.Password;
                        us.CreatedOn = dt;
                        db.UserLogins.Add(us);
                        db.SaveChanges();
                        string MembershipID = "EON"+DateTime.Now.Year+userlogindata.ID;
                        userlogindata.MembershipID = MembershipID;
                        db.Entry(userlogindata).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    
                ViewBag.Message = mr.MemberName + " : Successfully Registration";
                return View();
            }
            catch (Exception ex)
            { 
                throw;
            }
        }
        public ActionResult MemberEditData(int id)
        {
            try
            {
                MemberRegistration mr = db.MemberRegistrations.Where(m => m.ID.Equals(id)).FirstOrDefault();
                return View(mr);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult MemberEditData(MemberRegistration mr)
        {
            try
            {
                db.Entry(mr).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["UpdateMemberData"] = mr.MemberName + "Successfully Updated!";
                return RedirectToAction("MemberListForAdmin");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(Roles = "A")]
        public ActionResult MemberListForAdmin(int? page)
        {
            try
            {
                List<MemberRegistration> mr = db.MemberRegistrations.ToList();
                return View(mr.ToPagedList(page ?? 1, 10));
            }
            catch (Exception)
            { 
                throw;
            }
        }
        public ActionResult ActiveMemberListForAdmin(int? page)
        {
            try
            {
                return View(objcls.MemberListData().ToPagedList(page ?? 1, 10));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize(Roles = "A")]
        public ActionResult UserLoginList(int? page)
        {
            try
            {
                List<UserLogin> us = db.UserLogins.ToList();
                return View(us.ToPagedList(page ?? 1, 10));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult MemberProfileDetails(int id)
        {
            try
            {
                MemberRegistration mr = db.MemberRegistrations.Where(m => m.ID.Equals(id)).FirstOrDefault();
                if (mr != null)
                {
                    ViewBag.date = objcls.RemainsDays(id);
                }
                return View(mr);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult PackageEntry()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult PackageEntry(PackageDetail pd)
        {
            try
            {
                if (pd.Flag == "YES") { pd.Flag = "YES"; } else { pd.Flag = "NO"; }
                DateTime dt = DateTime.Now;
                pd.CreatedOn = dt;
                pd.CreatedBy = Session["UserName"].ToString();
                db.PackageDetails.Add(pd);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Package Entry Successfully Done!!!";
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult PackageListForAdmin(int? page)
        {
            try
            {
                return View((objcls.PkgListAdmin()).ToPagedList(page ?? 1, 10));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult PackageDataEdit(int id)
        {
            try
            {
                // if (mr.Flag == "YES") { mr.Flag = "YES"; } else { mr.Flag = "NO"; }
                PackageDetail pd = db.PackageDetails.Where(m => m.ID.Equals(id)).FirstOrDefault();
                return View(pd);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult PackageDataEdit(PackageDetail pd)
        {
            try
            {
                if (pd.Flag == "YES") { pd.Flag = "YES"; } else { pd.Flag = "NO"; }
                pd.CreatedOn = DateTime.Now;
                pd.CreatedBy = Session["UserName"].ToString();
                db.Entry(pd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PackageListForAdmin", "Admin");
            }
            catch (Exception)
            {

                throw;
            }
        }
       public ActionResult MemberRgistrationIfConfirm(int id)
        {
            try
            {
                Session["msg"] = objcls.MemberRgistrationConfirm(id);
                Confirm_MemberRegistration cm = db.Confirm_MemberRegistration.Where(m => m.ID.Equals(id)).FirstOrDefault();
                cm.ConfirmFlag = "YES";
                db.Entry(cm).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Home","Admin");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult ForgetPassword()
        {
            try
            {
                return RedirectToAction("Login","Admin");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult PhysicalAssessment(int id, int? page)
        {
            try
            {
                MemberRegistration mr = db.MemberRegistrations.Where(m => m.ID.Equals(id)).FirstOrDefault();
                ViewBag.Name = mr.MemberName;
                ViewBag.Mobile = mr.MobileNumber;
                Session["ItemID"] = id;
                List<PhysicalAssessment> pa = db.PhysicalAssessments.Where(m => m.MemberRegistration.ID.Equals(id)).ToList();
                return View(pa.ToPagedList(page ?? 1, 10));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult PhysicalAssessmentDelete(int id)
        {
            try
            {
                PhysicalAssessment pa = db.PhysicalAssessments.Where(m => m.ID.Equals(id)).FirstOrDefault();
                db.PhysicalAssessments.Remove(pa);
                db.SaveChanges();
                int ItemID = Convert.ToInt32(Session["ItemID"]);
               // return View("PhysicalAssessment", "Admin");
                return RedirectToAction("PhysicalAssessment","Admin", new { id = ItemID });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SavePhysicalAssessment(PhysicalAssessment pa)
        {
            try
            {
                pa.CreatedBy = Session["UserName"].ToString();
                pa.CreatedOn = DateTime.Now;
                db.PhysicalAssessments.Add(pa);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult ListJoinCurrentMonthForAdmin(int? page)
        {
            try
            {
                return View((objcls.ListJoinCurrentMonth()).ToPagedList(page ?? 1, 10));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult MemberPayment(int id, int? page)
        {
            try
            {
                MemberRegistration mr = db.MemberRegistrations.Where(m => m.ID.Equals(id)).FirstOrDefault();
                ViewBag.Name = mr.MemberName;
                ViewBag.Mobile = mr.MobileNumber;
                ViewBag.MemberID = id;
                ViewBag.SelectedPackage = mr.TransactionDetails.Count > 0 ? mr.TransactionDetails.LastOrDefault().PackageDetails_PK_ID : 0;
                List<TransactionDetail> MemberTransactionDetails = db.TransactionDetails.Where(m => m.MemberRegistration_PK_ID.Value.Equals(id)).ToList();
                return View(MemberTransactionDetails.ToPagedList(page ?? 1, 10));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveMemberPayment(TransactionDetail memberTransactionDetail)
        {
            try
            {
                memberTransactionDetail.CreatedBy = Session["UserName"].ToString();
                memberTransactionDetail.CreatedOn = DateTime.Now;
                memberTransactionDetail.Payment_Date = DateTime.Now;                
                db.TransactionDetails.Add(memberTransactionDetail);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult RenewMemberList()
        {
            try
            {
                string status = "YES";
                List<MemberRegistration> mr = db.MemberRegistrations.Where(m => m.Flag.Equals(status)).ToList();
                DateTime today = DateTime.Today;
                DateTime sevenDaysEarlier = today.AddDays(-5);
                
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(Roles = "A")]
        public ActionResult MemberSearch(int? page)
        {
            try
            {
                ///search area starts
                string memberName = Request.QueryString["Name"] != null ? Request.QueryString["Name"] : "";
                int memberSelectedPackage = Request.QueryString["PackageDetails_PK_ID"] != null && Request.QueryString["PackageDetails_PK_ID"] != "" ? Convert.ToInt32(Request.QueryString["PackageDetails_PK_ID"]) : 0;
                string memberMobileNumber = Request.QueryString["MobileNumber"] != null ? Request.QueryString["MobileNumber"] : "";
                List<MemberRegistration> mr = db.MemberRegistrations
                    .Where(member => member.MemberName.Contains(memberName))
                    .Where(member => member.MobileNumber.Contains(memberMobileNumber))
                    //.Where(member => member.TransactionDetails.LastOrDefault().PackageDetails_PK_ID == memberSelectedPackage)                 
                    .OrderByDescending(m => m.ID).ToList();
                 
                return View(mr.ToPagedList(page ?? 1, 10));
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
