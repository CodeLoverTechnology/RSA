using RSA.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSA.Controllers
{
    public class HomeController : Controller
    {
        private RSA_YatraDBEntities db = new RSA_YatraDBEntities();
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
            var NewsList = db.T_News_Masters.OrderByDescending(x => x.NewsID).Take(25);
            return View(NewsList);
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

        public ActionResult NewsDetails(int? id)
        {
            int NewsID = Convert.ToInt32(id);
            T_News_Masters t_News_Masters = db.T_News_Masters.Where(x=>x.NewsID == NewsID).FirstOrDefault();
            return View(t_News_Masters);
        }
    }
}