using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRUE_CONCEPT.Models;

namespace TRUE_CONCEPT.Areas.Client.Controllers
{
    public class TrangChuController : Controller
    {
        private TRUE_CONCEPTEntities db = new TRUE_CONCEPTEntities();
        // GET: Client/TrangChu
        public ActionResult Index()
        {
         
            return View();
        }
    }
}