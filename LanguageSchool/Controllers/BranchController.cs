using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using Microsoft.Ajax.Utilities;

namespace LanguageSchool.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BranchController : Controller
    {
        // GET: Branch
        LanguageAcademyEntities db = new LanguageAcademyEntities();
        public ActionResult Index()
        {
            var branchList = db.Branches.ToList();
            return View(branchList);
        }

        public ActionResult BranchDetail(int id)
        {
            var myTransportation = db.BusTransportations.Where(x => x.BranchId == id).ToList();
            var mySocial = db.SocialOpportunities.Where(x => x.BranchId == id).ToList();
            var myCourse = db.Courses.Where(x => x.BranchId == id).ToList();
            var model = new ResultViewModel { _BusTransportations = myTransportation, _SocialOpportunities = mySocial, _Courses = myCourse };


            var myBranch = db.Branches.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.myBra = myBranch.Name;
            var myAddress = db.Addresses.Where(x => x.BranchId == id).FirstOrDefault();
            ViewBag.myAdd = myAddress.Description;
            return View(model);
        }

        [HttpGet]
        public ActionResult AddBranch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBranch(Branch b)
        {
            db.Branches.Add(b);
            db.SaveChanges();
            return RedirectToAction("Index", "Branch");
        }



        [HttpGet]
        public ActionResult AddAddress()
        {
            var degerler = (from i in db.Branches.ToList()
                    select new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }
                ).ToList();
            ViewBag.dgr = degerler;

            return View();
        }

        [HttpPost]
        public ActionResult AddAddress(Address a)
        {
            var br = db.Branches.Where(m => m.Id == a.Branch.Id).FirstOrDefault();
            a.Branch = br;
            db.Addresses.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index", "Branch");
        }




        [HttpGet]
        public ActionResult AddBus()
        {
            var degerler = (from i in db.Branches.ToList()
                    select new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }
                ).ToList();
            ViewBag.dgr = degerler;

            return View();
        }

        [HttpPost]
        public ActionResult AddBus(BusTransportation bus)
        {
            var br = db.Branches.Where(m => m.Id == bus.Branch.Id).FirstOrDefault();
            bus.Branch = br;
            db.BusTransportations.Add(bus);
            db.SaveChanges();
            return RedirectToAction("Index", "Branch");
        }

        [HttpGet]
        public ActionResult AddSocial()
        {
            var degerler = (from i in db.Branches.ToList()
                    select new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }
                ).ToList();
            ViewBag.dgr = degerler;

            return View();
        }

        [HttpPost]
        public ActionResult AddSocial(SocialOpportunity social)
        {
            var br = db.Branches.Where(m => m.Id == social.Branch.Id).FirstOrDefault();
            social.Branch = br;
            db.SocialOpportunities.Add(social);
            db.SaveChanges();
            return RedirectToAction("Index", "Branch");
        }

    }
}