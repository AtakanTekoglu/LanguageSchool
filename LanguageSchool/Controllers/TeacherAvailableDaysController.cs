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
    public class TeacherAvailableDaysController : Controller
    {
        private LanguageAcademyEntities db = new LanguageAcademyEntities();

        // GET: TeacherAvailableDays
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.Teacher).Include(a => a.WeekDay);
            return View(appointments.ToList());
        }

        // GET: TeacherAvailableDays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: TeacherAvailableDays/Create
        public ActionResult Create()
        {



            var teacher = db.Teachers.ToList();
            IEnumerable<SelectListItem> selectList = from t in teacher
                select new SelectListItem
                {
                    Text = t.Firstname + " " + t.Lastname,
                    Value = t.Id.ToString()
                };

            var weekday = db.WeekDays.ToList();
            IEnumerable<SelectListItem> list = from m in weekday
                select new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                };

            ViewBag.TeacherId = new SelectList(selectList, "Value", "Text");
            ViewBag.WeekDayId = new SelectList(list, "Value", "Text");
            return View();
        }

        // POST: TeacherAvailableDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeacherId,Hours,WeekDayId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Description", appointment.TeacherId);
            ViewBag.WeekDayId = new SelectList(db.WeekDays, "Id", "Name", appointment.WeekDayId);
            return View(appointment);
        }

        // GET: TeacherAvailableDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            var teacher = db.Teachers.ToList();
            IEnumerable<SelectListItem> selectList = from t in teacher
                select new SelectListItem
                {
                    Text = t.Firstname + " " + t.Lastname,
                    Value = t.Id.ToString()
                };


            ViewBag.TeacherId = new SelectList(selectList, "Value", "Text", appointment.TeacherId);
            ViewBag.WeekDayId = new SelectList(db.WeekDays, "Id", "Name", appointment.WeekDayId);
            return View(appointment);
        }

        // POST: TeacherAvailableDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeacherId,Hours,WeekDayId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Description", appointment.TeacherId);
            ViewBag.WeekDayId = new SelectList(db.WeekDays, "Id", "Name", appointment.WeekDayId);
            return View(appointment);
        }

        // GET: TeacherAvailableDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: TeacherAvailableDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
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
