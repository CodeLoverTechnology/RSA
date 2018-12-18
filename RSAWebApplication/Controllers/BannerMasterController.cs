using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RSAWebApplication.DBModels;
using RSAWebApplication.App_Code;

namespace RSAWebApplication.Controllers
{
    public class BannerMasterController : Controller
    {
        private RSADBEntities db = new RSADBEntities();

        // GET: BannerMaster
        public async Task<ActionResult> Index()
        {
            return View(await db.M_BannerMaster.ToListAsync());
        }

        // GET: BannerMaster/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_BannerMaster m_BannerMaster = await db.M_BannerMaster.FindAsync(id);
            if (m_BannerMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_BannerMaster);
        }

        // GET: BannerMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BannerMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BannerID,BannerHeader,BannerText,BannerImagesPath,Description,BannerSequence")] M_BannerMaster m_BannerMaster)
        {
            string FullPathWithFileName1 = null;
            string FolderPathForImage1 = null;
            string FolderPath = Server.MapPath(Resources.RSAResourcesAdmin.BannerImagesPath) + "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek;

            if (!string.IsNullOrEmpty(Request.Files["BannerImagesPath"].FileName))
            {
                FullPathWithFileName1 = FolderPath + "\\" + Request.Files["BannerImagesPath"].FileName;
                FolderPathForImage1 = "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["BannerImagesPath"].FileName;
            }
            if (RSACommonFunctions.IsFolderExist(FolderPath))
            {
                if (!string.IsNullOrEmpty(Request.Files["BannerImagesPath"].FileName))
                {
                    Request.Files["BannerImagesPath"].SaveAs(FullPathWithFileName1);
                    m_BannerMaster.BannerImagesPath = FolderPathForImage1;
                }
            }
            if (ModelState.IsValid)
            {
                m_BannerMaster.CreatedBy = Session["LoginUser"].ToString();
                m_BannerMaster.CreatedDate = DateTime.Now;
                m_BannerMaster.ModifiedBy = Session["LoginUser"].ToString();
                m_BannerMaster.ModifiedDate = DateTime.Now;
                m_BannerMaster.Active = true;
                db.M_BannerMaster.Add(m_BannerMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(m_BannerMaster);
        }

        // GET: BannerMaster/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_BannerMaster m_BannerMaster = await db.M_BannerMaster.FindAsync(id);
            if (m_BannerMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_BannerMaster);
        }

        // POST: BannerMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BannerID,BannerHeader,BannerText,BannerImagesPath,Description,BannerSequence,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] M_BannerMaster m_BannerMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_BannerMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(m_BannerMaster);
        }

        // GET: BannerMaster/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_BannerMaster m_BannerMaster = await db.M_BannerMaster.FindAsync(id);
            if (m_BannerMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_BannerMaster);
        }

        // POST: BannerMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            M_BannerMaster m_BannerMaster = await db.M_BannerMaster.FindAsync(id);
            db.M_BannerMaster.Remove(m_BannerMaster);
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
