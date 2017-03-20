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
using Hack.JourneyWeb.Database;
using Hack.JourneyWeb.Database.Model;

namespace Hack.JourneyWeb.Controllers
{
    public class BadgesController : ApiController
    {
        private JourneyContext db = new JourneyContext();

        // GET: api/Badges
        public IQueryable<Badge> GetBadges()
        {
            return db.Badges;
        }

        //// GET: api/Badges/5
        //[ResponseType(typeof(Badge))]
        //public async Task<IHttpActionResult> GetBadge(int id)
        //{
        //    Badge badge = await db.Badges.FindAsync(id);
        //    if (badge == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(badge);
        //}

        /// <summary>
        /// Retrieve sum of all user points
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET: api/Badges/8446ac53-da0a-e711-9891-8c89a50b5973
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> GetBadge(Guid id)
        {
            var badge = await db.Badges.Where(w => w.UserId == id).SumAsync(s => s.Points);

            return Ok(badge);
        }

        // PUT: api/Badges/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBadge(int id, Badge badge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != badge.Id)
            {
                return BadRequest();
            }

            db.Entry(badge).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BadgeExists(id))
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

        // POST: api/Badges
        [ResponseType(typeof(Badge))]
        public async Task<IHttpActionResult> PostBadge(Badge badge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Badges.Add(badge);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = badge.Id }, badge);
        }

        // DELETE: api/Badges/5
        [ResponseType(typeof(Badge))]
        public async Task<IHttpActionResult> DeleteBadge(int id)
        {
            Badge badge = await db.Badges.FindAsync(id);
            if (badge == null)
            {
                return NotFound();
            }

            db.Badges.Remove(badge);
            await db.SaveChangesAsync();

            return Ok(badge);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BadgeExists(int id)
        {
            return db.Badges.Count(e => e.Id == id) > 0;
        }
    }
}