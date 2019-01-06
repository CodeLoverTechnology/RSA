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
    public class DailyNawsMasterController : Controller
    {
        private RSADBEntities db = new RSADBEntities();

        // GET: DailyNawsMaster
        public async Task<ActionResult> Index()
        {
            return View(await db.M_DailyNawsMaster.ToListAsync());
        }

        // GET: DailyNawsMaster/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_DailyNawsMaster m_DailyNawsMaster = await db.M_DailyNawsMaster.FindAsync(id);
            if (m_DailyNawsMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_DailyNawsMaster);
        }

        // GET: DailyNawsMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DailyNawsMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DailyNewsID,Title,FileName,Remarks")] M_DailyNawsMaster m_DailyNawsMaster)
        {
            if (ModelState.IsValid)
            {
                string FullPathWithFileName1 = null;
                string FolderPathForImage1 = null;
                string FolderPath = Server.MapPath(Resources.RSAResourcesAdmin.BannerImagesPath) + "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek;

                if (!string.IsNullOrEmpty(Request.Files["txtFileName"].FileName))
                {
                    FullPathWithFileName1 = FolderPath + "\\" + Request.Files["txtFileName"].FileName;
                    FolderPathForImage1 = "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["txtFileName"].FileName;
                }
                if (RSACommonFunctions.IsFolderExist(FolderPath))
                {
                    if (!string.IsNullOrEmpty(Request.Files["txtFileName"].FileName))
                    {
                        Request.Files["txtFileName"].SaveAs(FullPathWithFileName1);
                        m_DailyNawsMaster.FileName = FolderPathForImage1;
                    }
                }

                m_DailyNawsMaster.CreatedBy = Session["LoginUser"].ToString();
                m_DailyNawsMaster.CreatedDate = DateTime.Now;
                m_DailyNawsMaster.ModifiedBy = Session["LoginUser"].ToString();
                m_DailyNawsMaster.ModifiedDate = DateTime.Now;
                m_DailyNawsMaster.Active = true;
                db.M_DailyNawsMaster.Add(m_DailyNawsMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(m_DailyNawsMaster);
        }

        // GET: DailyNawsMaster/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_DailyNawsMaster m_DailyNawsMaster = await db.M_DailyNawsMaster.FindAsync(id);
            if (m_DailyNawsMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_DailyNawsMaster);
        }

        // POST: DailyNawsMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DailyNewsID,Title,FileName,Remarks,CreatedBy,CreatedDate,Active")] M_DailyNawsMaster m_DailyNawsMaster)
        {
            if (ModelState.IsValid)
            {
                string FullPathWithFileName1 = null;
                string FolderPathForImage1 = null;
                string FolderPath = Server.MapPath(Resources.RSAResourcesAdmin.BannerImagesPath) + "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek;

                if (!string.IsNullOrEmpty(Request.Files["txtFileName"].FileName))
                {
                    FullPathWithFileName1 = FolderPath + "\\" + Request.Files["txtFileName"].FileName;
                    FolderPathForImage1 = "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["txtFileName"].FileName;
                }
                if (RSACommonFunctions.IsFolderExist(FolderPath))
                {
                    if (!string.IsNullOrEmpty(Request.Files["txtFileName"].FileName))
                    {
                        Request.Files["txtFileName"].SaveAs(FullPathWithFileName1);
                        m_DailyNawsMaster.FileName = FolderPathForImage1;
                    }
                }

                m_DailyNawsMaster.ModifiedBy = Session["LoginUser"].ToString();
                m_DailyNawsMaster.ModifiedDate = DateTime.Now;
                db.Entry(m_DailyNawsMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(m_DailyNawsMaster);
        }

        // GET: DailyNawsMaster/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_DailyNawsMaster m_DailyNawsMaster = await db.M_DailyNawsMaster.FindAsync(id);
            if (m_DailyNawsMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_DailyNawsMaster);
        }

        // POST: DailyNawsMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            M_DailyNawsMaster m_DailyNawsMaster = await db.M_DailyNawsMaster.FindAsync(id);
            db.M_DailyNawsMaster.Remove(m_DailyNawsMaster);
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
