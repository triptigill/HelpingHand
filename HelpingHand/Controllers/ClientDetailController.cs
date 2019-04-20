using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpingHand.Models;
using System.Text;
using Newtonsoft.Json;

namespace HelpingHand.Controllers
{
    public class ClientDetailController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: ClientDetail
        public async Task<ActionResult> Index()
        {
            return View(await db.ClientDetail.ToListAsync());
        }

        // GET: ClientDetail/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDetail clientDetail = await db.ClientDetail.FindAsync(id);
            if (clientDetail == null)
            {
                return HttpNotFound();
            }
            return View(clientDetail);
        }

        // GET: ClientDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ClientId,ClientName,Address1,Address2,Telephone,Mobile,Email")] ClientDetail clientDetail)
        {
            if (ModelState.IsValid)
            {
                clientDetail.ClientId = Guid.NewGuid();
                db.ClientDetail.Add(clientDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(clientDetail);
        }

        // GET: ClientDetail/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDetail clientDetail = await db.ClientDetail.FindAsync(id);
            if (clientDetail == null)
            {
                return HttpNotFound();
            }
            return View(clientDetail);
        }

        // POST: ClientDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClientId,ClientName,Address1,Address2,Telephone,Mobile,Email")] ClientDetail clientDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(clientDetail);
        }

        // GET: ClientDetail/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDetail clientDetail = await db.ClientDetail.FindAsync(id);
            if (clientDetail == null)
            {
                return HttpNotFound();
            }
            return View(clientDetail);
        }

        // POST: ClientDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            ClientDetail clientDetail = await db.ClientDetail.FindAsync(id);
            db.ClientDetail.Remove(clientDetail);
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
        public string GetPatientList(string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            string test = string.Empty;
            sSearch = sSearch.ToLower();
            int totalRecord = db.ClientDetail.Count();

            var patients = new List<ClientDetail>();
            if (!string.IsNullOrEmpty(sSearch))
                patients = db.ClientDetail.Where(a => a.ClientName.ToLower().Contains(sSearch)
                || a.Address1.ToLower().Contains(sSearch)


                || a.Mobile.StartsWith(sSearch)
                || a.Email.StartsWith(sSearch)


                ).OrderBy(a => a.ClientId).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            else
                patients = db.ClientDetail.OrderBy(a => a.ClientId).Skip(iDisplayStart).Take(iDisplayLength).ToList();

            var result = patients;


            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"sEcho\": ");
            sb.Append(sEcho);
            sb.Append(",");
            sb.Append("\"iTotalRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"iTotalDisplayRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"aaData\": ");
            sb.Append(JsonConvert.SerializeObject(result));
            sb.Append("}");
            return sb.ToString();
        }

    }
}
