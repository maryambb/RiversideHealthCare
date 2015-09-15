using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class VolunteerPublicController : Controller
    {
        //Instantiates an object of the VolunteerClass
        VolunteerClass objVol = new VolunteerClass();


        public ActionResult VolunteerIndex()
        {
            var vol = objVol.getVolunteers();
            return View(vol);
        }

        public ActionResult passID(int id)
        {

            var vol = objVol.getVolunteerById(id);
            if (vol == null)
            {
                return View("NotFound");
            }
            else
            {
                //passing id to view using tempdata
                TempData["id"] = id;
                return RedirectToAction("VolunteerInsert");
            }
        }

        public ActionResult VolunteerInsert()
        {
            return View();
        }

        //Inserts form into table
        [HttpPost]
        public ActionResult VolunteerInsert(volunteer_application_form vol)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    vol.oppID = (int)TempData["id"];
                    objVol.commitVoInsert(vol);
                    return RedirectToAction("VolunteerIndex"); //On sucessful insert, redirect to the index view
                }
                catch
                {
                    //Error handling, return to Donation view if something goes wrong
                    return View();
                }
            }
            return View();

        }

    }
}
