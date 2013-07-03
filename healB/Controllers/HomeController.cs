using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace healB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "This is only a test.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            /*
            ViewBag.SQLSERVER_CONNECTION_STRING = ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"];
            ViewBag.SQLSERVER_URI = ConfigurationManager.AppSettings["SQLSERVER_URI"];
            ViewBag.SQLSERVER_CONNECTION_STRING_ALIAS = ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING_ALIAS"];
             */

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
