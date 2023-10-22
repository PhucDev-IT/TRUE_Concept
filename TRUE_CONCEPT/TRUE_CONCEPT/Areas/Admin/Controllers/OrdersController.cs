using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;
namespace TRUE_CONCEPT.Areas.Admin.Controllers
{
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
                        where orders.Status == "Chờ"
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
            Console.WriteLine("Hellllo");
                Order obj = db.Orders.SingleOrDefault(x => x.IDOrder == idOrder);
                obj.Status = "Xác nhận";
                db.SaveChanges();
                return Json(new { success = true});
         
        }

        [HttpPost]
        public ActionResult CancelOrder(int idOrder, int idCustomer)
        {
            db.usp_CancelInvoice(idOrder, idCustomer);
            return Json(new { success = true });
        }
    }
}