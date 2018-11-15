using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult एक_परिचय()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult लक्ष्य_और_उदेश्य()
        {
            return View();
        }

        public ActionResult संविधान()
        {
            return View();
        }

        public ActionResult संयोजक()
        {
            return View();
        }

        public ActionResult यात्रा()
        {
            return View();
        }

        public ActionResult गैलरी()
        {
            return View();
        }

        public ActionResult मीडिया()
        {
            return View();
        }
        public ActionResult संपर्क()
        {
            ViewBag.Message = "राष्ट्रीय समान अधिकार संपर्क";
            return View();
        }
    }
}