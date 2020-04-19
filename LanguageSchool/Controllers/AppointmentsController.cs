using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;

namespace LanguageSchool.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppointmentsController : Controller
    {
        // GET: Appointments
        LanguageAcademyEntities db = new LanguageAcademyEntities();

        [HttpGet]
        public ActionResult Create()
        {
            var listTeachers = db.Teachers.ToList();
            var listDays = db.WeekDays.ToList();

            var model = new TeacherDayAppointmentController {_WeekDays = listDays, _Teachers = listTeachers};

            var myTeacherList = (from teacher in db.Teachers.ToList()
                select new SelectListItem
                {
                    Text = teacher.Firstname + " " + teacher.Lastname,
                    Value = teacher.Id.ToString(),
                }).ToList();
            ViewBag.myTeacherList = myTeacherList;
            var myDayList = (from day in db.WeekDays.ToList()
                select new SelectListItem
                {
                    Text = day.Name,
                    Value = day.Id.ToString(),
                }).ToList();
            ViewBag.myDayList = myTeacherList;

            return View(model);
        }
    }
}