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
    /// <summary>
    /// 作家
    /// </summary>
    [Authorize]
    public class AuthorController : Controller {
        /// <summary>Log</summary>
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private AAAAAEntities db = new AAAAAEntities();

        
        // GET: Authors
        public ActionResult Index(int page=1) {
             //取得頁數
            ViewBag.page = page;
            return View(db.Authors.OrderByDescending(x => x.Num).ToList());
        }


        // GET: Authors/Details/5
        public ActionResult Details(Guid? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null) {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorID,AuthorName,Gender,Num")] Author author) {
            if (ModelState.IsValid) {
                author.AuthorID = Guid.NewGuid();
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }


        [Authorize(Roles = "123")] //角色不對的話, 議會導向至登入
        // GET: Authors/Edit/guid....
        [Route("Edit/{id:guid}")]
        public ActionResult Edit(Guid? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null) {
                return HttpNotFound();
            }
            return View(author);
        }


        // POST: Publisher/Edit/guid....
        [Route("Edit/{id:guid}")]
        [HttpPost] //[HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorID,AuthorName,Gender,Num")] Author author) {
            logger.Debug("edit post");

            if (ModelState.IsValid) {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }








        // GET: Authors/Delete/5
        public ActionResult Delete(Guid? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null) {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id) {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    }
}
