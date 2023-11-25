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
    [Authorize]
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
        public ActionResult Index(string inputSearch = "", int searchCategoryId = 0)
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
        public JsonResult Add(Product model)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        System.Diagnostics.Debug.WriteLine("Values: " + model);
                        // Xử lý hình ảnh ImgDemo ở đây

                        model.ImgDemo = addImage(model.ImgDemo) != null ? addImage("fImage") : "/Assets/Image/404-error-not-found.png";

                        // Xử lý danh sách 5 hình ảnh
                        List<String> images = new List<string>();
                        foreach(string path in model.PreviewImages)
                        {
                            string result = addImage(path);
                            if (result != null)
                            {
                                images.Add(result);
                                
                                System.Diagnostics.Debug.WriteLine("Path: " + result);
                            }
                        }            

                        model.CreateAt = DateTime.Now;
                        db.Products.Add(model);
                        db.SaveChanges();

                        // Thêm các hình ảnh vào bảng IMAGES
                        foreach (string path in images)
                        {
                            db.IMAGES.Add(new IMAGE(model.ID, path));
                        }
                        db.SaveChanges();

                        // Commit transaction
                        dbContextTransaction.Commit();
                        return Json(true);
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Error Add: " + e.ToString());
              
                    // Rollback transaction nếu có lỗi xảy ra
                    dbContextTransaction.Rollback();
                }
            }
            ViewData["Categories"] = new SelectList(db.Categories.ToList(), "IDCategory", "NameCategory");
            return Json(false);
        }

        [HttpPost]
        private string addImage(string name)
        {
            var f = Request.Files[name];
            System.Diagnostics.Debug.WriteLine("F = : " + f);
            if (f != null && f.ContentLength > 0)
            {
                string fileName = DateTime.Now+f.FileName;
                string folderName = Server.MapPath("~/Assets/Uploads/" + fileName);
                f.SaveAs(folderName);
                return "/Assets/Uploads/" + fileName;
            }
            return null;
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
                obj.ImgDemo = product.ImgDemo;
                obj.Status = product.Status;
                obj.IDCategory = product.IDCategory;
  

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