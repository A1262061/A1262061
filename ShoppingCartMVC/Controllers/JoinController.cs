using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCartMVC.Models;

namespace ShoppingCartMVC.Controllers
{
    public class JoinController : Controller
    {
        ShoppingCartEntities db = new ShoppingCartEntities();
        // GET: Join
        public ActionResult Index()
        {
         

            var join = (from Orders in db.Orders
                       join item in db.Orders_Items
                       on Orders.O_num equals item.O_num
                       select new Join { O_num=Orders.O_num,Amount=item.Amount,O_Name=Orders.O_Name}).ToList();
        
            return View(join);
        }
    }
}