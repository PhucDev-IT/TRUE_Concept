using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;

namespace TRUE_CONCEPT.Areas.Client.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly int pageSize = 20; // Số lượng sản phẩm trên mỗi trang
        private TRUE_CONCEPTEntities db;
        private List<Category> categories = new List<Category>();
        public ProductsController()
        {
            db = DbMangager.GetInstance();
        }

        public void getListCategory()
        {
            if (categories.Count <=0)
            {
                var productCountsByCategory = from c in db.Categories
                                              join p in db.Products on c.IDCategory equals p.IDCategory into groupedProducts
                                              select new
                                              {
                                                  ID = c.IDCategory,
                                                  CategoryName = c.NameCategory,
                                                  ProductCount = groupedProducts.Count()
                                              };
                // Chuyển đổi kết quả truy vấn vào danh sách categories
                foreach (var item in productCountsByCategory)
                {
                    categories.Add(new Category
                    {
                        IDCategory = item.ID,
                        NameCategory = item.CategoryName,
                        QuantityProduct = item.ProductCount
                    });
                }

            }

        }
        // GET: Client/Products
        public ActionResult Index(int? idCategory, int? page)
        {
            getListCategory();
            int pageNumber = (page ?? 1); // Số trang hiện tại, mặc định là 1 nếu không có giá trị page

            List<Product> products = new List<Product>();
            if (idCategory == null)
            {
                products = db.Products
                                 .Where(x => x.Status == "ON")
                                 .OrderBy(p => p.ID)
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();

            }
            else
            {
                products = db.Products.Where(x => x.IDCategory == idCategory && x.Status == "ON").Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.Categories = categories;
            return View(products);
        }
    }
}