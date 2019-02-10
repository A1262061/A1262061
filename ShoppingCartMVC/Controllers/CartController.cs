using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCartMVC.Models;
namespace ShoppingCartMVC.Controllers
{
    
    public class CartController : Controller
    {
        ShoppingCartEntities db = new ShoppingCartEntities();
        // GET: Cart
        public ActionResult Cart()
        {
            string MId=Session["Member"].ToString();
            var Cart = db.Cart.Where(m => m.M_num == MId).ToList();
            
            return View("Cart", "_LayoutMember", Cart);
        }
        [HttpPost]
        public ActionResult Add(string P_num,string P_Style)
        {
            if(Session["Member"]==null)
            {
                return RedirectToAction("SignIn", "Sign");
            }
            int p_num = int.Parse(P_num);
            string MId = Session["Member"].ToString();
            var Product = db.Product.Where(m=>m.P_num == p_num).FirstOrDefault();
            var Cart = db.Cart.Where(m => m.M_num == MId && m.C_num == P_num && m.P_style==P_Style).FirstOrDefault();
            if(Cart==null)
            {
                Cart CartList = new Cart();
                CartList.Amount = 1;
                CartList.M_num = MId;
                CartList.P_price = Product.P_Price;
                CartList.C_num = Product.P_num.ToString();
                CartList.P_style = P_Style;
                CartList.P_name = Product.P_Name;
                db.Cart.Add(CartList);
            }
            else
            {
                Cart.Amount += 1;
            }
            db.SaveChanges();
            return RedirectToAction("ProductPage", "ProductPage", new { Id =Product.P_num });
        }
        public ActionResult Remove(string Id)
        {
            string MId = Session["Member"].ToString();
            var Cart = db.Cart.Where(m => m.M_num == MId && m.C_num == Id).FirstOrDefault();
            db.Cart.Remove(Cart);
            db.SaveChanges();


            return RedirectToAction("Cart", "Cart");

        }
    }
}