using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingCartMVC.Models;

namespace ShoppingCartMVC.Controllers
{
    public class SignController : Controller
    {
        ShoppingCartEntities db = new ShoppingCartEntities();

        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string Account, string Password)
        {

            Member member = db.Member.Where(m => m.M_Password == Password && m.M_Account == Account).FirstOrDefault();
            if (member == null)
            {
                ViewBag.Mes = "Error";
                return View();

            }
            Session["Member"] = member.M_num;

            return RedirectToAction("Index", "Index");
        }
        // GET: Members/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Members/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "M_num,M_Name,M_Account,M_Password,M_Tel,M_Gender,M_Address")] Member member)
        {
            var Member = db.Member.Where(m => m.M_Account == member.M_Account).FirstOrDefault();
            if(Member!=null)
            {
                ViewBag.Mes = "Repeat";
                return View();
            }
            
            Random rd = new Random();
            if (ModelState.IsValid)
            {
                member.M_num =rd.Next(20,1245);
                db.Member.Add(member);
                db.SaveChanges();
                return RedirectToAction("SignIn", "Sign");
            }

            return View();

        }

    }

}