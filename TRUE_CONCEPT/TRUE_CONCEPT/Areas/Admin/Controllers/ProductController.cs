using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;
namespace TRUE_CONCEPT.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private TRUE_CONCEPTEntities db;
        private List<ChiTietPhieuNhap> listPhieuNhaps = new List<ChiTietPhieuNhap>();
 

        public ProductController()
        {
            db = DbMangager.GetInstance();

        }

        // GET: Admin/Product
        [HttpGet]
        public async Task<ActionResult> Index(string inputSearch = "", int searchCategoryId = 0)
        {
            List<Product> list;
            if (searchCategoryId == 0)
            {
                list = db.Products.Where(x => x.NameProduct.Contains(inputSearch)).ToList();
            }
            else
            {
                list = db.Products.Where(x => x.NameProduct.Contains(inputSearch) && x.IDCategory == searchCategoryId).ToList();
            }

          
            ViewData["Categories"] = new SelectList(db.Categories.ToList(), "IDCategory", "NameCategory");
            return View(list);
        }


        public ActionResult Add()
        {
            ViewData["Categories"] = new SelectList(db.Categories.ToList(), "IDCategory", "NameCategory");
        
            return View();
        }
        [HttpPost]
        public ActionResult Add(Product model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Img_Url = "/Assets/Image/404-error-not-found.png"; // Đặt giá trị mặc định cho Img_Url
                    var f = Request.Files["fImage"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string fileName = f.FileName;
                        string folderName = Server.MapPath("~/Assets/Uploads/" + fileName);
                        f.SaveAs(folderName);
                        model.Img_Url = "/Assets/Uploads/" + fileName;
                    }

                    db.Products.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error Add: " + e.ToString());
                }
            }
            ViewData["Categories"] = new SelectList(db.Categories.ToList(), "IDCategory", "NameCategory");
            return View(model);
        }

        public ActionResult Update(int? id)
        {
            var categories = db.Categories.ToList();
            ViewData["Categories"] = new SelectList(categories, "IDCategory", "NameCategory");
            if (id != null)
            {
                Product product = db.Products.Find(id);
                return View(product);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Update(Product product)
        {
            try
            {
                Product obj = db.Products.Find(product.ID);
                obj.NameProduct = product.NameProduct;
                obj.PriceOld = product.PriceOld;
                obj.NewPrice = product.NewPrice;
                obj.Unit = product.Unit;
                obj.Quantity = product.Quantity;
                obj.Description = product.Description;
                obj.Img_Url = product.Img_Url;
                obj.Status = product.Status;
                obj.IDCategory = product.IDCategory;
                obj.VAT = product.VAT;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Update: " + e.ToString());
                return View();
            }
        }

        public ActionResult Delete(int id)
        {

            try
            {
                Product product = db.Products.Find(id);
                return View(product);
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Delete(Product product)
        {
            try
            {
                Product obj = db.Products.Find(product.ID);
                db.Products.Remove(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private bool IsImageFile(HttpPostedFileBase file)
        {
            // Kiểm tra định dạng tệp hình ảnh ở đây
            // Ví dụ: kiểm tra phần mở rộng của tệp
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(fileExtension);
        }

       

    }
}