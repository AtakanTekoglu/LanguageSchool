using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;

namespace LanguageSchool.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClassController : Controller
    {
        // GET: Class
        LanguageAcademyEntities db = new LanguageAcademyEntities();
        public ActionResult ShowClass()
        {
            var classList = db.Classes.ToList();
            return View(classList);
        }

        public ActionResult ClassWeekDay(int id)
        {
            var selection = db.Class_WeekDay.Where(m => m.ClassId == id).ToList();

            return View(selection);
        }

        public ActionResult ClassTeacherDetail(int id)
        {

            ViewBag.dgr = db.Teachers.Where(m => m.Id == id).FirstOrDefault();


            var myList = (from t in db.Appointments.Where(m=>m.TeacherId==id)
                    from d in db.WeekDays.Where(m=>m.Id==t.WeekDayId)
                    //where t.TeacherId == id
                    select new TeacherAppointment
                    {
                        DayName = d.Name,
                        WHours = t.Hours
                    }
                ).ToList();

            // Get the languages which can be learned by the teacher based on teacher id
            var query = from t in db.Teachers.Where(x => x.Id == id)
                        from l in t.Languages
                        select new
                        {
                            LangName = l.Name,
                        };
            var list = new List<Language>();
            foreach (var item in query)
            {
                list.Add(new Language()
                {
                    Name = item.LangName
                });
            }

            var model = new TeacherDetailViewModel { TeacherAppointments = myList,Languages =list};


            return View(model);
        }


    }
}