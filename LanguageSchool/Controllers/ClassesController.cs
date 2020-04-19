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
    public class ClassesController : Controller
    {
        private LanguageAcademyEntities db = new LanguageAcademyEntities();

        // GET: Classes/Create
        public ActionResult Create()
        {
            var teacher = db.Teachers.ToList();
            IEnumerable<SelectListItem> selectList = from t in teacher
                                                     select new SelectListItem
                                                     {
                                                         Text = t.Firstname+" "+t.Lastname,
                                                         Value = t.Id.ToString()
                                                     };

            var kurs = db.Courses.ToList();
            IEnumerable<SelectListItem> list = from m in kurs
                select new SelectListItem
                {
                    Text = m.Language.Name+" "+m.Level.Name+" ("+m.Level.Sign+")",
                    Value = m.Id.ToString()
                };

            ViewBag.CourseId = new SelectList(list, "Value", "Text");
            ViewBag.TeacherId = new SelectList(selectList, "Value", "Text");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,StartDate,EndDate,Price,TeacherId,CourseId")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(@class);
                db.SaveChanges();
                return RedirectToAction("ShowClass", "Class");
            }


            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Description", @class.CourseId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Description", @class.TeacherId);
            return View(@class);
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
