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

namespace RSA.Controllers
{
    public class NewsSubCategoryMastersController : Controller
    {
        private RSA_YatraDBEntities db = new RSA_YatraDBEntities();

        // GET: NewsSubCategoryMasters
        public async Task<ActionResult> Index()
        {
            var m_NewsSubCategoryMasters = db.M_NewsSubCategoryMasters.Include(m => m.M_NewsCategoryMaster);
            return View(await m_NewsSubCategoryMasters.ToListAsync());
        }

        // GET: NewsSubCategoryMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_NewsSubCategoryMasters m_NewsSubCategoryMasters = await db.M_NewsSubCategoryMasters.FindAsync(id);
            if (m_NewsSubCategoryMasters == null)
            {
                return HttpNotFound();
            }
            return View(m_NewsSubCategoryMasters);
        }

        // GET: NewsSubCategoryMasters/Create
        public ActionResult Create()
        {
            ViewBag.NewsCategory = new SelectList(db.M_NewsCategoryMaster, "NewsCategoryID", "CategoryName");
            return View();
        }

        // POST: NewsSubCategoryMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NewsSubCategoryID,NewsCategory,SubCategory,Description,Notes")] M_NewsSubCategoryMasters m_NewsSubCategoryMasters)
        {
            if (ModelState.IsValid)
            {
                m_NewsSubCategoryMasters.CreatedBy = Session["LoginUser"].ToString();
                m_NewsSubCategoryMasters.CreatedDate = DateTime.Now;
                m_NewsSubCategoryMasters.ModifiedBy = Session["LoginUser"].ToString();
                m_NewsSubCategoryMasters.ModifiedDate = DateTime.Now;
                m_NewsSubCategoryMasters.Active = true;

                db.M_NewsSubCategoryMasters.Add(m_NewsSubCategoryMasters);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.NewsCategory = new SelectList(db.M_NewsCategoryMaster, "NewsCategoryID", "CategoryName", m_NewsSubCategoryMasters.NewsCategory);
            return View(m_NewsSubCategoryMasters);
        }

        // GET: NewsSubCategoryMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_NewsSubCategoryMasters m_NewsSubCategoryMasters = await db.M_NewsSubCategoryMasters.FindAsync(id);
            if (m_NewsSubCategoryMasters == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewsCategory = new SelectList(db.M_NewsCategoryMaster, "NewsCategoryID", "CategoryName", m_NewsSubCategoryMasters.NewsCategory);
            return View(m_NewsSubCategoryMasters);
        }

        // POST: NewsSubCategoryMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NewsSubCategoryID,NewsCategory,SubCategory,Description,Notes,CreatedBy,CreatedDate,Active")] M_NewsSubCategoryMasters m_NewsSubCategoryMasters)
        {
            if (ModelState.IsValid)
            {
                m_NewsSubCategoryMasters.ModifiedBy = Session["LoginUser"].ToString();
                m_NewsSubCategoryMasters.ModifiedDate = DateTime.Now;

                db.Entry(m_NewsSubCategoryMasters).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.NewsCategory = new SelectList(db.M_NewsCategoryMaster, "NewsCategoryID", "CategoryName", m_NewsSubCategoryMasters.NewsCategory);
            return View(m_NewsSubCategoryMasters);
        }

        // GET: NewsSubCategoryMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_NewsSubCategoryMasters m_NewsSubCategoryMasters = await db.M_NewsSubCategoryMasters.FindAsync(id);
            if (m_NewsSubCategoryMasters == null)
            {
                return HttpNotFound();
            }
            return View(m_NewsSubCategoryMasters);
        }

        // POST: NewsSubCategoryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            M_NewsSubCategoryMasters m_NewsSubCategoryMasters = await db.M_NewsSubCategoryMasters.FindAsync(id);
            db.M_NewsSubCategoryMasters.Remove(m_NewsSubCategoryMasters);
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
