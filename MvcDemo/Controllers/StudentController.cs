using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemo.Models;
using PagedList;

namespace MvcDemo.Controllers
{
    public class StudentController : Controller
    {
        private SFS db = new SFS();


        public ActionResult StuGrant(string id)
        {
            var Vouchers = db.VOUCHER_TABLE.Include(s => s.STUDENT);


            if (!String.IsNullOrEmpty(id))
            {
                Vouchers = Vouchers.Where(
                    s => s.STUDENT_ID.ToUpper().Contains(id.ToUpper())
                    );
            }
            return View(Vouchers.ToList());
        }




        //
        // GET: /Student/
        
        public ActionResult Index(string searchString)
        {
           var students = db.STUDENTS.Include(s => s.AVAILABLE_COURSE).Include(s => s.DETAILED_ETHINICITY_TABLE).Include(s => s.ETHINICITY_TABLE);
            

            /*help from http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application 
            */
           if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(
                    s => s.STUDENT_LNAME.ToUpper().Contains(searchString.ToUpper() )
                    || s.STUDENT_FNAME.ToUpper().Contains(searchString.ToUpper() )
                    || s.STUDENT_ID.ToUpper().Contains(searchString.ToUpper())                 
                    );
            }
           return View(students.ToList() );
        }


        public PartialViewResult PartialStudents(string searchString)
        {
          

            var students = db.STUDENTS.Include(s => s.AVAILABLE_COURSE).Include(s => s.DETAILED_ETHINICITY_TABLE).Include(s => s.ETHINICITY_TABLE);

            
            /*help from http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application 
            */
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(
                    s => s.STUDENT_LNAME.ToUpper().Contains(searchString.ToUpper())
                    || s.STUDENT_FNAME.ToUpper().Contains(searchString.ToUpper())
                    || s.STUDENT_ID.ToUpper().Contains(searchString.ToUpper())
                    );
            }

            return PartialView(students.ToList());
        }



        //
        // GET: /Student/Details/5

        public ActionResult Details(string id = null)
        {
            STUDENT student = db.STUDENTS.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }



        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            ViewBag.AVAIL_COURSE_ID = new SelectList(db.AVAILABLE_COURSE, "AVAIL_COURSE_ID", "AVAIL_COURSE_ID");
            ViewBag.DETAILED_ETHINICITY_ID = new SelectList(db.DETAILED_ETHINICITY_TABLE, "DETAILED_ETHINICITY_ID", "DETAILED_ETHINICITY");
            ViewBag.ETHINICITY_ID = new SelectList(db.ETHINICITY_TABLE, "ETHINICITY_ID", "ETHINICITY");
            return View();
        }

        //
        // POST: /Student/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(STUDENT student)
        {
            if (ModelState.IsValid)
            {
                db.STUDENTS.Add(student);
                db.SaveChanges();
                TempData["notice"] = "Student Has Been Successfully Added";
                return RedirectToAction("Index");
            }

            ViewBag.AVAIL_COURSE_ID = new SelectList(db.AVAILABLE_COURSE, "AVAIL_COURSE_ID", "AVAIL_COURSE_ID", student.AVAIL_COURSE_ID);
            ViewBag.DETAILED_ETHINICITY_ID = new SelectList(db.DETAILED_ETHINICITY_TABLE, "DETAILED_ETHINICITY_ID", "DETAILED_ETHINICITY", student.DETAILED_ETHINICITY_ID);
            ViewBag.ETHINICITY_ID = new SelectList(db.ETHINICITY_TABLE, "ETHINICITY_ID", "ETHINICITY", student.ETHINICITY_ID);
            return View(student);
        }

        //
        // GET: /Student/Edit/5

        public ActionResult Edit(string id = null)
        {
            STUDENT student = db.STUDENTS.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.AVAIL_COURSE_ID = new SelectList(db.AVAILABLE_COURSE, "AVAIL_COURSE_ID", "AVAIL_COURSE_ID", student.AVAIL_COURSE_ID);
            ViewBag.DETAILED_ETHINICITY_ID = new SelectList(db.DETAILED_ETHINICITY_TABLE, "DETAILED_ETHINICITY_ID", "DETAILED_ETHINICITY", student.DETAILED_ETHINICITY_ID);
            ViewBag.ETHINICITY_ID = new SelectList(db.ETHINICITY_TABLE, "ETHINICITY_ID", "ETHINICITY", student.ETHINICITY_ID);
            return View(student);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(STUDENT student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "Student Has Been Successfully Edited!";
                return RedirectToAction("Index");
            }
            ViewBag.AVAIL_COURSE_ID = new SelectList(db.AVAILABLE_COURSE, "AVAIL_COURSE_ID", "AVAIL_COURSE_ID", student.AVAIL_COURSE_ID);
            ViewBag.DETAILED_ETHINICITY_ID = new SelectList(db.DETAILED_ETHINICITY_TABLE, "DETAILED_ETHINICITY_ID", "DETAILED_ETHINICITY", student.DETAILED_ETHINICITY_ID);
            ViewBag.ETHINICITY_ID = new SelectList(db.ETHINICITY_TABLE, "ETHINICITY_ID", "ETHINICITY", student.ETHINICITY_ID);
            return View(student);
        }

        //
        // GET: /Student/Delete/5

        public ActionResult Delete(string id = null)
        {
            STUDENT student = db.STUDENTS.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            STUDENT student = db.STUDENTS.Find(id);
            db.STUDENTS.Remove(student);
            db.SaveChanges();
            TempData["notice"] = "Student Has Been Successfully deleted!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}