using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asignment6.Models;

namespace Asignment6.Controllers
{
    [Authorize]
    public class BeverageController : Controller
    {
        private BeverageGFarnsworthEntities db = new BeverageGFarnsworthEntities();
      
        [HttpPost, ActionName("filter")]
        public ActionResult Filter(bool active = false, bool unactive = false)
        {
            Session["name"] = Request.Form.Get("name");
            Session["min"] = Request.Form.Get("min");
            Session["max"] = Request.Form.Get("max");
            Session["pack"] = Request.Form.Get("pack");
            Session["active"] = active;
            Session["unactive"] = unactive;
            return RedirectToAction("Index");
        }

        // GET: /Beverage/
        public ActionResult Index()
        {
            Decimal min = 0;
            Decimal max = 10000000;
            string name = "";
            bool active = true;
            bool unactive = true;
            string pack = "";
            if(Session["active"] != null)
            {
                active = (bool)Session["active"];
                ViewBag.active = active;
            }
            if (Session["unactive"] != null)
            {
                unactive = (bool)Session["unactive"];
                ViewBag.unactive = unactive;
            }
            if(!string.IsNullOrWhiteSpace((string)Session["min"]))
            {
                min = decimal.Parse((string)Session["min"]);
                ViewBag.min = min;
                
            }
            if (!string.IsNullOrWhiteSpace((string)Session["max"]))
            {
                max = decimal.Parse((string)Session["max"]);
                ViewBag.max = max;
            }
            if (!string.IsNullOrWhiteSpace((string)Session["name"]))
            {
                name = ((string)Session["name"]);
                ViewBag.name = name;
            }
            if (!string.IsNullOrWhiteSpace((string)Session["pack"]))
            {
                pack = ((string)Session["pack"]);
                ViewBag.pack = pack;
            }
            Beverage[] filtered = new Beverage[0];
            if(active && unactive)
                filtered = db.Beverages.Where(b => b.name.Contains(name) && b.price <= max && b.price >= min && b.pack.Contains(pack)).ToArray();
            else if(active)
                filtered = db.Beverages.Where(b => b.name.Contains(name) && b.price <= max && b.price >= min && b.pack.Contains(pack) && b.active == true).ToArray();
            else if(unactive)
                filtered = db.Beverages.Where(b => b.name.Contains(name) && b.price <= max && b.price >= min && b.pack.Contains(pack) && b.active == false).ToArray();
            return View(filtered);
        }

        // GET: /Beverage/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // GET: /Beverage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Beverage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,name,pack,price,active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                db.Beverages.Add(beverage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(beverage);
        }

        // GET: /Beverage/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: /Beverage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,name,pack,price,active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beverage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beverage);
        }

        // GET: /Beverage/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: /Beverage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Beverage beverage = db.Beverages.Find(id);
            db.Beverages.Remove(beverage);
            db.SaveChanges();
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
