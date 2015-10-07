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
    public class VoucherTypesController : Controller
    {
        private SFS db = new SFS();

        //
        // GET: /VoucherTypes/

        public ActionResult Index()
        {
            return View(db.VOUCHER_TYPE_TABLLE.ToList());
        }

        //
        // GET: /VoucherTypes/Details/5

        public ActionResult Details(int id = 0)
        {
            VOUCHER_TYPE_TABLLE voucher_type_tablle = db.VOUCHER_TYPE_TABLLE.Find(id);
            if (voucher_type_tablle == null)
            {
                return HttpNotFound();
            }
            return View(voucher_type_tablle);
        }

        //
        // GET: /VoucherTypes/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /VoucherTypes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VOUCHER_TYPE_TABLLE voucher_type_tablle)
        {

            //reset the id 
            if (ModelState.ContainsKey("VOUCHER_TYPE_ID"))
                ModelState["VOUCHER_TYPE_ID"].Errors.Clear();

            if (ModelState.IsValid)
            {
                db.VOUCHER_TYPE_TABLLE.Add(voucher_type_tablle);
                db.SaveChanges();
                TempData["notice"] = "The New Voucher Type Has Been Successfully Added";
                return RedirectToAction("Index");
            }

            return View(voucher_type_tablle);
        }

        //
        // GET: /VoucherTypes/Edit/5

        public ActionResult Edit(int id = 0)
        {
            VOUCHER_TYPE_TABLLE voucher_type_tablle = db.VOUCHER_TYPE_TABLLE.Find(id);
            if (voucher_type_tablle == null)
            {
                return HttpNotFound();
            }
            return View(voucher_type_tablle);
        }

        //
        // POST: /VoucherTypes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VOUCHER_TYPE_TABLLE voucher_type_tablle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voucher_type_tablle).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "The Voucher Type Has Been Successfully Altered";
                return RedirectToAction("Index");
            }
            return View(voucher_type_tablle);
        }

        //
        // GET: /VoucherTypes/Delete/5

        public ActionResult Delete(int id = 0)
        {
            VOUCHER_TYPE_TABLLE voucher_type_tablle = db.VOUCHER_TYPE_TABLLE.Find(id);
            if (voucher_type_tablle == null)
            {
                return HttpNotFound();
            }
            return View(voucher_type_tablle);
        }

        //
        // POST: /VoucherTypes/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VOUCHER_TYPE_TABLLE voucher_type_tablle = db.VOUCHER_TYPE_TABLLE.Find(id);
            db.VOUCHER_TYPE_TABLLE.Remove(voucher_type_tablle);
            db.SaveChanges();
            TempData["notice"] = "The Voucher Type Has Been Successfully Deleted";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}