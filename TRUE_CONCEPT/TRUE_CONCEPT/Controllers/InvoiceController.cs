using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;
using TRUE_CONCEPT.Models.ViewModel;

namespace TRUE_CONCEPT.Controllers
{
    public class InvoiceController : Controller
    {
        private TRUE_CONCEPTEntities db = new TRUE_CONCEPTEntities();
        // GET: Client/Orders
        public ActionResult Index()
        {
            if(Session["UserCurrent"] as User == null)
            {
                return RedirectToAction("Index", "Login", new { area = "Admin" });
            }
            OrderViewModel item = (OrderViewModel)Session["orders"];
            TempData["ordersTmp"] = item;
            return View(item);
        }



        //Mua nhiều hàng cùng lúc
        [HttpPost]
        public ActionResult PaymentTheBill()
        {
            OrderViewModel ordersViewModel = (OrderViewModel)TempData["ordersTmp"];

             User u = Session["UserCurrent"] as User;
            if (ordersViewModel == null || u == null || u.FullName == null || u.Address==null || u.NumberPhone == null)
            {
                TempData["ErrorMessage"] = "Thông tin nhận hàng chưa đầy đủ!";
                return RedirectToAction("Index");
            }
            Order order = new Order()
            {
                IDCustomer = u.IDCustomer,
                Status = "Chờ xác nhận",
                FeeShipment = 17000,
                InforShipment = u.Address,
                OrderDate = DateTime.Now,
                ThanhTien = ordersViewModel.Total - 17000
            };

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Orders.Add(order);
                    db.SaveChanges();

                    foreach (ItemCartViewModel item in ordersViewModel.ItemCarts)
                    {
                        db.OrderDetails.Add(new OrderDetail()
                        {
                            IDOrder = order.IDOrder,
                            IDProduct = item.product.ID,
                            Quantity = item.Quantity,
                            Price = item.product.NewPrice,
                            TotalMoney = item.TotalMoney
                        });

                    }
                    db.SaveChanges();

                    transaction.Commit();

                    //Xóa session sau khi mua thành công
                    HttpContext.Session.Remove("orders");

                    // Xóa giỏ hàng sau khi mua thành công
                    foreach (ItemCartViewModel item in ordersViewModel.ItemCarts)
                    {
                        ItemCart obj = db.ItemCarts.FirstOrDefault(c => c.ID == u.IDCustomer && c.IDProduct == item.product.ID);
                        db.ItemCarts.Remove(obj);
                    }
                    db.SaveChanges();

                    return RedirectToAction("Index", "TrangChu",new { area = ""});
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine("Error Add: " + e.ToString());
                    TempData["ErrorMessage"] = "Có lỗi xảy ra";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}