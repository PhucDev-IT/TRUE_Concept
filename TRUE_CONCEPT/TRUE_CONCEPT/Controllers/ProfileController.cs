using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;

namespace TRUE_CONCEPT.Controllers
{
    public class ProfileController : Controller
    {
        private TRUE_CONCEPTEntities db = new TRUE_CONCEPTEntities();
        // GET: Profile
        public ActionResult Index()
        {
            User u = Session["UserCurrent"] as User;
            if(u != null)
            {
                return View(u);
            }
            return RedirectToAction("Index", "Login", new { area = "Authentication" });
        }

        [HttpPost]
        public ActionResult Update(User user)
        {
            try
            {
                User u = db.Users.Find(user.IDCustomer);
                if (u != null)
                {
                    u.FullName = user.FullName;
                    u.NumberPhone = user.NumberPhone;
                    u.Address = user.Address;

                    db.SaveChanges();
                    Session["UserCurrent"] = u;
                    return Json(new { success = true });
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error Add: " + e.ToString());
            }
            return Json(new { success = false });
        }
    }
}