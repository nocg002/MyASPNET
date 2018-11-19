using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp1.Models;

namespace WebApp1.Controllers {
    public class BookInforsController : Controller {
        private AAAAAEntities db = new AAAAAEntities();

        // GET: BookInfors
        public ActionResult Index(int page = 1) {
            int pageSize = 5;

            var bookInfors = db.BookInfors.Include(b => b.Product).Include(b => b.Publisher);
            return View(bookInfors.ToList().ToPagedList(page, pageSize));
        }

        // GET: BookInfors/Details/5
        public ActionResult Details(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookInfor bookInfor = db.BookInfors.Find(id);
            if (bookInfor == null) {
                return HttpNotFound();
            }
            return View(bookInfor);
        }

        // GET: BookInfors/Create
        public ActionResult Create() {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "CategoryID");
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName");
            return View();
        }

        // POST: BookInfors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,PublisherID,IssueDate,ISBN13,LanguageID,Introduction,AuthorAbout,Catalog,Preface")] BookInfor bookInfor) {
            if (ModelState.IsValid) {
                db.BookInfors.Add(bookInfor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "CategoryID", bookInfor.ProductID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", bookInfor.PublisherID);
            return View(bookInfor);
        }

        // GET: BookInfors/Edit/5
        public ActionResult Edit(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookInfor bookInfor = db.BookInfors.Find(id);
            if (bookInfor == null) {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "CategoryID", bookInfor.ProductID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", bookInfor.PublisherID);
            return View(bookInfor);
        }

        // POST: BookInfors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,PublisherID,IssueDate,ISBN13,LanguageID,Introduction,AuthorAbout,Catalog,Preface")] BookInfor bookInfor) {
            if (ModelState.IsValid) {
                db.Entry(bookInfor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "CategoryID", bookInfor.ProductID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", bookInfor.PublisherID);
            return View(bookInfor);
        }

        // GET: BookInfors/Delete/5
        public ActionResult Delete(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookInfor bookInfor = db.BookInfors.Find(id);
            if (bookInfor == null) {
                return HttpNotFound();
            }
            return View(bookInfor);
        }

        // POST: BookInfors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id) {
            BookInfor bookInfor = db.BookInfors.Find(id);
            db.BookInfors.Remove(bookInfor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
