using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using healB.Models;

namespace healB.Controllers
{
    public class MedicinesController : Controller
    {
        private healBEntities db = new healBEntities();

        //
        // GET: /Medicines/

        public ActionResult Index()
        {
            var medicines = db.medicines.Include(m => m.time);
            return View(medicines.ToList());
        }

        //
        // GET: /Medicines/Details/5

        public ActionResult Details(int id = 0)
        {
            medicine medicine = db.medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        //
        // GET: /Medicines/Create

        public ActionResult Create()
        {
            ViewBag.time_id = new SelectList(db.times, "time_id", "name");
            return View();
        }

        //
        // POST: /Medicines/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.medicines.Add(medicine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.time_id = new SelectList(db.times, "time_id", "name", medicine.time_id);
            return View(medicine);
        }

        //
        // GET: /Medicines/Edit/5

        public ActionResult Edit(int id = 0)
        {
            medicine medicine = db.medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            ViewBag.time_id = new SelectList(db.times, "time_id", "name", medicine.time_id);
            return View(medicine);
        }

        //
        // POST: /Medicines/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.time_id = new SelectList(db.times, "time_id", "name", medicine.time_id);
            return View(medicine);
        }

        //
        // GET: /Medicines/Delete/5

        public ActionResult Delete(int id = 0)
        {
            medicine medicine = db.medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        //
        // POST: /Medicines/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            medicine medicine = db.medicines.Find(id);
            db.medicines.Remove(medicine);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}