using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingCartMVC.Models;

namespace ShoppingCartMVC.Controllers
{
    public class ProductsController : Controller
    {
        private ShoppingCartEntities db = new ShoppingCartEntities();

        // GET: Products
        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.ProductActivity).Include(p => p.ProductClassify);
            return View("Index", "_LayoutAdmin", product.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("Details", "_LayoutAdmin", product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.PActivity_num = new SelectList(db.ProductActivity, "PActivity_num", "PActivity_Name");
            ViewBag.PClassify_num = new SelectList(db.ProductClassify, "PC_num", "PC_Name");
            return View("Create", "_LayoutAdmin");
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "P_num,P_Name,P_Price,P_Description,P_Dimension,P_Image,P_HasMultipleStyle,PActivity_num,PClassify_num")] Product product, HttpPostedFileBase P_Image)
        {
            var fileName = Path.GetFileName(P_Image.FileName);
            string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), fileName);
            P_Image.SaveAs(path);
            if (ModelState.IsValid)
            {
                product.P_Image=path;
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PActivity_num = new SelectList(db.ProductActivity, "PActivity_num", "PActivity_Name", product.PActivity_num);
            ViewBag.PClassify_num = new SelectList(db.ProductClassify, "PC_num", "PC_Name", product.PClassify_num);
            return View("Create", "_LayoutAdmin", product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.PActivity_num = new SelectList(db.ProductActivity, "PActivity_num", "PActivity_Name", product.PActivity_num);
            ViewBag.PClassify_num = new SelectList(db.ProductClassify, "PC_num", "PC_Name", product.PClassify_num);
            return View("Edit", "_LayoutAdmin", product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "P_num,P_Name,P_Price,P_Description,P_Dimension,P_Image,P_HasMultipleStyle,PActivity_num,PClassify_num")] Product product, HttpPostedFileBase P_Image)
        {
            var fileName = Path.GetFileName(P_Image.FileName);
            string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), fileName);
            P_Image.SaveAs(path);
            if (ModelState.IsValid)
            {
                product.P_Image = path;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PActivity_num = new SelectList(db.ProductActivity, "PActivity_num", "PActivity_Name", product.PActivity_num);
            ViewBag.PClassify_num = new SelectList(db.ProductClassify, "PC_num", "PC_Name", product.PClassify_num);
            return View("Edit", "_LayoutAdmin", product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("Delete", "_LayoutAdmin", product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
