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
    public class EthnicityController : Controller
    {
        private SFS db = new SFS();

        //
        // GET: /Ethnicity/

        public ActionResult Index()
        {
            return View(db.ETHINICITY_TABLE.ToList());
        }

        //
        // GET: /Ethnicity/Details/5

        public ActionResult Details(int id = 0)
        {
            ETHINICITY_TABLE ethinicity_table = db.ETHINICITY_TABLE.Find(id);
            if (ethinicity_table == null)
            {
                return HttpNotFound();
            }
            return View(ethinicity_table);
        }

        //
        // GET: /Ethnicity/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Ethnicity/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ETHINICITY_TABLE ethinicity_table)
        {

            //reset the id 
            if (ModelState.ContainsKey("ETHINICITY_ID"))
                ModelState["ETHINICITY_ID"].Errors.Clear();

            if (ModelState.IsValid)
            {
                db.ETHINICITY_TABLE.Add(ethinicity_table);
                db.SaveChanges();
                TempData["notice"] = "The New Ethinicity Has Been Successfully Added";
                return RedirectToAction("Index");
            }

            return View(ethinicity_table);
        }

        //
        // GET: /Ethnicity/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ETHINICITY_TABLE ethinicity_table = db.ETHINICITY_TABLE.Find(id);
            if (ethinicity_table == null)
            {
                return HttpNotFound();
            }
            return View(ethinicity_table);
        }

        //
        // POST: /Ethnicity/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ETHINICITY_TABLE ethinicity_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ethinicity_table).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "Ethinicity Has Been Successfully Changed";
                return RedirectToAction("Index");
            }
            return View(ethinicity_table);
        }

        //
        // GET: /Ethnicity/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ETHINICITY_TABLE ethinicity_table = db.ETHINICITY_TABLE.Find(id);
            if (ethinicity_table == null)
            {
                return HttpNotFound();
            }
            return View(ethinicity_table);
        }

        //
        // POST: /Ethnicity/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ETHINICITY_TABLE ethinicity_table = db.ETHINICITY_TABLE.Find(id);
            db.ETHINICITY_TABLE.Remove(ethinicity_table);
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