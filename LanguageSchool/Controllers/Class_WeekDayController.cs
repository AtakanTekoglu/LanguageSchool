using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;

namespace LanguageSchool.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Class_WeekDayController : Controller
    {
        private LanguageAcademyEntities db = new LanguageAcademyEntities();

        // GET: Class_WeekDay/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name");
            ViewBag.WeekDayId = new SelectList(db.WeekDays, "Id", "Name");
            return View();
        }

        // POST: Class_WeekDay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassId,WeekDayId,Hours")] Class_WeekDay class_WeekDay)
        {
            if (ModelState.IsValid)
            {
                db.Class_WeekDay.Add(class_WeekDay);
                db.SaveChanges();
                return RedirectToAction("ShowClass","Class");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", class_WeekDay.ClassId);
            ViewBag.WeekDayId = new SelectList(db.WeekDays, "Id", "Name", class_WeekDay.WeekDayId);
            return View(class_WeekDay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
