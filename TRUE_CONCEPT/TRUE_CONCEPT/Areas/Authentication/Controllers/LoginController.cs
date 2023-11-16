using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TRUE_CONCEPT.Models;

namespace TRUE_CONCEPT.Areas.Authentication.Controllers
{
    public class LoginController : Controller
    {
        private TRUE_CONCEPTEntities db;
        public LoginController()
        {
            db = DbMangager.GetInstance();
        }

        // GET: Authentication/Login
        // GET: Authencation
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Account a, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Account result = db.Accounts.FirstOrDefault(x => x.UserName == a.UserName && x.Password == a.Password);
                    if (result != null)
                    {
                        FormsAuthentication.SetAuthCookie(result.UserName, false);
                        if (ReturnUrl == null || ReturnUrl == "")
                        {
                            return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                        }
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác");
                    }


                }
                catch
                {

                }

            }
            return View(a);

        }
    }
}