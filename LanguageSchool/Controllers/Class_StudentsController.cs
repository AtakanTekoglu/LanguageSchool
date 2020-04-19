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
    [Authorize(Roles = "Admin,Personel")]
    public class Class_StudentsController : Controller
    {
        private LanguageAcademyEntities db = new LanguageAcademyEntities();


        public ActionResult Deneme()
        {

            return View();
        }

        // GET: Class_Students
        public ActionResult Index()
        {
            var class_Students = db.Class_Students.Include(c => c.Class).Include(c => c.Student);
            return View(class_Students.ToList());
        }

        // GET: Class_Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Students class_Students = db.Class_Students.Find(id);
            if (class_Students == null)
            {
                return HttpNotFound();
            }
            return View(class_Students);
        }

        // GET: Class_Students/Create
        public ActionResult Create()
        {
            //List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem() { Text = "1", Value = "1" });
            //items.Add(new SelectListItem() { Text = "2", Value = "2" });
            //items.Add(new SelectListItem() { Text = "3", Value = "3" });
            //items.Add(new SelectListItem() { Text = "4", Value = "4" });
            //items.Add(new SelectListItem() { Text = "5", Value = "5" });
            //items.Add(new SelectListItem() { Text = "6", Value = "6" });

            //ViewData["ListItems"] = items;

            var student = db.Students.ToList();
            IEnumerable<SelectListItem> selectList = from t in student
                                                     select new SelectListItem
                                                     {
                                                         Text = t.Firstname + " " + t.Lastname,
                                                         Value = t.Id.ToString()
                                                     };

            var myClasses = db.Classes.ToList();
            IEnumerable<SelectListItem> selectListClass = from t in myClasses
                                                          select new SelectListItem
                                                          {
                                                              Text = t.Name + " - " + t.Price + " TL",
                                                              Value = t.Id.ToString()
                                                          };

            ViewBag.ClassId = new SelectList(selectListClass, "Value", "Text");
            ViewBag.StudentId = new SelectList(selectList, "Value", "Text");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClassId,StudentId")] Class_Students class_Students, FormCollection form)
        {
            int installment = Convert.ToInt32(form["txtInstallment"].ToString());
            var targetPrice = db.Classes.Where(x => x.Id == class_Students.ClassId).FirstOrDefault();
            var classFee = targetPrice.Price;

            if (ModelState.IsValid)
            {
                FinancialStatement financial = new FinancialStatement();
                double fee = classFee / installment;
                DateTime todayDateTime = DateTime.Now;
                DateTime paymentDateTime;
                for (int i = 0; i < installment; i++)
                {
                    paymentDateTime = todayDateTime.AddMonths(i + 1);
                    if (paymentDateTime.DayOfWeek.ToString() == "Saturday")
                    {
                        paymentDateTime = paymentDateTime.AddDays(-1);
                    }
                    else if (paymentDateTime.DayOfWeek.ToString() == "Sunday")
                    {
                        paymentDateTime = paymentDateTime.AddDays(1);
                    }

                    financial.Amount = fee;
                    financial.InstallmentCount = installment;
                    financial.OrderOfInstallment = (i + 1);
                    financial.Date = paymentDateTime;
                    financial.IsPaymentInAdvance = false;
                    financial.StudentId = class_Students.StudentId;
                    financial.ClassId = class_Students.ClassId;
                    financial.PaymentStatus = false;
                    db.FinancialStatements.Add(financial);
                    db.SaveChanges();
                }

               
                db.Class_Students.Add(class_Students);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", class_Students.ClassId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "City", class_Students.StudentId);
            return View(class_Students);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Students class_Students = db.Class_Students.Find(id);
            if (class_Students == null)
            {
                return HttpNotFound();
            }
            return View(class_Students);
        }

        // POST: Class_Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class_Students class_Students = db.Class_Students.Find(id);
            db.Class_Students.Remove(class_Students);
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
