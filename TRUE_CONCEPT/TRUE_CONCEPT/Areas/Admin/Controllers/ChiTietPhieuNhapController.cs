using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;
using TRUE_CONCEPT.Models.ViewModel;

namespace TRUE_CONCEPT.Areas.Admin.Controllers
{
    public class ChiTietPhieuNhapController : Controller
    {
        private TRUE_CONCEPTEntities db = new TRUE_CONCEPTEntities();
        // GET: Admin/ChiTietPhieuNhap
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                ViewBag.MaPhieuNhap = id;
                var query = from ctpn in db.ChiTietPhieuNhaps.Where(c => c.MaPhieuNhap == id)
                            join p in db.Products
                            on ctpn.IDProduct equals p.ID
                            select new
                            {

                                IDProduct = p.ID,
                                NameProduct = p.NameProduct,
                                ImgDemoProduct = p.ImgDemo,
                                Quantity = ctpn.Quantity,
                                Price = ctpn.Price,
                                TotalMoney = ctpn.TONGTIEN
                            };
                List<CTPNViewModel> viewModel = query.ToList().Select(item => new CTPNViewModel
                {

                    IDProduct = item.IDProduct,
                    NameProduct = item.NameProduct,
                    ImgDemoProduct = item.ImgDemoProduct,
                    Quantity = (float)item.Quantity,
                    Price = (double)item.Price,
                    TotalMoney = (double)item.TotalMoney
                }).ToList();
                return View(viewModel);
            }

            return View();
        }

        //Thêm mới sản phẩm vào phiếu nhập
        [HttpGet]
        public ActionResult AddProduct(int? MaPhieuNhap)
        {

            try
            {
                if (MaPhieuNhap != null)
                {
                    ViewBag.MaPhieuNhap = MaPhieuNhap;
                    List<ProductViewModel> productList = db.Products
                                                    .Where(p => p.Status == "ON" &&
                                                  !db.ChiTietPhieuNhaps.Any(ct => ct.MaPhieuNhap == MaPhieuNhap && ct.IDProduct == p.ID))
                                                 .Select(p => new ProductViewModel
                                                 {
                                                     ID = p.ID,
                                                     NameProduct = p.NameProduct,
                                                     Price = p.NewPrice,
                                                     Img_Url = p.ImgDemo
                                                 })
                                                 .ToList();
                    return View(productList);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error Add: " + e.ToString());
            }

            return RedirectToAction("Index", "NhapHang", new { area = "Admin" });
        }

        [HttpPost]
        public ActionResult ThemChiTietPhieuNhap(List<ChiTietPhieuNhap> list)
        {

            try
            {
                foreach (ChiTietPhieuNhap item in list)
                {
                    db.ChiTietPhieuNhaps.Add(item);
                }
                db.SaveChanges();
         
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error Add: " + e.ToString());
            }
            return Json(new { success = false });
        }
    }
}