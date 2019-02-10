using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCartMVC.Models;

namespace ShoppingCartMVC.Controllers
{
    public class ProductPageController : Controller
    {
        ShoppingCartEntities db = new ShoppingCartEntities();
        // GET: ProductPage
        public ActionResult ProductPage(int? Id)
        {
            var Product = db.Product.Where(m=>m.P_num==Id).ToList();
            if(Session["Member"]==null)
            {
                return View("ProductPage", "_LayoutMember", Product);

               
            }
            return View("ProductPage", "_Layout", Product);
        }
        [ChildActionOnly]
        public ActionResult ImgList(int? ProductId)
        {
            var ImgList=db.Product.Where(m => m.P_num != ProductId).OrderBy(m => Guid.NewGuid()).Take(4).ToList();
            return PartialView("_ProductPageImgList", ImgList);
        }
        [ChildActionOnly]
        public ActionResult Style(int? ProductId)
        {
            
            var Style = db.ProductStyle.Where(m => m.P_num == ProductId).ToList();
            return PartialView("_ProductPageStyle", Style);
          
        }
    }
}