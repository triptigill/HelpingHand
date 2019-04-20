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
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace HelpingHand.Controllers
{
    public class CompanyDetailController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: CompanyDetail
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyDetail.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase postedFile)
        {
            ApplicationDBContext db = new ApplicationDBContext();
            List<CompanyDetail> customers = new List<CompanyDetail>();
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        customers.Add(new CompanyDetail
                        {
                            CompanyId = Guid.NewGuid(),
                            CompanyName = row.Split(',')[0],
                            Address = row.Split(',')[1],
                            City = row.Split(',')[2],
                            PIN = Convert.ToInt32(row.Split(',')[3]),
                            Telephone = row.Split(',')[4],
                            VAT = Convert.ToInt32(row.Split(',')[5])
                        });
                    }
                }
                db.CompanyDetail.AddRange(customers);
                await db.SaveChangesAsync();
            }

            return View("Index");
        }


        // GET: CompanyDetail/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDetail companyDetail = await db.CompanyDetail.FindAsync(id);
            if (companyDetail == null)
            {
                return HttpNotFound();
            }
            return View(companyDetail);
        }

        // GET: CompanyDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyId,CompanyName,Address,City,PIN,Telephone,VAT")] CompanyDetail companyDetail)
        {
            if (ModelState.IsValid)
            {
                companyDetail.CompanyId = Guid.NewGuid();
                db.CompanyDetail.Add(companyDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyDetail);
        }

        // GET: CompanyDetail/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDetail companyDetail = await db.CompanyDetail.FindAsync(id);
            if (companyDetail == null)
            {
                return HttpNotFound();
            }
            return View(companyDetail);
        }

        // POST: CompanyDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyId,CompanyName,Address,City,PIN,Telephone,VAT")] CompanyDetail companyDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyDetail);
        }

        // GET: CompanyDetail/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDetail companyDetail = await db.CompanyDetail.FindAsync(id);
            if (companyDetail == null)
            {
                return HttpNotFound();
            }
            return View(companyDetail);
        }

        // POST: CompanyDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            CompanyDetail companyDetail = await db.CompanyDetail.FindAsync(id);
            db.CompanyDetail.Remove(companyDetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public string GetPatientList(string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            string test = string.Empty;
            sSearch = sSearch.ToLower();
            int totalRecord = db.CompanyDetail.Count();

            var patients = new List<CompanyDetail>();
            if (!string.IsNullOrEmpty(sSearch))
                patients = db.CompanyDetail.Where(a => a.CompanyName.ToLower().Contains(sSearch)
                || a.Address.ToLower().Contains(sSearch)

                || a.Telephone.ToLower().Contains(sSearch)

                || a.City.StartsWith(sSearch)

                ).OrderBy(a => a.CompanyId).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            else
                patients = db.CompanyDetail.OrderBy(a => a.CompanyId).Skip(iDisplayStart).Take(iDisplayLength).ToList();

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
