using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RSAWebApplication.DBModels;

namespace RSAWebApplication.Controllers
{
    public class M_NewsCategoryMasterController : ApiController
    {
        private RSADBEntities db = new RSADBEntities();

        // GET: api/M_NewsCategoryMaster
        public IQueryable<M_NewsCategoryMaster> GetM_NewsCategoryMaster()
        {
            return db.M_NewsCategoryMaster;
        }

        // GET: api/M_NewsCategoryMaster/5
        [ResponseType(typeof(M_NewsCategoryMaster))]
        public async Task<IHttpActionResult> GetM_NewsCategoryMaster(int id)
        {
            M_NewsCategoryMaster m_NewsCategoryMaster = await db.M_NewsCategoryMaster.FindAsync(id);
            if (m_NewsCategoryMaster == null)
            {
                return NotFound();
            }

            return Ok(m_NewsCategoryMaster);
        }

        // PUT: api/M_NewsCategoryMaster/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutM_NewsCategoryMaster(int id, M_NewsCategoryMaster m_NewsCategoryMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != m_NewsCategoryMaster.NewsCategoryID)
            {
                return BadRequest();
            }

            db.Entry(m_NewsCategoryMaster).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!M_NewsCategoryMasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/M_NewsCategoryMaster
        [ResponseType(typeof(M_NewsCategoryMaster))]
        public async Task<IHttpActionResult> PostM_NewsCategoryMaster(M_NewsCategoryMaster m_NewsCategoryMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.M_NewsCategoryMaster.Add(m_NewsCategoryMaster);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = m_NewsCategoryMaster.NewsCategoryID }, m_NewsCategoryMaster);
        }

        // DELETE: api/M_NewsCategoryMaster/5
        [ResponseType(typeof(M_NewsCategoryMaster))]
        public async Task<IHttpActionResult> DeleteM_NewsCategoryMaster(int id)
        {
            M_NewsCategoryMaster m_NewsCategoryMaster = await db.M_NewsCategoryMaster.FindAsync(id);
            if (m_NewsCategoryMaster == null)
            {
                return NotFound();
            }

            db.M_NewsCategoryMaster.Remove(m_NewsCategoryMaster);
            await db.SaveChangesAsync();

            return Ok(m_NewsCategoryMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool M_NewsCategoryMasterExists(int id)
        {
            return db.M_NewsCategoryMaster.Count(e => e.NewsCategoryID == id) > 0;
        }
    }
}