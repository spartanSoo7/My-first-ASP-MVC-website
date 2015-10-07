using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemo.Models;

namespace MvcDemo.Controllers
{
    public class DetailedEthinicityController : Controller
    {
        private SFS db = new SFS();

        //
        // GET: /DetailedEthinicity/

        public ActionResult Index()
        {
            var detailed_ethinicity_table = db.DETAILED_ETHINICITY_TABLE.Include(d => d.ETHINICITY_TABLE);
            return View(detailed_ethinicity_table.ToList());
        }

        //
        // GET: /DetailedEthinicity/Details/5

        public ActionResult Details(int id = 0)
        {
            DETAILED_ETHINICITY_TABLE detailed_ethinicity_table = db.DETAILED_ETHINICITY_TABLE.Find(id);
            if (detailed_ethinicity_table == null)
            {
                return HttpNotFound();
            }
            return View(detailed_ethinicity_table);
        }

        //
        // GET: /DetailedEthinicity/Create

        public ActionResult Create()
        {
            ViewBag.ETHINICITY_ID = new SelectList(db.ETHINICITY_TABLE, "ETHINICITY_ID", "ETHINICITY");
            return View();
        }

        //
        // POST: /DetailedEthinicity/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DETAILED_ETHINICITY_TABLE detailed_ethinicity_table)
        {

            //reset the id 
            if (ModelState.ContainsKey("DETAILED_ETHINICITY_ID"))
                ModelState["DETAILED_ETHINICITY_ID"].Errors.Clear();

            if (ModelState.IsValid)
            {
                db.DETAILED_ETHINICITY_TABLE.Add(detailed_ethinicity_table);
                db.SaveChanges();
                TempData["notice"] = "The New Ethinicity Has Been Successfully Added";
                return RedirectToAction("Index");
            }

            ViewBag.ETHINICITY_ID = new SelectList(db.ETHINICITY_TABLE, "ETHINICITY_ID", "ETHINICITY", detailed_ethinicity_table.ETHINICITY_ID);
            return View(detailed_ethinicity_table);
        }

        //
        // GET: /DetailedEthinicity/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DETAILED_ETHINICITY_TABLE detailed_ethinicity_table = db.DETAILED_ETHINICITY_TABLE.Find(id);
            if (detailed_ethinicity_table == null)
            {
                return HttpNotFound();
            }
            ViewBag.ETHINICITY_ID = new SelectList(db.ETHINICITY_TABLE, "ETHINICITY_ID", "ETHINICITY", detailed_ethinicity_table.ETHINICITY_ID);
            return View(detailed_ethinicity_table);
        }

        //
        // POST: /DetailedEthinicity/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DETAILED_ETHINICITY_TABLE detailed_ethinicity_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailed_ethinicity_table).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "Ethinicity Has Been Successfully Changed";
                return RedirectToAction("Index");
            }
            ViewBag.ETHINICITY_ID = new SelectList(db.ETHINICITY_TABLE, "ETHINICITY_ID", "ETHINICITY", detailed_ethinicity_table.ETHINICITY_ID);
            return View(detailed_ethinicity_table);
        }

        //
        // GET: /DetailedEthinicity/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DETAILED_ETHINICITY_TABLE detailed_ethinicity_table = db.DETAILED_ETHINICITY_TABLE.Find(id);
            if (detailed_ethinicity_table == null)
            {
                return HttpNotFound();
            }
            return View(detailed_ethinicity_table);
        }

        //
        // POST: /DetailedEthinicity/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DETAILED_ETHINICITY_TABLE detailed_ethinicity_table = db.DETAILED_ETHINICITY_TABLE.Find(id);
            db.DETAILED_ETHINICITY_TABLE.Remove(detailed_ethinicity_table);
            db.SaveChanges();
            TempData["notice"] = "Ethinicity Has Been Successfully Added";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}