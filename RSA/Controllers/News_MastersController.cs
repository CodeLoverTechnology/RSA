using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RSA.DBModels;
using RSA.App_Code;

namespace RSA.Controllers
{
    public class News_MastersController : Controller
    {
        private RSA_YatraDBEntities db = new RSA_YatraDBEntities();

        // GET: News_Masters
        public async Task<ActionResult> Index()
        {
            var t_News_Masters = db.T_News_Masters.Include(t => t.M_NewsSubCategoryMasters).Include(t => t.M_RSAMasters);
            return View(await t_News_Masters.ToListAsync());
        }

        // GET: News_Masters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_News_Masters t_News_Masters = await db.T_News_Masters.FindAsync(id);
            if (t_News_Masters == null)
            {
                return HttpNotFound();
            }
            return View(t_News_Masters);
        }

        // GET: News_Masters/Create
        public ActionResult Create()
        {
            ViewBag.NewsSubCategoryID = new SelectList(db.M_NewsSubCategoryMasters, "NewsSubCategoryID", "SubCategory");
            ViewBag.NewsType = new SelectList(db.M_RSAMasters, "RSAID", "TableValue");
            return View();
        }

        // POST: News_Masters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NewsID,NewsType,NewsSubCategoryID,NewsHeading,SubHeading,Para1,Para2,Para3,Para4,Para5,Img1,Img2,Img3,Img4,Img5,Remarks,Location")] T_News_Masters t_News_Masters)
        {
            string FullPathWithFileName1 = null;
            string FullPathWithFileName2 = null;
            string FullPathWithFileName3 = null;
            string FullPathWithFileName4 = null;
            string FullPathWithFileName5 = null;

            string FolderPathForImage1 = null;
            string FolderPathForImage2 = null;
            string FolderPathForImage3 = null;
            string FolderPathForImage4 = null;
            string FolderPathForImage5 = null;

            string FolderPath = Server.MapPath(Resources.RSAResourcesAdmin.NewsImagesPath) + "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek;

            if (!string.IsNullOrEmpty(Request.Files["Img1"].FileName))
            {
                FullPathWithFileName1 = FolderPath + "\\" + Request.Files["Img1"].FileName;
                FolderPathForImage1 = "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["Img1"].FileName;
            }
            if (!string.IsNullOrEmpty(Request.Files["Img2"].FileName))
            {
                FullPathWithFileName2 = FolderPath + "\\" + Request.Files["Img2"].FileName;
                FolderPathForImage2 = "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["Img2"].FileName;
            }
            if (!string.IsNullOrEmpty(Request.Files["Img3"].FileName))
            {
                FullPathWithFileName3 = FolderPath + "\\" + Request.Files["Img3"].FileName;
                FolderPathForImage3 = "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["Img3"].FileName;
            }
            if (!string.IsNullOrEmpty(Request.Files["Img4"].FileName))
            {
                FullPathWithFileName4 = FolderPath + "\\" + Request.Files["Img4"].FileName;
                FolderPathForImage4 = "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["Img4"].FileName;
            }
            if (!string.IsNullOrEmpty(Request.Files["Img5"].FileName))
            {
                FullPathWithFileName5 = FolderPath + "\\" + Request.Files["Img5"].FileName;
                FolderPathForImage5 = "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["Img5"].FileName;
            }
            if (RSACommonFunctions.IsFolderExist(FolderPath))
            {
                if (!string.IsNullOrEmpty(Request.Files["Img1"].FileName))
                {
                    Request.Files["Img1"].SaveAs(FullPathWithFileName1);
                    t_News_Masters.Img1 = FolderPathForImage1;
                }
                if (!string.IsNullOrEmpty(Request.Files["Img2"].FileName))
                {
                    Request.Files["Img2"].SaveAs(FullPathWithFileName2);
                    t_News_Masters.Img2 = FolderPathForImage2;
                }
                if (!string.IsNullOrEmpty(Request.Files["Img3"].FileName))
                {
                    Request.Files["Img3"].SaveAs(FullPathWithFileName3);
                    t_News_Masters.Img3 = FolderPathForImage3;
                }
                if (!string.IsNullOrEmpty(Request.Files["Img4"].FileName))
                {
                    Request.Files["Img4"].SaveAs(FullPathWithFileName4);
                    t_News_Masters.Img4 = FolderPathForImage4;
                }
                if (!string.IsNullOrEmpty(Request.Files["Img5"].FileName))
                {
                    Request.Files["Img5"].SaveAs(FullPathWithFileName5);
                    t_News_Masters.Img5 = FolderPathForImage5;
                }

            }
            if (ModelState.IsValid)
            {
                t_News_Masters.CreatedBy = Session["LoginUser"].ToString();
                t_News_Masters.CreatedDate = DateTime.Now;
                t_News_Masters.ModifiedBy = Session["LoginUser"].ToString();
                t_News_Masters.ModifiedDate = DateTime.Now;
                t_News_Masters.Active = true;
                db.T_News_Masters.Add(t_News_Masters);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.NewsSubCategoryID = new SelectList(db.M_NewsSubCategoryMasters, "NewsSubCategoryID", "SubCategory", t_News_Masters.NewsSubCategoryID);
            ViewBag.NewsType = new SelectList(db.M_RSAMasters, "RSAID", "TableValue", t_News_Masters.NewsType);
            return View(t_News_Masters);
        }

        // GET: News_Masters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_News_Masters t_News_Masters = await db.T_News_Masters.FindAsync(id);
            if (t_News_Masters == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewsSubCategoryID = new SelectList(db.M_NewsSubCategoryMasters, "NewsSubCategoryID", "SubCategory", t_News_Masters.NewsSubCategoryID);
            ViewBag.NewsType = new SelectList(db.M_RSAMasters, "RSAID", "TableValue", t_News_Masters.NewsType);
            return View(t_News_Masters);
        }

        // POST: News_Masters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NewsID,NewsType,NewsSubCategoryID,NewsHeading,SubHeading,Para1,Para2,Para3,Para4,Para5,Img1,Img2,Img3,Img4,Img5,Remarks,Location,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] T_News_Masters t_News_Masters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_News_Masters).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.NewsSubCategoryID = new SelectList(db.M_NewsSubCategoryMasters, "NewsSubCategoryID", "SubCategory", t_News_Masters.NewsSubCategoryID);
            ViewBag.NewsType = new SelectList(db.M_RSAMasters, "RSAID", "TableValue", t_News_Masters.NewsType);
            return View(t_News_Masters);
        }

        // GET: News_Masters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_News_Masters t_News_Masters = await db.T_News_Masters.FindAsync(id);
            if (t_News_Masters == null)
            {
                return HttpNotFound();
            }
            return View(t_News_Masters);
        }

        // POST: News_Masters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            T_News_Masters t_News_Masters = await db.T_News_Masters.FindAsync(id);
            db.T_News_Masters.Remove(t_News_Masters);
            await db.SaveChangesAsync();
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
