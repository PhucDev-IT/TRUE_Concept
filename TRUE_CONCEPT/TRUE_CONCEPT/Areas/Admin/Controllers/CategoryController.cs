using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;

namespace TRUE_CONCEPT.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private TRUE_CONCEPTEntities db;
        
        public CategoryController()
        {
            db = DbMangager.GetInstance();
        }
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Add()
        {  
            return View();
        }
        [HttpPost]
        public ActionResult Add(Category model)
        {
            try
            {
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Add: " + e.ToString());
                return View();
            }
        }
        public ActionResult Update(int? id)
        {
          
            if (id != null)
            {
                Category obj = db.Categories.Find(id);
                return View(obj);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Update(Category category)
        {
            try
            {
                Category obj = db.Categories.Find(category.IDCategory);
                obj.NameCategory = category.NameCategory ;
                //Thêm ảnh
               
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
                Category obj = db.Categories.Find(id);
                return View(obj);
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            try
            {
                Category obj = db.Categories.Find(category.IDCategory);
                db.Categories.Remove(obj);
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