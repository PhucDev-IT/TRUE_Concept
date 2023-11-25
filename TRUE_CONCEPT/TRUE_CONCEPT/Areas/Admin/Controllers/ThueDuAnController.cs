using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;

namespace TRUE_CONCEPT.Areas.Admin.Controllers
{
    [Authorize]
    public class ThueDuAnController : Controller
    {
        private TRUE_CONCEPTEntities db;

        public ThueDuAnController()
        {
            db = DbMangager.GetInstance();
        }

        // GET: Admin/ThueDuAn
        public ActionResult Index()
        {
            var query = from thueDuAn in db.ThueDuAns                 
                        join user in db.Users
                        on thueDuAn.IDCustomer equals user.IDCustomer
                        select new 
                        {
                            MaDuAn = thueDuAn.MaDuAn,
                            tenKhachHang = user.FullName,
                            AddressCustomer = thueDuAn.AddressCustomer,
                            NgayThueDuAn = thueDuAn.NgayThueDuAn,
                            NgayDuKienHoanThanh = thueDuAn.NgayDuKienHoanThanh,
                            NganSachDuKien = thueDuAn.NganSachDuKien,
                            YeuCauThietKe = thueDuAn.YeuCauThietKe,
                            KieuThietKe = thueDuAn.KieuThietKe,
                            MoTaThem = thueDuAn.MoTaThem
                        };


            var results = query.ToList()
                 .Select(result => new ThueDuAn
                 {
                     MaDuAn = result.MaDuAn,
                     nameCustomer = result.tenKhachHang,
                     AddressCustomer = result.AddressCustomer,
                     NgayThueDuAn = result.NgayThueDuAn,
                     NgayDuKienHoanThanh = result.NgayDuKienHoanThanh,
                     NganSachDuKien = result.NganSachDuKien,
                     YeuCauThietKe = result.YeuCauThietKe,
                     KieuThietKe = result.KieuThietKe,
                     MoTaThem = result.MoTaThem
                     
                 })
                 .ToList();

            
            return View(results);
        }

        public ActionResult Add()
        {
            return View();
        }

    }
}