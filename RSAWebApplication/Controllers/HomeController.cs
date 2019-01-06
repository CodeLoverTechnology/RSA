using RSAWebApplication.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RSAWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private RSADBEntities db = new RSADBEntities();
        public ActionResult Index()
        {
            IList<M_BannerMaster> ObjM_BannerMaster = new List<M_BannerMaster>();
            var BannerResult = (from s in db.M_BannerMaster.OrderByDescending(x => x.ModifiedDate) select new {s.BannerHeader, s.BannerImagesPath,s.BannerText }).Take(5);
            foreach(var bannerdetails in BannerResult)
            {
                ObjM_BannerMaster.Add(new M_BannerMaster() {
                    BannerText= bannerdetails.BannerText,
                    BannerHeader= bannerdetails.BannerHeader,
                    BannerImagesPath = bannerdetails.BannerImagesPath
                });
            }
            return View(ObjM_BannerMaster);
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

        public async Task<ActionResult> ShowNews()
        {
            return View(await db.M_DailyNawsMaster.ToListAsync());
        }
        

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            // ViewBag.ReturnUrl = returnUrl;
            return View();
        }



        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection frm )
        {
            string LoginID = frm["LoginID"].ToString();
                string Pwd= frm["Password"].ToString();
            if (Resources.RSAResourcesAdmin.SupUser == LoginID && Resources.RSAResourcesAdmin.Pwd == Pwd)
            {
                Session["LoginUser"] = LoginID;
                return RedirectToAction("AdminIndex", "RSAAdmin", null);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true           
            return View();
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.ContactNo };
        //        var result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

        //            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
        //            // Send an email with this link
        //            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        //            return RedirectToAction("Index", "Home");
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
        
    }
}