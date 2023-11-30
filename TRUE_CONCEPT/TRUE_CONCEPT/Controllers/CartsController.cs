using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;
using TRUE_CONCEPT.Models.ViewModel;

namespace TRUE_CONCEPT.Areas.Client.Controllers
{
    public class CartsController : Controller
    {
        private TRUE_CONCEPTEntities db = new TRUE_CONCEPTEntities();
        // GET: Client/Carts
        public ActionResult Index()
        {
            User a = Session["UserCurrent"] as User;
            Session["orders"] = null;
            if (a == null)
            {
                return RedirectToAction("Index", "Login", new { area = "Admin" });
            }

            List<ItemCartViewModel> itemCarts = new List<ItemCartViewModel>();

            var query = (from c in db.ItemCarts.Where(c => c.ID == a.IDCustomer)
                         join p in db.Products.Where(m => m.Status == "ON")
                         on c.IDProduct equals p.ID
                         join g in db.Categories
                         on p.IDCategory equals g.IDCategory
                         select new
                         {
                             c.IDProduct,
                             p.NameProduct,
                             ConLai = p.Quantity,
                             p.NewPrice,
                             p.Unit,
                             p.ImgDemo,
                             c.Quantity,
                             g.NameCategory
                         });

            if (query.Any())
            {
                 itemCarts = query.ToList()
                            .Select(item => new ItemCartViewModel
                            {
                               product = new Product
                               {
                                   ID = item.IDProduct,
                                   Quantity = (double)item.ConLai,
                                   NameProduct = item.NameProduct,
                                   NewPrice = item.NewPrice,
                                   Unit = item.Unit,
                                   ImgDemo = item.ImgDemo
                               },
                               nameCategory = item.NameCategory,
                               Quantity = (int)item.Quantity
                           }).ToList();
                            }


            return View(itemCarts);
        }

        //Xóa sản phẩm
        [HttpPost]
        public JsonResult Remove(int? id)
        {
            User a = Session["UserCurrent"] as User;

            try
            {
                if (a != null && id != null)
                {
                    ItemCart itemCart = db.ItemCarts.FirstOrDefault(c => c.ID == a.IDCustomer && c.IDProduct == id);
                    db.ItemCarts.Remove(itemCart);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error Add: " + e.ToString());
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult ConvertView(OrderViewModel item)
        {
            Session["orders"] = item;
            return Json(new { success = true });
        }




    }
}