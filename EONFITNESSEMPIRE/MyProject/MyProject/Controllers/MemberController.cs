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
    public class MemberController : Controller
    {
        clsFunction objcls = new clsFunction();
        DbGymEntities db = new DbGymEntities();
        public ActionResult RegistrationByMember()
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
        public ActionResult RegistrationByMember(Confirm_MemberRegistration data, HttpPostedFileBase file)
        {
            try
            {
                string path = null;
                string fname = null;
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
                data.ConfirmFlag = "WATING";
                data.ProfilePic = fname;
                DateTime dt = DateTime.Now;
                data.CreatedOn = dt;
                data.CreatedBy = "Admin";//Session["UserName"].ToString();
                db.Confirm_MemberRegistration.Add(data);
                db.SaveChanges();
                ViewBag.verification = "Registration Done. Please Contact to administrator!!!";
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult VerifictionRemainList(int? page)
        {
            try
            {
                return View((objcls.VerifictionRemainList()).ToPagedList(page ?? 1, 10));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult NotConfirm(int id)
        {
            try
            {
                Confirm_MemberRegistration cm = db.Confirm_MemberRegistration.Where(m => m.ID.Equals(id)).FirstOrDefault();
                cm.ConfirmFlag = "NO";
                db.Entry(cm).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Home","Admin");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
