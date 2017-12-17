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
        //
        // GET: /Member/
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
                data.ProfilePic = fname;
                DateTime dt = DateTime.Now;
                data.CreatedOn = dt;
                data.CreatedBy = "Admin";//Session["UserName"].ToString();
                db.Confirm_MemberRegistration.Add(data);
                db.SaveChanges();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
