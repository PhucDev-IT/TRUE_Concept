using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;

namespace TRUE_CONCEPT.Areas.Admin.Controllers
{
    [Authorize]
    public class NhapHangController : Controller
    {
        // GET: Admin/NhapHang

        private TRUE_CONCEPTEntities db;

        /// <summary>
        /// Hiển thị danh sách phiếu nhập theo háng
        /// Có chức năng thêm, trong mỗi phiếu sẽ có: sửa, chi tiết, xóa
        /// </summary>
        /// <returns></returns>
        /// 
        public NhapHangController()
        {
            db = DbMangager.GetInstance();

        }

        public ActionResult Index()
        {
            return View(db.BaoCaoNhaps.ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();  
        }
        [HttpPost]
        public ActionResult Add(BaoCaoNhap baoCaoNhap)
        {
            try
            {
                db.BaoCaoNhaps.Add(baoCaoNhap);

                foreach (ChiTietPhieuNhap ctpn in baoCaoNhap.ChiTietPhieuNhaps)
                {
                    ctpn.MaPhieuNhap = baoCaoNhap.MaPhieuNhap;
                    db.ChiTietPhieuNhaps.Add(ctpn);
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return Json(new { success = true });
        }

        public ActionResult Update(int? id)
        {
            if (id == null) return View();

            try
            {
                var obj = db.BaoCaoNhaps.Find(id);
                return View(obj);
            }
            catch {
                return View();
            }
        }

       public ActionResult Details(int? id)
        {
            if (id != null)
            {
                BaoCaoNhap obj = db.BaoCaoNhaps.Find(id);

                var query = from ctpn in db.ChiTietPhieuNhaps
                            where ctpn.MaPhieuNhap == id
                            join product in db.Products
                            on ctpn.IDProduct equals product.ID
                            select new ProductViewModel
                            {
                                ID = ctpn.IDProduct,
                                NameProduct = product.NameProduct,
                                Price = ctpn.Price,
                                Quantity = (double)ctpn.Quantity,
                                Img_Url = product.ImgDemo
                            };
                ViewBag.product = query.ToList();
                return View(obj);
            }
            return View();
        }



    }
}