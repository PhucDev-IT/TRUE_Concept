using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;
using TRUE_CONCEPT.Models.ViewModel;

namespace TRUE_CONCEPT.Areas.Authentication.Controllers
{
    public class SignUpController : Controller
    {
        private TRUE_CONCEPTEntities db;
        public SignUpController()
        {
            db = DbMangager.GetInstance();
        }
        // GET: Authentication/SignUp
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.ViewModel.SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.RePassword)
                {
                    ModelState.AddModelError("RePassword", "Nhập lại mật khẩu không khớp");
                    return View(model);
                }
                if(db.Accounts.Where(x => x.UserName == model.Email).Count() > 0)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
                    return View(model);

                }
                TempData["SignUpModel"] = model;
                return RedirectToAction("VerifyEmail");
            }

            return View(model);
        }


        public ActionResult VerifyEmail()
        {
            var signUpModel = TempData["SignUpModel"] as SignUpViewModel;
         
            return View(signUpModel);
        }

        [HttpPost]
        public JsonResult SendEmail(string email, string FullName, string VerificationCode)
        {
            try
            {
                if (SendingEmail.SendingVerificationEmail(email, FullName, VerificationCode))
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và gửi về thông báo lỗi
                return Json(new { success = false, errorMessage = ex.Message });
            }

        }

        [HttpPost]
        public JsonResult Register(string FullName, string Email, string Password)
        {
            string hashingPassword = SHA256HashingPassword.GetSHA256Hash(Password);
            Account a = new Account
            {
                UserName = Email,
                Password = hashingPassword
            };
            User u = new User
            {
                FullName = FullName,
                Email = Email
            };

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Accounts.Add(a);
                    db.SaveChanges();

                    u.IDCustomer = a.ID;
                    db.Users.Add(u);
                    db.SaveChanges();

                    transaction.Commit();
                    return Json(true);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine("Error Add: " + ex.ToString());
                    return Json(false);
                }
            }
        }

        public ActionResult Completed()
        {
            return View();
        }

        /// <summary>
        /// Mục đích không cho quay về trang trước, xóa cache
        /// </summary>
        /// <returns></returns>
        public ActionResult BackToLogin()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
            return RedirectToAction("Index","TrangChu", new { area = "Client"});
        }

    }
}