using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemo.Models;
using System.Globalization;

namespace MvcDemo.Controllers
{
    public class VoucherController : Controller
    {
        private SFS db = new SFS();

        //
        // GET: /Voucher/

        public ActionResult Index(string searchString)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE);

            /*help from http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application 
            */
            if (!String.IsNullOrEmpty(searchString))
            {
                voucher_table = voucher_table.Where(
                    s => s.STUDENT_ID.ToUpper().Contains(searchString.ToUpper())
                        || s.STUDENT.STUDENT_LNAME.ToUpper().Contains(searchString.ToUpper())
                        || s.STUDENT.STUDENT_FNAME.ToUpper().Contains(searchString.ToUpper())
                    );
            }
            return View(voucher_table.ToList());
        }


        //
        // GET: /Voucher/Details/5

        public ActionResult Details(int id = 0)
        {
            VOUCHER_TABLE voucher_table = db.VOUCHER_TABLE.Find(id);
            if (voucher_table == null)
            {
                return HttpNotFound();
            }
            return View(voucher_table);
        }

        //
        // GET: /Voucher/Create
                        //when coming from the viewing grants for particular students (stuGrant), it brings the student ID and sets it as the defualt value for the dropdownlist 
        public ActionResult Create(string id = null)
        {
            if (id != "null")
            {
                TempData["StuID"] = id;
            }
            else
            {
                TempData["StuID"] = null;
            }                                                                            /*Sets the default value*/
            ViewBag.STUDENT_ID = new SelectList(db.STUDENTS, "STUDENT_ID", "STUDENT_ID", TempData["StuID"]);
            ViewBag.VOUCHER_TYPE_ID = new SelectList(db.VOUCHER_TYPE_TABLLE, "VOUCHER_TYPE_ID", "VOUCHER_TYPE");
            return View();
        }

        //
        // POST: /Voucher/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VOUCHER_TABLE voucher_table)
        {

            //reset the id 
            if (ModelState.ContainsKey("REF_NUM"))
                ModelState["REF_NUM"].Errors.Clear();

            if (ModelState.IsValid)
            {
              voucher_table.DATE_OF_ISSUE = DateTime.Now;

                db.VOUCHER_TABLE.Add(voucher_table);
                db.SaveChanges();
                TempData["notice"] = "New Voucher Has Been Successfully Issued";
                return RedirectToAction("Index");
            }
                                                                                            
            ViewBag.STUDENT_ID = new SelectList(db.STUDENTS, "STUDENT_ID", "STUDENT_ID", voucher_table.STUDENT_ID);
            ViewBag.VOUCHER_TYPE_ID = new SelectList(db.VOUCHER_TYPE_TABLLE, "VOUCHER_TYPE_ID", "VOUCHER_TYPE", voucher_table.VOUCHER_TYPE_ID);
            return View(voucher_table);
        }


        //
        // GET: /Voucher/Edit/5

        public ActionResult Edit(int id = 0)
        {
            VOUCHER_TABLE voucher_table = db.VOUCHER_TABLE.Find(id);
            if (voucher_table == null)
            {
                return HttpNotFound();
            }
            ViewBag.STUDENT_ID = new SelectList(db.STUDENTS, "STUDENT_ID", "STUDENT_ID", voucher_table.STUDENT_ID);
            ViewBag.VOUCHER_TYPE_ID = new SelectList(db.VOUCHER_TYPE_TABLLE, "VOUCHER_TYPE_ID", "VOUCHER_TYPE", voucher_table.VOUCHER_TYPE_ID);
            return View(voucher_table);
        }

        //
        // POST: /Voucher/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VOUCHER_TABLE voucher_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voucher_table).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "The Issued Voucher Has Been Successfully Altered";
                return RedirectToAction("Index");
            }
            ViewBag.STUDENT_ID = new SelectList(db.STUDENTS, "STUDENT_ID", "STUDENT_FNAME", voucher_table.STUDENT_ID);
            ViewBag.VOUCHER_TYPE_ID = new SelectList(db.VOUCHER_TYPE_TABLLE, "VOUCHER_TYPE_ID", "VOUCHER_TYPE", voucher_table.VOUCHER_TYPE_ID);
            return View(voucher_table);
        }

        //
        // GET: /Voucher/Delete/5

        public ActionResult Delete(int id = 0)
        {
            VOUCHER_TABLE voucher_table = db.VOUCHER_TABLE.Find(id);
            if (voucher_table == null)
            {
                return HttpNotFound();
            }
            return View(voucher_table);
        }

        //
        // POST: /Voucher/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VOUCHER_TABLE voucher_table = db.VOUCHER_TABLE.Find(id);
            db.VOUCHER_TABLE.Remove(voucher_table);
            db.SaveChanges();
            TempData["notice"] = "The Issued Voucher Has Been Successfully Revocated";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}