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
                list = db.Products.Where(x => x.NameProduct.Contains(inputSearch) && x.Status == "ON").ToList();
            }
            else
            {
                list = db.Products.Where(x => x.NameProduct.Contains(inputSearch) && x.IDCategory == searchCategoryId && x.Status == "ON").ToList();
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
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
      
                        // Xử lý hình ảnh ImgDemo
                        string imgDemo = addImage("fImage_1");
                        
                        model.ImgDemo = imgDemo != null ? imgDemo : "/Assets/Image/404-error-not-found.png";

                        List<string> images = new List<string>();
                        for (int i = 1;i<= 5; i++)
                        {
                     
                            string name = "fImage_" + i;
                            string result = addImage(name);
                            if(result != null)
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

                        ViewData["Categories"] = new SelectList(db.Categories.ToList(), "IDCategory", "NameCategory");
                        return View();
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Error Add: " + e.ToString());
                    ModelState.AddModelError("", "Có lỗi xảy ra");
                    // Rollback transaction nếu có lỗi xảy ra
                    dbContextTransaction.Rollback();
                }
            }
            ViewData["Categories"] = new SelectList(db.Categories.ToList(), "IDCategory", "NameCategory");
            return View(model);
        }

        [HttpPost]
        private string addImage(string name)
        {
            try
            {
                var f = Request.Files[name];

                if (f != null && f.ContentLength > 0)
                {
                    string fileName = f.FileName;
                    string folderName = Server.MapPath("~/Assets/Uploads/" + fileName);
                    f.SaveAs(folderName);
                    return "/Assets/Uploads/" + fileName;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error Uploads image: " + e.ToString());     
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
                obj.ImgDemo = "/Assets/Image/404-error-not-found.png";

                var fImage = Request.Files["fImage"];
                if (fImage != null && fImage.ContentLength > 0)
                {
                    string fileName = fImage.FileName;
                    string folderName = Server.MapPath("~/Assets/Uploads/" + fileName);
                    fImage.SaveAs(folderName);
                    obj.ImgDemo = "/Assets/Uploads/" + fileName;
                }

                obj.NameProduct = product.NameProduct;
                obj.PriceOld = product.PriceOld;
                obj.NewPrice = product.NewPrice;
                obj.Unit = product.Unit;
                obj.Quantity = product.Quantity;
                obj.Description = product.Description;
          
                obj.Status = product.Status;
                obj.IDCategory = product.IDCategory;
  

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error  update: " + e.ToString());
                return View();
            }
        }

      
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Product obj = db.Products.Find(id);
                obj.Status = "LOCK";
     
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error Remove Product: " + e.ToString());
                return Json(new { success = false });
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

        [HttpPost]
        public ActionResult Upload()
        {
         
            string nameProduct = Request.Form["NameProduct"];
            string priceOld = Request.Form["PriceOld"];
            System.Diagnostics.Debug.WriteLine("Name: " + nameProduct);
            // Lấy danh sách các files ảnh từ FormData
            var files = Request.Files;
            List<string> imagePaths = new List<string>();

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (file.ContentLength > 0)
                {
                    // Xử lý file ở đây, ví dụ lưu vào thư mục hoặc làm các thao tác cần thiết
                    string fileName = file.FileName;
                    string folderName = Server.MapPath("~/Assets/Uploads/" + fileName);
                    file.SaveAs(folderName);
                    imagePaths.Add("/Assets/Uploads/" + fileName); // Lưu đường dẫn của file
                }
            }

            // Sử dụng các giá trị và danh sách file đã lấy được ở đây để thực hiện các thao tác tiếp theo

            return Json(new { success = true });
        }




    }
}