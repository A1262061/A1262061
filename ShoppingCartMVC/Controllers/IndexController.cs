using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCartMVC.Models;
namespace ShoppingCartMVC.Controllers
{
    public class IndexController : Controller
    {
        ShoppingCartEntities db = new ShoppingCartEntities();
        // GET: Index
        public ActionResult Index(string searchString)
        {
            var Product = db.Product.OrderBy(m => Guid.NewGuid()).ToList().Take(6);
            if (!String.IsNullOrEmpty(searchString))
            {
                Product = db.Product.Where(s => s.P_Name.Contains(searchString));
            }
            if (Session["Member"] != null)
            {
                return View("Index", "_LayoutMemberIndex", Product);
            }
            else
            {
                return View("Index", "_LayoutIndex", Product);

            }
        }
        [ChildActionOnly]
        public ActionResult ShowBanner()
        {
            var List = db.Product.OrderBy(m => Guid.NewGuid()).Take(3).ToList();
            return PartialView("_ShowBanner", List);
        }

    }
}