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
                    string hashingPassword = SHA256HashingPassword.GetSHA256Hash(a.Password);
                    Account result = db.Accounts.FirstOrDefault(x => x.UserName == a.UserName && x.Password == hashingPassword);
                    if (result != null)
                    {
                        if (result.Decentralization == "Client")
                        {
                            Session["AccountUserCurrent"] = result;
                            return RedirectToAction("Index", "TrangChu", new { area = "" });

                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(result.ID.ToString(), false);
                            if (ReturnUrl == null || ReturnUrl == "")
                            {
                                return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                            }
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