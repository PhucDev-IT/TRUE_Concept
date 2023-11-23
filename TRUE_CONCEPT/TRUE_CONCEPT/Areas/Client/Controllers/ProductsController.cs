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
        private TRUE_CONCEPTEntities db = new TRUE_CONCEPTEntities();
        private List<Category> categories = new List<Category>();
 


        public void getListCategory()
        {
            if (categories.Count <= 0)
            {
                var productCountsByCategory = from c in db.Categories
                                              join p in db.Products.Where(p => p.Status == "ON")
                                              on c.IDCategory equals p.IDCategory into groupedProducts
                                              from gp in groupedProducts.DefaultIfEmpty()
                                              group gp by new { c.IDCategory, c.NameCategory } into grouped
                                              select new
                                              {
                                                  ID = grouped.Key.IDCategory,
                                                  CategoryName = grouped.Key.NameCategory,
                                                  ProductCount = grouped.Count(p => p != null)
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
        /// <summary>
        /// Case 1: Cate - null
        /// case 2: cate - page
        /// case 3: null - null
        /// case 4: null - page
        /// </summary>
        /// <param name="idCategory"></param>
        /// <param name="page"></param>
        /// <returns></returns>

        public ActionResult Index(int? idCategory, int? page)
        {
            getListCategory();
            
            int pageNumber = (page ?? 1); // Số trang hiện tại, mặc định là 1 nếu không có giá trị page

            List<Product> products = new List<Product>();
            int sumSize = 0;


            if (idCategory == null)
            {
                    products = db.Products
                                     .Where(x => x.Status == "ON")
                                     .OrderBy(p => p.ID)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();
                    sumSize = db.Products.Where(x => x.Status == "ON").Count();
            }
            else
            {
         
                products = db.Products.Where(x => x.IDCategory == idCategory && x.Status == "ON")
                     .OrderBy(p => p.ID)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
                sumSize = db.Products.Where(x => x.IDCategory == idCategory && x.Status == "ON").Count();
            }
            System.Diagnostics.Debug.WriteLine("So luong: " + products.Count);

            //Chia số trang

            ViewBag.TotalPage = 400;//(sumSize / pageSize); ;
            ViewBag.Categories = categories;

            ViewBag.currentPage = page??1;
            ViewBag.idCategory = idCategory;
            return View(products);
        }

   

    }
}