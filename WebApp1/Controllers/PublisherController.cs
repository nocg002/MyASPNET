using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApp1.Models;

namespace WebApp1.Controllers {
    /// <summary>
    /// 出版社-控制器
    /// </summary>
    [RoutePrefix("Publisher")]
    [Route("{action=Index}")]
    public class PublisherController : Controller {
        /// <summary>Log</summary>
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private AAAAAEntities db = new AAAAAEntities();


        // GET: Publisher/Index  (default=12)
        // GET: Publisher/Index/5
        [Route("Index/{page:int=12}")]     
        public ActionResult Index(int page) {
            #region
            //取得頁數
            ViewBag.page = page;

            //取得新的網址: Publishers?page=111
            //取得目前的網址
            var newUrl = RouteTable.Routes.GetVirtualPathForArea(
                Request.RequestContext, //傳入目前的RequestContext
                new RouteValueDictionary(new { page = 111 })
            ).VirtualPath;
            ViewBag.newUrl = newUrl;

            //Controller內取得route value
            var currentController = RouteData.Values["controller"];
            var currentAction = RouteData.Values["action"];
            ViewBag.currentController = currentController;

            return View(db.Publishers.OrderByDescending(x => x.Num).ToList());
            #endregion
        }

        // GET: Publisher/Details/...guid....
        [Route("Details/{id:guid}")]
        public ActionResult Details(Guid? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null) {
                return HttpNotFound();
            }
            return View(publisher);
        }


        
        
        // GET: Publisher/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Publisher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PublisherID,PublisherName,ContactName,Telephone,MobilePhone,Address,Remark,Num")] Publisher publisher) {
            if (ModelState.IsValid) {
                publisher.PublisherID = Guid.NewGuid();
                db.Publishers.Add(publisher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publisher);
        }


        // GET: Publisher/Edit/guid....
        [Route("Edit/{id:guid}")]
        public ActionResult Edit(Guid? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null) {
                return HttpNotFound();
            }
            return View(publisher);
        }

        
        // POST: Publisher/Edit/guid....
        [Route("Edit/{id:guid}")]
        [HttpPost] //[HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid? id, [Bind(Include = "PublisherID,PublisherName,ContactName,Telephone,MobilePhone,Address,Remark,Num")] Publisher publisher) {
            #region
            logger.Trace("edit post =" + id);
            if (ModelState.IsValid) {
                db.Entry(publisher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publisher);
             #endregion
        }

        // GET: Publisher/Delete/5
        public ActionResult Delete(Guid? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null) {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: Publisher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id) {
            Publisher publisher = db.Publishers.Find(id);
            db.Publishers.Remove(publisher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        //為防止過度攻擊，請啟用要綁定的特定屬性
        // POST: Publisher/Edit/guid....
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        // POST: Publisher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    }
}
