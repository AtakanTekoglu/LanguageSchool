using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;

namespace LanguageSchool.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        // GET: Course
       
        LanguageAcademyEntities db = new LanguageAcademyEntities();


        public ActionResult ShowCourse()
        {
            var courses = db.Courses.ToList();
            return View(courses);
        }

        [HttpGet]
        public ActionResult AddCourse()
        {

            var brancheSelectListItems = (from i in db.Branches.ToList()
                    select new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }
                ).ToList();
            ViewBag.branch = brancheSelectListItems;


            var languageSelectListItems = (from i in db.Languages.ToList()
                    select new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }
                ).ToList();
            ViewBag.language = languageSelectListItems;

            var levelSelectListItems = (from i in db.Levels.ToList()
                    select new SelectListItem
                    {
                        Text = i.Sign +" "+ i.Name,
                        Value = i.Id.ToString()
                    }
                ).ToList();
            ViewBag.level = levelSelectListItems;

            return View();
        }

        [HttpPost]
        public ActionResult AddCourse(Course course)
        {

            var branch = db.Branches.Where(m => m.Id == course.Branch.Id).FirstOrDefault();
            var language = db.Languages.Where(m => m.Id == course.Language.Id).FirstOrDefault();
            var level = db.Levels.Where(m => m.Id == course.Level.Id).FirstOrDefault();
            course.Branch = branch;
            course.Language = language;
            course.Level = level;
            db.Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("ShowCourse", "Course");


            //return View();
        }
    }
}