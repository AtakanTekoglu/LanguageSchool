using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;

namespace LanguageSchool.Controllers
{
    [Authorize(Roles = "Admin,Personel")]
    public class HomeController : Controller
    {
        LanguageAcademyEntities db = new LanguageAcademyEntities();
        public ActionResult Index()
        {
            var totalAmount = db.FinancialStatements.Sum(x => x.Amount);
            ViewBag.totalAmount = totalAmount;

            var listOfStudents = db.Students.ToList();
            var numberOfStudents = listOfStudents.Count();
            ViewBag.numberOfStudents = numberOfStudents;

            var listOfBranch = db.Branches.ToList();
            var numberOfBranch = listOfBranch.Count();
            ViewBag.numberOfBranch = numberOfBranch;

            var listOfClasses = db.Classes.ToList();
            var numberOfClasses = listOfClasses.Count();
            ViewBag.numberOfClasses = numberOfClasses;

            var listOfTeachers = db.Teachers.ToList();
            var numberOfTeacher = listOfTeachers.Count();
            ViewBag.numberOfTeacher = numberOfTeacher;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}