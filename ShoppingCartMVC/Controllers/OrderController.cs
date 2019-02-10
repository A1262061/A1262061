using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingCartMVC.Models;
namespace ShoppingCartMVC.Controllers
{
    public class OrderController : Controller
    {
        ShoppingCartEntities db = new ShoppingCartEntities();
        // GET: Order
        public ActionResult CreateOrder()
        {

            return View();
        }
        public ActionResult OrderList()
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("SignIn", "Sign");
            }
            int UserId = int.Parse(Session["Member"].ToString());
            var OrderList = db.Orders.Where(m => m.M_num == UserId).ToList();
            return View("OrderList", "_LayoutMember",OrderList);

        }
        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Order_Item = db.Orders_Items.Where(m => m.O_num == id).ToList();


            return View("Details", "_LayoutMember", Order_Item);
        }


        [HttpPost]
        public ActionResult CreateOrder(string O_Address,string O_Name,string O_Phone)
        {

            Random random = new Random();
            var UserId = Session["Member"].ToString();
            List<Cart> Cart = db.Cart.Where(m => m.M_num == UserId).ToList();
            var TotalPrice = db.Cart.Where(m => m.M_num == UserId).Select(m => m.P_price).Sum();
            Orders Create_Orders = new Orders();
            Create_Orders.O_Date = DateTime.Now;
            Create_Orders.O_Address = O_Address;
            Create_Orders.O_num = random.Next(5, 788);
            Create_Orders.O_Phone = O_Phone;
            Create_Orders.O_Name = O_Name;
            Create_Orders.M_num = int.Parse(UserId);
            Create_Orders.Price = (int)TotalPrice;
            Create_Orders.Status = "NoReady";
            db.Orders.Add(Create_Orders);

            int Count = 0;
            foreach (var Item in Cart)
            {
                Orders_Items Orders_Detial = new Orders_Items();
                Orders_Detial.Amount = (int)Item.Amount;
                Orders_Detial.OItems_num = Count;
                Orders_Detial.P_name = Item.P_name;
                Orders_Detial.O_num = Create_Orders.O_num;
                Orders_Detial.P_Style = Item.P_style;
                db.Cart.Remove(Item);
                db.Orders_Items.Add(Orders_Detial);
                Count++;

            }

            db.SaveChanges();
            return RedirectToAction("Index", "Index");


        }

    }
}