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
            Account a = Session["AccountUserCurrent"] as Account;
            List<ItemCartViewModel> itemCarts = new List<ItemCartViewModel>();

            var query = (from c in db.ItemCarts.Where(c => c.ID == a.ID)
                             join p in db.Products
                             on c.IDProduct equals p.ID
                             join g in db.Categories
                             on p.IDCategory equals g.IDCategory
                             select new
                             {
                                 IdProduct = c.IDProduct,
                                 quantity = c.Quantity,
                                 NameProduct = p.NameProduct,
                                 currentPrice = p.NewPrice,
                                 ImageDemo = p.ImgDemo,
                                 unit = p.Unit,
                                 nameCategory = g.NameCategory
                             });

            itemCarts = query.ToList()
               .Select(item => new ItemCartViewModel
               {
                   IdProduct = item.IdProduct,
                   quantity = (double)item.quantity,
                   NameProduct = item.NameProduct,
                   currentPrice = (double)item.currentPrice,
                   ImageDemo = item.ImageDemo,
                   unit = item.unit,
                   nameCategory = item.nameCategory,
               }).ToList();

            
            return View(itemCarts);
        }

        //Xóa sản phẩm
        [HttpPost]
        public JsonResult Remove(int? id)
        {
            Account a = Session["AccountUserCurrent"] as Account;

            try
            {
                if (a != null && id != null)
                {
                    ItemCart itemCart = db.ItemCarts.FirstOrDefault(c => c.ID == a.ID && c.IDProduct == id);
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


        //Mua nhiều hàng cùng lúc
        [HttpPost]
        public JsonResult PaymentProducts(List<OrderDetail> orderDetails)
        {
            double sum = 0;
            foreach (OrderDetail item in orderDetails)
            {
                sum += (double)item.TotalMoney;
            }
            Account a = Session["AccountUserCurrent"] as Account;

            if (a == null) return Json(new { success = false });

            Order order = new Order()
            {
                IDCustomer = a.ID,
                Status = "Chờ xác nhận",
                ThanhTien = sum
            };

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Orders.Add(order);
                    db.SaveChanges();

                    foreach (OrderDetail orderDetail in orderDetails)
                    {
                        orderDetail.IDOrder = order.IDOrder;
                        db.OrderDetails.Add(orderDetail);
                    }
                    db.SaveChanges();

                    transaction.Commit();

                    // Xóa giỏ hàng sau khi mua thành công
                    foreach (OrderDetail orderDetail in orderDetails)
                    {
                        ItemCart obj = db.ItemCarts.FirstOrDefault(c => c.ID == a.ID && c.IDProduct == orderDetail.IDProduct);
                        db.ItemCarts.Remove(obj);
                    }
                    db.SaveChanges();

                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine("Error Add: " + e.ToString());
                    return Json(new { success = false });
                }
            }
        }


    }
}