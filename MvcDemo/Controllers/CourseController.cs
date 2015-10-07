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
    public class CourseController : Controller
    {
        private SFS db = new SFS();

        //
        // GET: /Course/

        public ActionResult Index()
        {
            var course_table = db.COURSE_TABLE.Include(c => c.FACULTY_TABLE);
            return View(course_table.ToList());
        }

        //
        // GET: /Course/Details/5

        public ActionResult Details(int id = 0)
        {
            COURSE_TABLE course_table = db.COURSE_TABLE.Find(id);
            if (course_table == null)
            {
                return HttpNotFound();
            }
            return View(course_table);
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            ViewBag.FACULTY_ID = new SelectList(db.FACULTY_TABLE, "FACULTY_ID", "FACULTY_NAME");
            return View();
        }

        //
        // POST: /Course/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(COURSE_TABLE course_table)
        {
            //reset the id 
            if (ModelState.ContainsKey("COURSE_ID"))
                ModelState["COURSE_ID"].Errors.Clear();

            if (ModelState.IsValid)
            {
                db.COURSE_TABLE.Add(course_table);
                db.SaveChanges();
                TempData["notice"] = "New Course Has Been Successfully registered!";
                return RedirectToAction("Index");
            }

            ViewBag.FACULTY_ID = new SelectList(db.FACULTY_TABLE, "FACULTY_ID", "FACULTY_NAME", course_table.FACULTY_ID);
            return View(course_table);
        }

        //
        // GET: /Course/Edit/5

        public ActionResult Edit(int id = 0)
        {
            COURSE_TABLE course_table = db.COURSE_TABLE.Find(id);
            if (course_table == null)
            {
                return HttpNotFound();
            }
            ViewBag.FACULTY_ID = new SelectList(db.FACULTY_TABLE, "FACULTY_ID", "FACULTY_NAME", course_table.FACULTY_ID);
            return View(course_table);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(COURSE_TABLE course_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course_table).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "The Course Has Been Successfully Altered!";
                return RedirectToAction("Index");
            }
            ViewBag.FACULTY_ID = new SelectList(db.FACULTY_TABLE, "FACULTY_ID", "FACULTY_NAME", course_table.FACULTY_ID);
            return View(course_table);
        }

        //
        // GET: /Course/Delete/5

        public ActionResult Delete(int id = 0)
        {
            COURSE_TABLE course_table = db.COURSE_TABLE.Find(id);
            if (course_table == null)
            {
                return HttpNotFound();
            }
            return View(course_table);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COURSE_TABLE course_table = db.COURSE_TABLE.Find(id);
            db.COURSE_TABLE.Remove(course_table);
            db.SaveChanges();
            TempData["notice"] = "The Course Has Been Successfully Deleted!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}