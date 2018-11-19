using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp1.Controllers {
    /// <summary>
    /// home-控制器
    /// </summary>
    //[RoutePrefix("Home")]
    [Route("{action=Index}")]
    public class HomeController : Controller {
        /// <summary>Log</summary>
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //[Route("~/")]
        [Route]                 // GET: localhost:63211
        [Route("Index")]        // GET: /Index
        [Route("Home/Index")]   // GET: /Home/Index
        public ActionResult Index() {
            logger.Info("我是 Index() action");
            return View();
        }

        // GET: /About
        [Route("About")]
        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        
        // GET: /Contact
        [Route("Contact")]
        [Route("aasddff")]
        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}