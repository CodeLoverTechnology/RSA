using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RSAWebApplication.DBModels;

namespace RSAWebApplication.Controllers
{
    public class M_NewsSubCategoryMastersController : ApiController
    {
        private RSADBEntities db = new RSADBEntities();

        // GET: api/M_NewsSubCategoryMasters
        public IQueryable<M_NewsSubCategoryMasters> GetM_NewsSubCategoryMasters()
        {
            return db.M_NewsSubCategoryMasters;
        }

        // GET: api/M_NewsSubCategoryMasters/5
        [ResponseType(typeof(M_NewsSubCategoryMasters))]
        public async Task<IHttpActionResult> GetM_NewsSubCategoryMasters(int id)
        {
            M_NewsSubCategoryMasters m_NewsSubCategoryMasters = await db.M_NewsSubCategoryMasters.FindAsync(id);
            if (m_NewsSubCategoryMasters == null)
            {
                return NotFound();
            }

            return Ok(m_NewsSubCategoryMasters);
        }

        // PUT: api/M_NewsSubCategoryMasters/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutM_NewsSubCategoryMasters(int id, M_NewsSubCategoryMasters m_NewsSubCategoryMasters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != m_NewsSubCategoryMasters.NewsSubCategoryID)
            {
                return BadRequest();
            }

            db.Entry(m_NewsSubCategoryMasters).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!M_NewsSubCategoryMastersExists(id))
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

        // POST: api/M_NewsSubCategoryMasters
        [ResponseType(typeof(M_NewsSubCategoryMasters))]
        public async Task<IHttpActionResult> PostM_NewsSubCategoryMasters(M_NewsSubCategoryMasters m_NewsSubCategoryMasters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.M_NewsSubCategoryMasters.Add(m_NewsSubCategoryMasters);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = m_NewsSubCategoryMasters.NewsSubCategoryID }, m_NewsSubCategoryMasters);
        }

        // DELETE: api/M_NewsSubCategoryMasters/5
        [ResponseType(typeof(M_NewsSubCategoryMasters))]
        public async Task<IHttpActionResult> DeleteM_NewsSubCategoryMasters(int id)
        {
            M_NewsSubCategoryMasters m_NewsSubCategoryMasters = await db.M_NewsSubCategoryMasters.FindAsync(id);
            if (m_NewsSubCategoryMasters == null)
            {
                return NotFound();
            }

            db.M_NewsSubCategoryMasters.Remove(m_NewsSubCategoryMasters);
            await db.SaveChangesAsync();

            return Ok(m_NewsSubCategoryMasters);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool M_NewsSubCategoryMastersExists(int id)
        {
            return db.M_NewsSubCategoryMasters.Count(e => e.NewsSubCategoryID == id) > 0;
        }
    }
}