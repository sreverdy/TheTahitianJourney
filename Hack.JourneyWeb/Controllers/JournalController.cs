using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hack.JourneyWeb.Database;
using Hack.JourneyWeb.Database.Model;

namespace Hack.JourneyWeb.Controllers
{
    public class JournalController : Controller
    {
        private JourneyContext db = new JourneyContext();

        // GET: Journal
        //public async Task<ActionResult> Index()
        //{
        //    var entries = db.Entries.Include(e => e.User);
        //    return View(await entries.ToListAsync());
        //}

        // GET: Journal/Details/5
        public async Task<ActionResult> Read(Guid id)
        {
            bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            var entries = db.Entries.Include(e => e.User).Where(a => a.UserId == id);
            return View(await entries.ToListAsync());
        }

        // GET: Journal/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Journal/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DateCreated,Comments,Photos,UserId")] Entry entry)
        {
            if (ModelState.IsValid)
            {
                db.Entries.Add(entry);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", entry.UserId);
            return View(entry);
        }

        // GET: Journal/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entry entry = await db.Entries.FindAsync(id);
            if (entry == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", entry.UserId);
            return View(entry);
        }

        // POST: Journal/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DateCreated,Comments,Photos,UserId")] Entry entry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entry).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", entry.UserId);
            return View(entry);
        }

        // GET: Journal/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entry entry = await db.Entries.FindAsync(id);
            if (entry == null)
            {
                return HttpNotFound();
            }
            return View(entry);
        }

        // POST: Journal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Entry entry = await db.Entries.FindAsync(id);
            db.Entries.Remove(entry);
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
