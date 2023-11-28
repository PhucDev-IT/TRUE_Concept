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
            OrderViewModel item = (OrderViewModel)Session["orders"];
            TempData["ordersTmp"] = item;
            return View(item);
        }



        //Mua nhiều hàng cùng lúc
        [HttpPost]
        public ActionResult PaymentTheBill()
        {
            OrderViewModel ordersViewModel = (OrderViewModel)TempData["ordersTmp"];


            Account a = Session["AccountUserCurrent"] as Account;

            User u = Session["UserCurrent"] as User;


            if (a == null || ordersViewModel == null || u == null) return RedirectToAction("Index");

            Order order = new Order()
            {
                IDCustomer = a.ID,
                Status = "Chờ xác nhận",
                FeeShipment = 17000,
                InforShipment = a.User.Address,
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
                            IDProduct = item.IdProduct,
                            Quantity = item.Quantity,
                            Price = item.currentPrice,
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
                        ItemCart obj = db.ItemCarts.FirstOrDefault(c => c.ID == a.ID && c.IDProduct == item.IdProduct);
                        db.ItemCarts.Remove(obj);
                    }
                    db.SaveChanges();

                    return RedirectToAction("Index", "TrangChu",new { area = ""});
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine("Error Add: " + e.ToString());
                    return RedirectToAction("Index");
                }
            }
        }
    }
}