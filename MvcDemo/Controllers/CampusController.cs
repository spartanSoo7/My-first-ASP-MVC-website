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
    public class CampusController : Controller
    {
        private SFS db = new SFS();

        //
        // GET: /Campus/

        public ActionResult Index()
        {
            return View(db.CAMPUS_TABLE.ToList());
        }

        //
        // GET: /Campus/Details/5

        public ActionResult Details(int id = 0)
        {
            CAMPUS_TABLE campus_table = db.CAMPUS_TABLE.Find(id);
            if (campus_table == null)
            {
                return HttpNotFound();
            }
            return View(campus_table);
        }

        //
        // GET: /Campus/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Campus/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CAMPUS_TABLE campus_table)
        {
            //reset the id 
            if (ModelState.ContainsKey("CAMPUS_ID"))
                ModelState["CAMPUS_ID"].Errors.Clear();

            if (ModelState.IsValid)
            {
                db.CAMPUS_TABLE.Add(campus_table);
                db.SaveChanges();
                TempData["notice"] = "New Campus Has Been Successfully registered!";
                return RedirectToAction("Index");
            }

            return View(campus_table);
        }

        //
        // GET: /Campus/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CAMPUS_TABLE campus_table = db.CAMPUS_TABLE.Find(id);
            if (campus_table == null)
            {
                return HttpNotFound();
            }
            return View(campus_table);
        }

        //
        // POST: /Campus/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CAMPUS_TABLE campus_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campus_table).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "The Campus Has Been Successfully Altered!";
                return RedirectToAction("Index");
            }
            return View(campus_table);
        }

        //
        // GET: /Campus/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CAMPUS_TABLE campus_table = db.CAMPUS_TABLE.Find(id);
            if (campus_table == null)
            {
                return HttpNotFound();
            }
            return View(campus_table);
        }

        //
        // POST: /Campus/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)     /*need to implement cacading delete*/
        {
            CAMPUS_TABLE campus_table = db.CAMPUS_TABLE.Find(id);
            db.CAMPUS_TABLE.Remove(campus_table);
            db.SaveChanges();
            TempData["notice"] = "The Campus Has Been Successfully Deleted!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}