using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;
namespace TRUE_CONCEPT.Areas.Client.Controllers
{
    public class Detail_productsController : Controller
    {
        private TRUE_CONCEPTEntities db = new TRUE_CONCEPTEntities();
        // GET: Client/Deatail_products
        public ActionResult Index(int idProduct)
        {
            string userName = Session["User"] as string;
            ViewBag.isCurrentUser = checkCurrentUser();
            return View(db.Products.Find(idProduct));
        }


        [HttpPost]
        public JsonResult AddToCart(ItemCart cart)
        {
            try
            {
                Account a = Session["AccountUserCurrent"] as Account;
                //Kiếm sản phẩm đã có trước đó
                ItemCart cartFind = db.ItemCarts.FirstOrDefault(c => c.ID == a.ID && c.IDProduct == cart.IDProduct);
                if(cartFind != null)
                {
                    cartFind.Quantity += cart.Quantity;
                }
                else
                {
                    cart.ID = a.ID;
                    db.ItemCarts.Add(cart);
                    int count = (int)Session["numberCart"];
                    Session["numberCart"] = count++;
                }

                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception e){
                System.Diagnostics.Debug.WriteLine("Error Add: " + e.ToString());
                return Json(new { success = false });

            }

        }


        private bool checkCurrentUser()
        {
            Account a = Session["AccountUserCurrent"] as Account;
            return a!=null;
        }
    }
}