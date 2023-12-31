﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TRUE_CONCEPT.Models;

namespace TRUE_CONCEPT.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeAdminController : Controller
    {
        private DateTime currentDate = DateTime.Today;
        private TRUE_CONCEPTEntities db;
        public HomeAdminController()
        {
            db = DbMangager.GetInstance();
        }

        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            // Lấy ngày và tháng hiện tại
            int currentDay = DateTime.Now.Day;
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            HomeViewModel viewModel = new HomeViewModel();

            viewModel.QuantityNewOrders = db.Orders.Where(x => x.OrderDate == currentDate).Count();

            viewModel.LatestOrders = db.Orders.OrderByDescending(x => x.OrderDate).Take(7).ToList();

            viewModel.newsProducts = db.Products.OrderByDescending(x => x.CreateAt).Take(4).ToList();

           
            return View(viewModel);
        }


    }
}