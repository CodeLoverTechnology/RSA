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
    public class T_News_MastersController : ApiController
    {
        private RSADBEntities db = new RSADBEntities();
        private IList<T_News_Masters> ObjNewsList = new List<T_News_Masters>();
        // GET: api/T_News_Masters
        public IHttpActionResult GetT_News_Masters()
        {
            var Result = db.T_News_Masters;
            foreach (var News in Result)
            {
                ObjNewsList.Add(new T_News_Masters()
                {
                    NewsID = News.NewsID,
                    Active = News.Active,
                    CreatedBy = News.CreatedBy,
                    CreatedDate = News.CreatedDate,
                    Img1 = News.Img1,
                    Img2 = News.Img2,
                    Img3 = News.Img3,
                    Img4 = News.Img4,
                    Img5 = News.Img5,
                    Location = News.Location,
                    ModifiedBy = News.ModifiedBy,
                    ModifiedDate = News.ModifiedDate,
                    NewsHeading = News.NewsHeading,
                    NewsSubCategoryID = News.NewsSubCategoryID,
                    NewsType = News.NewsType,
                    NoOfNewsViews = News.NoOfNewsViews,
                    Para1 = News.Para1,
                    Para2 = News.Para2,
                    Para3 = News.Para3,
                    Para4 = News.Para4,
                    Para5 = News.Para5,
                    Remarks = News.Remarks,
                    SubHeading = News.SubHeading
                });
            }
            return Ok(ObjNewsList);
            //return Ok(await db.T_News_Masters.ToListAsync());
        }

        // GET: api/T_News_Masters/5
        //[ResponseType(typeof(T_News_Masters))]
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