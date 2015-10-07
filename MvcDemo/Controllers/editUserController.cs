using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemo.Models;
using WebMatrix.WebData;
using System.Net.Mail;
using System.Web.Security;

namespace MvcDemo.Controllers
{
    public class editUserController : Controller
    {
        private SFS db = new SFS();

        //
        // GET: /editUserEmail/

        public ActionResult Index()
        {
            return View(db.UserProfiles.ToList());
        }

        //
        // GET: /editUserEmail/Details/5

        public ActionResult Details(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }


        //
        // GET: /editUserEmail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }


        //
        // POST: /editUserEmail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "User has been changed";
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }



        //
        // GET: /editUserEmail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            else if (id == 1)
            {
                TempData["noticeDel"] = "YOU CANNOT DELETE ADMIN!";
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        //
        // POST: /editUserEmail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            webpages_Membership webPageMem = db.webpages_Membership.Find(id);
            db.UserProfiles.Remove(userprofile);
            db.webpages_Membership.Remove(webPageMem);
            db.SaveChanges();
            TempData["notice"] = "User has been deleted";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}