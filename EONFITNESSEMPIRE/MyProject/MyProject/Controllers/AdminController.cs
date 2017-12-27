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
            int id = Convert.ToInt32(Session["ID"]);
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
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
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
                PackageDetail pd = db.PackageDetails.Where(m => m.ID.Equals(id)).FirstOrDefault();
                return View(pd);
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
    }
}
