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
    public class FacultyController : Controller
    {
        private SFS db = new SFS();

        //
        // GET: /Faculty/

        public ActionResult Index()
        {
            return View(db.FACULTY_TABLE.ToList());
        }

        //
        // GET: /Faculty/Details/5

        public ActionResult Details(int id = 0)
        {
            FACULTY_TABLE faculty_table = db.FACULTY_TABLE.Find(id);
            if (faculty_table == null)
            {
                return HttpNotFound();
            }
            return View(faculty_table);
        }

        //
        // GET: /Faculty/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Faculty/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FACULTY_TABLE faculty_table)
        {
            //reset the id 
            if (ModelState.ContainsKey("FACULTY_ID"))
                ModelState["FACULTY_ID"].Errors.Clear();
           
            if (ModelState.IsValid)
            {
                db.FACULTY_TABLE.Add(faculty_table);
                db.SaveChanges();
                TempData["notice"] = "New Faculty Has Been Successfully registered!";
                return RedirectToAction("Index");
            }

            return View(faculty_table);
        }

        //
        // GET: /Faculty/Edit/5

        public ActionResult Edit(int id = 0)
        {
            FACULTY_TABLE faculty_table = db.FACULTY_TABLE.Find(id);
            if (faculty_table == null)
            {
                return HttpNotFound();
            }
            return View(faculty_table);
        }

        //
        // POST: /Faculty/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FACULTY_TABLE faculty_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faculty_table).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "The Faculty Has Been Successfully Altered!";
                return RedirectToAction("Index");
            }
            return View(faculty_table);
        }

        //
        // GET: /Faculty/Delete/5

        public ActionResult Delete(int id = 0)
        {
            FACULTY_TABLE faculty_table = db.FACULTY_TABLE.Find(id);
            if (faculty_table == null)
            {
                return HttpNotFound();
            }
            return View(faculty_table);
        }

        //
        // POST: /Faculty/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FACULTY_TABLE faculty_table = db.FACULTY_TABLE.Find(id);
            db.FACULTY_TABLE.Remove(faculty_table);
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