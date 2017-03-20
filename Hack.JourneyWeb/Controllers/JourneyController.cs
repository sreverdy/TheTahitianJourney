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
    public class JourneyController : ApiController
    {
        private JourneyContext db = new JourneyContext();

        // GET: api/Journey
        public IQueryable<Entry> GetEntries()
        {
            return db.Entries;
        }

        
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Entry))]
        public async Task<IHttpActionResult> GetEntry(int id)
        {
            var entry = await db.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            return Ok(entry);
        }

        // GET: api/Journey/8446ac53-da0a-e711-9891-8c89a50b5973
        /// <summary>
        /// Liste des entrées du journal par utilisateur
        /// </summary>
        [ResponseType(typeof(List<Entry>))]
        public async Task<IHttpActionResult> GetUserEntry(Guid id)
        {
            var entry = await db.Entries.Where(w => w.UserId == id).ToListAsync();
            if (entry == null)
            {
                return NotFound();
            }

            return Ok(entry);
        }

        // PUT: api/Journey/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEntry(int id, Entry entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entry.Id)
            {
                return BadRequest();
            }

            db.Entry(entry).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
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

        // POST: api/Journey
        [ResponseType(typeof(Entry))]
        public async Task<IHttpActionResult> PostEntry(Entry entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entries.Add(entry);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = entry.Id }, entry);
        }

        // DELETE: api/Journey/5
        [ResponseType(typeof(Entry))]
        public async Task<IHttpActionResult> DeleteEntry(int id)
        {
            Entry entry = await db.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            db.Entries.Remove(entry);
            await db.SaveChangesAsync();

            return Ok(entry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntryExists(int id)
        {
            return db.Entries.Count(e => e.Id == id) > 0;
        }
    }
}