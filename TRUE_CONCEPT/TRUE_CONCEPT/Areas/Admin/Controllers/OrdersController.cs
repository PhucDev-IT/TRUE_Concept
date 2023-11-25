using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;
namespace TRUE_CONCEPT.Areas.Admin.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private TRUE_CONCEPTEntities db;
        public OrdersController()
        {
            db = DbMangager.GetInstance();
        }
        // GET: Admin/Oders
        public ActionResult ChoXacNhan()
        {
            var query = from orders in db.Orders
                        where orders.Status == "Chờ xác nhận"
                        orderby orders.OrderDate ascending
                        join user in db.Users
                        on orders.IDCustomer equals user.IDCustomer
                        select new
                        {
                            IDOrder = orders.IDOrder,
                            IDCustomer = orders.IDCustomer,
                            nameCustomer = user.FullName,
                            OrderDate = orders.OrderDate,
                            thanhTien = orders.ThanhTien
                        };

            var results = query.ToList()
                .Select(result => new Order
                {
                    IDOrder = result.IDOrder,
                    IDCustomer = result.IDCustomer,
                    nameCustomer = result.nameCustomer,
                    OrderDate = result.OrderDate,
                    ThanhTien = result.thanhTien
                }).ToList();

            return View(results);
        }

    
        [HttpPost]
        public ActionResult ConfirmOrder(int? idOrder)
        {
        
                Order obj = db.Orders.SingleOrDefault(x => x.IDOrder == idOrder);
                obj.Status = "Xác nhận";
                db.SaveChanges();
                return Json(new { success = true});
         
        }

        [HttpPost]
        public ActionResult CancelOrder(int? idOrder, int? idCustomer)
        {
            if(idOrder!=null && idCustomer != null)
            {
                db.usp_CancelInvoice(idOrder, idCustomer);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult Details(int? idOrder)
        {
            if (idOrder != null)
            {
                try
                {
                    Order order = db.Orders.Find(idOrder);

                    var query = from o in db.OrderDetails
                                where o.IDOrder == idOrder
                                join p in db.Products
                                on o.IDProduct equals p.ID
                                select new ProductViewModel
                                {
                                    ID = o.IDProduct,
                                    NameProduct = p.NameProduct,
                                    Price = o.Price,
                                    Img_Url = p.ImgDemo,
                                    Quantity = (double)o.Quantity,
                                    TotalMoney = (double)o.TotalMoney
                                };

                    ViewBag.ProductList = query.ToList();
                    // System.Diagnostics.Debug.WriteLine("Size: "+ orderDetails.Count);
                    return View(order);
                }
                catch { }
            }
            return View();
            
        }
       
    }
}