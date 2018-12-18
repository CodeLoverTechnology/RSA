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
using RSA.DBModels;

namespace RSA.Controllers
{
    public class T_News_MastersController : ApiController
    {
        private RSA_YatraDBEntities db = new RSA_YatraDBEntities();

        [HttpGet]
        [Route("api/T_News_Masters/GetT_News_Masters")]
        public IHttpActionResult GetT_News_Masters()
        {
            return Ok(db.T_News_Masters);
        }

        // GET: api/T_News_Masters/5
        [ResponseType(typeof(T_News_Masters))]
        public async Task<IHttpActionResult> GetT_News_Masters(int id)
        {
            T_News_Masters t_News_Masters = await db.T_News_Masters.FindAsync(id);
            if (t_News_Masters == null)
            {
                return NotFound();
            }

            return Ok(t_News_Masters);
        }

        // PUT: api/T_News_Masters/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_News_Masters(int id, T_News_Masters t_News_Masters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_News_Masters.NewsID)
            {
                return BadRequest();
            }

            db.Entry(t_News_Masters).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_News_MastersExists(id))
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

        // POST: api/T_News_Masters
        [ResponseType(typeof(T_News_Masters))]
        public async Task<IHttpActionResult> PostT_News_Masters(T_News_Masters t_News_Masters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_News_Masters.Add(t_News_Masters);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_News_Masters.NewsID }, t_News_Masters);
        }

        // DELETE: api/T_News_Masters/5
        [ResponseType(typeof(T_News_Masters))]
        public async Task<IHttpActionResult> DeleteT_News_Masters(int id)
        {
            T_News_Masters t_News_Masters = await db.T_News_Masters.FindAsync(id);
            if (t_News_Masters == null)
            {
                return NotFound();
            }

            db.T_News_Masters.Remove(t_News_Masters);
            await db.SaveChangesAsync();

            return Ok(t_News_Masters);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_News_MastersExists(int id)
        {
            return db.T_News_Masters.Count(e => e.NewsID == id) > 0;
        }
    }
}