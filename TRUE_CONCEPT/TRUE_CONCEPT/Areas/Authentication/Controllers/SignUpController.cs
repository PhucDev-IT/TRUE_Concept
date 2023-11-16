using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;

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
        public JsonResult CheckEmail(string userName)
        {
            try
            {
                var result = db.Accounts.FirstOrDefault(x => x.UserName == userName);
                if (result != null)
                {
                    // Địa chỉ email đã tồn tại trong cơ sở dữ liệu
                    return Json(new { success = true });
                }
                else
                {
                    // Địa chỉ email không tồn tại trong cơ sở dữ liệu
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
        public JsonResult VerifyEmail(string email, string FullName, string VerificationCode)
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
        public JsonResult Register(string FullName, string email, string Password)
        {
            Account a = new Account
            {
                UserName = email,
                Password = Password
            };
            User u = new User
            {
                FullName = FullName,
                Email = email
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
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, errorMessage = ex.Message });
                }
            }
        }

    }
}