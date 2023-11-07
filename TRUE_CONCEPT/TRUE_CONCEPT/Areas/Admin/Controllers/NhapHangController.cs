using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;

namespace TRUE_CONCEPT.Areas.Admin.Controllers
{
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

        public ActionResult Add()
        {
            return View();  
        }
        [HttpPost]
        public ActionResult Add(BaoCaoNhap baoCaoNhap)
        {
            System.Diagnostics.Debug.WriteLine(baoCaoNhap);
            foreach (ChiTietPhieuNhap item in baoCaoNhap.ChiTietPhieuNhaps)
            {
                System.Diagnostics.Debug.WriteLine(item.Price);
            }
            return Json(new { success = true });
        }


    }
}