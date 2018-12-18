using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using RSAWebApplication.DBModels;

namespace RSAWebApplication.Controllers
{
    public class NewsCategoryMasterController : Controller
    {
        private RSADBEntities db = new RSADBEntities();

        // GET: NewsCategoryMaster
        public async Task<ActionResult> Index()
        {
            return View(await db.M_NewsCategoryMaster.ToListAsync());
        }

        // GET: NewsCategoryMaster/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_NewsCategoryMaster m_NewsCategoryMaster = await db.M_NewsCategoryMaster.FindAsync(id);
            if (m_NewsCategoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_NewsCategoryMaster);
        }

        // GET: NewsCategoryMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsCategoryMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NewsCategoryID,CategoryName,Description,Notes")] M_NewsCategoryMaster m_NewsCategoryMaster)
        {
            if (ModelState.IsValid)
            {
                m_NewsCategoryMaster.CreatedBy = Session["LoginUser"].ToString();
                m_NewsCategoryMaster.CreatedDate = DateTime.Now;
                m_NewsCategoryMaster.ModifiedBy = Session["LoginUser"].ToString();
                m_NewsCategoryMaster.ModifiedDate = DateTime.Now;
                m_NewsCategoryMaster.Active = true;

                db.M_NewsCategoryMaster.Add(m_NewsCategoryMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(m_NewsCategoryMaster);
        }

        // GET: NewsCategoryMaster/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_NewsCategoryMaster m_NewsCategoryMaster = await db.M_NewsCategoryMaster.FindAsync(id);
            if (m_NewsCategoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_NewsCategoryMaster);
        }

        // POST: NewsCategoryMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NewsCategoryID,CategoryName,Description,Notes,CreatedBy,CreatedDate,Active")] M_NewsCategoryMaster m_NewsCategoryMaster)
        {
            if (ModelState.IsValid)
            {
                m_NewsCategoryMaster.ModifiedBy = Session["LoginUser"].ToString();
                m_NewsCategoryMaster.ModifiedDate = DateTime.Now;

                db.Entry(m_NewsCategoryMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(m_NewsCategoryMaster);
        }

        // GET: NewsCategoryMaster/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_NewsCategoryMaster m_NewsCategoryMaster = await db.M_NewsCategoryMaster.FindAsync(id);
            if (m_NewsCategoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(m_NewsCategoryMaster);
        }

        // POST: NewsCategoryMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            M_NewsCategoryMaster m_NewsCategoryMaster = await db.M_NewsCategoryMaster.FindAsync(id);
            db.M_NewsCategoryMaster.Remove(m_NewsCategoryMaster);
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
