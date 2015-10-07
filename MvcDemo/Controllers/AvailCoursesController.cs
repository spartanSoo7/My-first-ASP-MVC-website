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
    public class AvailCoursesController : Controller
    {
        private SFS db = new SFS();

        //
        // GET: /AvailCourses/

        public ActionResult Index()
        {
            var available_course = db.AVAILABLE_COURSE.Include(a => a.CAMPUS_TABLE).Include(a => a.COURSE_TABLE).Include(a => a.FACULTY_TABLE);
            return View(available_course.ToList());
        }

        //
        // GET: /AvailCourses/Details/5

        public ActionResult Details(int id = 0)
        {
            AVAILABLE_COURSE available_course = db.AVAILABLE_COURSE.Find(id);
            if (available_course == null)
            {
                return HttpNotFound();
            }
            return View(available_course);
        }

        //
        // GET: /AvailCourses/Create

        public ActionResult Create()
        {
            ViewBag.CAMPUS_ID = new SelectList(db.CAMPUS_TABLE, "CAMPUS_ID", "CAMPUS_NAME");
            ViewBag.COURSE_ID = new SelectList(db.COURSE_TABLE, "COURSE_ID", "COURSE_NAME");
            ViewBag.FACULTY_ID = new SelectList(db.FACULTY_TABLE, "FACULTY_ID", "FACULTY_NAME");
            return View();
        }

        //
        // POST: /AvailCourses/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AVAILABLE_COURSE available_course)
        {

            //reset the id 
            if (ModelState.ContainsKey("AVAIL_COURSE_ID"))
                ModelState["AVAIL_COURSE_ID"].Errors.Clear();

            if (ModelState.IsValid)
            {
                db.AVAILABLE_COURSE.Add(available_course);
                db.SaveChanges();
                TempData["notice"] = "New Campus Faculty Course Has Been Successfully Linked!";
                return RedirectToAction("Index");
            }

            ViewBag.CAMPUS_ID = new SelectList(db.CAMPUS_TABLE, "CAMPUS_ID", "CAMPUS_NAME", available_course.CAMPUS_ID);
            ViewBag.COURSE_ID = new SelectList(db.COURSE_TABLE, "COURSE_ID", "COURSE_NAME", available_course.COURSE_ID);
            ViewBag.FACULTY_ID = new SelectList(db.FACULTY_TABLE, "FACULTY_ID", "FACULTY_NAME", available_course.FACULTY_ID);
            return View(available_course);
        }

        //
        // GET: /AvailCourses/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AVAILABLE_COURSE available_course = db.AVAILABLE_COURSE.Find(id);
            if (available_course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CAMPUS_ID = new SelectList(db.CAMPUS_TABLE, "CAMPUS_ID", "CAMPUS_NAME", available_course.CAMPUS_ID);
            ViewBag.COURSE_ID = new SelectList(db.COURSE_TABLE, "COURSE_ID", "COURSE_NAME", available_course.COURSE_ID);
            ViewBag.FACULTY_ID = new SelectList(db.FACULTY_TABLE, "FACULTY_ID", "FACULTY_NAME", available_course.FACULTY_ID);
            return View(available_course);
        }

        //
        // POST: /AvailCourses/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AVAILABLE_COURSE available_course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(available_course).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "The Campus Faculty Course Link Has Been Successfully Altered!";
                return RedirectToAction("Index");
            }
            ViewBag.CAMPUS_ID = new SelectList(db.CAMPUS_TABLE, "CAMPUS_ID", "CAMPUS_NAME", available_course.CAMPUS_ID);
            ViewBag.COURSE_ID = new SelectList(db.COURSE_TABLE, "COURSE_ID", "COURSE_NAME", available_course.COURSE_ID);
            ViewBag.FACULTY_ID = new SelectList(db.FACULTY_TABLE, "FACULTY_ID", "FACULTY_NAME", available_course.FACULTY_ID);
            return View(available_course);
        }

        //
        // GET: /AvailCourses/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AVAILABLE_COURSE available_course = db.AVAILABLE_COURSE.Find(id);
            if (available_course == null)
            {
                return HttpNotFound();
            }
            return View(available_course);
        }

        //
        // POST: /AvailCourses/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AVAILABLE_COURSE available_course = db.AVAILABLE_COURSE.Find(id);
            db.AVAILABLE_COURSE.Remove(available_course);
            db.SaveChanges();
            TempData["notice"] = "The Campus Faculty Course Has Been Successfully DELETED!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}