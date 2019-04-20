using HelpingHand.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HelpingHand.Controllers
{
    public class ProductDetailController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: ProductDetail
        public async Task<ActionResult> Index()
        {
            return View(await db.ProductDetail.ToListAsync());
        }

        // GET: ProductDetail/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = await db.ProductDetail.FindAsync(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);
        }

        // GET: ProductDetail/Create
        public ActionResult Create()
        {
            ViewBag.BookTypes = db.ProductDetail.Select(x => x.Booktype).Distinct().ToList().Select(a => new SelectListItem() { Value = a.ToString(), Text = a.ToString() }).ToList();
            return View();
        }

        public JsonResult GetGroups(string bookType)
        {
            var Grouplist = db.ProductDetail.Where(x => x.Booktype == bookType).Select(x => x.Group).Distinct().ToList();
            var result = Grouplist.Select(a => new SelectListItem() { Value = a.ToString(), Text = a.ToString() }).ToList();
            return Json(Grouplist, JsonRequestBehavior.AllowGet);
        }

        // POST: ProductDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,Title,BookSubType,Booktype,Group,Writer,ProductCode,Price,Description")] ProductDetail productDetail)
        {


            if (ModelState.IsValid)
            {
                productDetail.ProductId = Guid.NewGuid();
                db.ProductDetail.Add(productDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productDetail);
        }

        // GET: ProductDetail/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = await db.ProductDetail.FindAsync(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);
        }

        // POST: ProductDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,Title,BookSubType,Booktype,Group,Writer,Barcode")] ProductDetail productDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productDetail);
        }

        // GET: ProductDetail/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = await db.ProductDetail.FindAsync(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);
        }

        // POST: ProductDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            ProductDetail productDetail = await db.ProductDetail.FindAsync(id);
            db.ProductDetail.Remove(productDetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public string GetPatientList(string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            string test = string.Empty;
            sSearch = sSearch.ToLower();
            int totalRecord = db.ProductDetail.Count();

            var patients = new List<ProductDetail>();
            if (!string.IsNullOrEmpty(sSearch))
                patients = db.ProductDetail.Where(a => a.Title.ToLower().Contains(sSearch)
              

                || a.Booktype.ToLower().Contains(sSearch)

                //|| a.BookSubType.StartsWith(sSearch)
                || a.Writer.StartsWith(sSearch)
                || a.ProductCode.StartsWith(sSearch)

                ).OrderBy(a => a.ProductId).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            else
                patients = db.ProductDetail.OrderBy(a => a.ProductId).Skip(iDisplayStart).Take(iDisplayLength).ToList();

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
