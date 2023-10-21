using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;
namespace TRUE_CONCEPT.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private TRUE_CONCEPTEntities db;

        /// <summary>
        /// Mục tiêu: Để lưu trữ tránh việc phải truy vấn kết nối CSDL quá nhiều gây chậm chương trình
        /// Sẽ dùng để hiển thị trong combox chọn danh mục của sản phẩm
        /// </summary>
        private List<Category> listCategory;

        public ProductController()
        {
            db = DbMangager.GetInstance();
        }

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

      
        public ActionResult Add()
        {
            if (listCategory == null)
            {
                listCategory = db.Categories.ToList();
            }
            ViewData["IDCategory"] = new SelectList(listCategory, "IDCategory", "NameCategory");
            return View();
        }
        [HttpPost]
        public ActionResult Add(Product model)
        {
            try
            {
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }catch(Exception e)
            {
                Console.WriteLine("Error Add: " + e.ToString());
                return View();
            }
        }

        public ActionResult Update(int? id)
        {
            if (listCategory == null)
            {
                listCategory = db.Categories.ToList();
            }
            ViewData["IDCategory"] = new SelectList(listCategory, "IDCategory", "NameCategory");
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
            }catch(Exception e)
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



    }
}