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

namespace RSAWebApplication.Controllers
{
    public class M_RSAMastersController : Controller
    {
        private RSADBEntities db = new RSADBEntities();

        // GET: M_RSAMasters
        public async Task<ActionResult> Index()
        {
            return View(await db.M_RSAMasters.ToListAsync());
        }

        // GET: M_RSAMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_RSAMasters m_RSAMasters = await db.M_RSAMasters.FindAsync(id);
            if (m_RSAMasters == null)
            {
                return HttpNotFound();
            }
            return View(m_RSAMasters);
        }

        // GET: M_RSAMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: M_RSAMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RSAID,TableValue,TableName,Sequence")] M_RSAMasters m_RSAMasters)
        {
            if (ModelState.IsValid)
            {
                m_RSAMasters.CreatedBy = Session["LoginUser"].ToString();
                m_RSAMasters.CreatedDate = DateTime.Now;
                m_RSAMasters.ModifiedBy = Session["LoginUser"].ToString();
                m_RSAMasters.ModifiedDate = DateTime.Now;
                m_RSAMasters.Active = true;

                db.M_RSAMasters.Add(m_RSAMasters);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(m_RSAMasters);
        }

        // GET: M_RSAMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_RSAMasters m_RSAMasters = await db.M_RSAMasters.FindAsync(id);
            if (m_RSAMasters == null)
            {
                return HttpNotFound();
            }
            return View(m_RSAMasters);
        }

        // POST: M_RSAMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RSAID,TableValue,TableName,Sequence,CreatedBy,CreatedDate,Active")] M_RSAMasters m_RSAMasters)
        {
            if (ModelState.IsValid)
            {
                m_RSAMasters.ModifiedBy = Session["LoginUser"].ToString();
                m_RSAMasters.ModifiedDate = DateTime.Now;
                db.Entry(m_RSAMasters).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(m_RSAMasters);
        }

        // GET: M_RSAMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_RSAMasters m_RSAMasters = await db.M_RSAMasters.FindAsync(id);
            if (m_RSAMasters == null)
            {
                return HttpNotFound();
            }
            return View(m_RSAMasters);
        }

        // POST: M_RSAMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            M_RSAMasters m_RSAMasters = await db.M_RSAMasters.FindAsync(id);
            db.M_RSAMasters.Remove(m_RSAMasters);
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
