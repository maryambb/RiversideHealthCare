using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class FindDoctorPublicController : Controller
    {
        //Instantiates an object of the VolunteerClass
        FindDoctorClass objDoctor = new FindDoctorClass();


        public ActionResult doctorIndex()
        {
            return View();
        }


        //Inserts form into table
        [HttpPost]
        public ActionResult doctorIndex(FormCollection form)
        {
            string speciality = form["speciality"];

            TempData["doc"] = objDoctor.getdoctorsBySpeciality(speciality);

            return RedirectToAction("DoctorFound");
        }

        //to view the doctor detail information when the find button clicked
        public ActionResult DoctorFound()
        {
            if (TempData["doc"] == null)
                ViewBag.Doc = null;
            else
                ViewBag.Doc = 1;
            return View(TempData["doc"]);
        }

    }
}
