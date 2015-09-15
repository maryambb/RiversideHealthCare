using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using RiversideHealthCare.Filters;
using RiversideHealthCare.Models;
using System.IO;

using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class VolunteerAdminController : Controller
    {

        //-------------------------//
        //-----Volunteer Part------// 
        //-------------------------//

        VolunteerClass objVol = new VolunteerClass();

        [Authorize(Roles = "admin")]
        public ActionResult VolunteerIndex()
        {
            var vol = objVol.getVolunteers();
            return View(vol);
        }

        // GET: /Admin/Details
        [Authorize(Roles = "admin")]
        public ActionResult VolunteerDetails(int Id)
        {

            var vol = objVol.getVolunteerById(Id);
            if (vol == null)
            {
                return View("Error");
            }
            else
            {
                return View(vol);

            }
        }


        // GET: /volunteer/Insert
        [Authorize(Roles = "admin")]
        public ActionResult VolunteerInsert()
        {
            return View();
        }


        // POST: /volunteer/Insert
        // To handle file upload
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult VolunteerInsert(volunteer vol)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    objVol.commitInsert(vol);
                    return RedirectToAction("VolunteerIndex"); //On sucessful insert, redirect to the index view
                }
                catch
                {
                    //Error handling, return to  view if something goes wrong
                    return View();
                }
            }
            return View();

        }
        // GET: /updated
        [Authorize(Roles = "admin")]
        public ActionResult VolunteerUpdate(int Id)
        {

            var vol = objVol.getVolunteerById(Id);
            if (vol == null)
            {
                return View("Error");
            }
            return View(vol);
        }

        // POST: /Update
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult VolunteerUpdate(int Id, volunteer vol)
        {
            //If all the input were valid , then database will be updated
            if (ModelState.IsValid)
            {
                try
                {
                    objVol.commitUpdate(Id, vol.position, vol.location, vol.requirement);
                    return RedirectToAction("VolunteerIndex");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: /Delete
        [Authorize(Roles = "admin")]
        public ActionResult VolunteerDelete(int Id)
        {

            //It shows Delete view according to the selected id
            var vol = objVol.getVolunteerById(Id);
            if (vol == null)
            {
                return View("Error");
            }
            return View(vol);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult VolunteerDelete(int Id, volunteer vol)
        {
            //Selected value will be deleted from the database
            try
            {
                objVol.commitDelete(Id);
                return RedirectToAction("VolunteerIndex");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "admin")]
        public ActionResult Error()
        {
            //shows the Error view
            return View();
        }

        //-------------------------//
        //-----Volunteer Application Part------// 
        //-------------------------//

        VolunteerApplicationClass objApp = new VolunteerApplicationClass();
        [Authorize(Roles = "admin")]
        public ActionResult Applications()
        {
            var applications = objApp.getApplications();
            return View(applications);
        }

        //Passign the id for getting volunteer application
        [Authorize(Roles = "admin")]
        public ActionResult passID(int id)
        {

            var vol = objVol.getVolunteerById(id);
            if (vol == null)
            {
                return View("NotFound");
            }
            else
            {
                TempData["id"] = id;
                return RedirectToAction("AppDetails");
            }
        }
        //This is Details view. When user wants to see the all applications
        [Authorize(Roles = "admin")]
        public ActionResult AppDetails(volunteer_application_form app)
        {
            var oppID = (int)TempData["id"];

            var vol = objVol.getApplications(oppID);
            if (vol == null)
            {
                return View("Error");
            }
            else
            {

                return View(vol);

            }
        }


        //To view the application by giving the id
        [Authorize(Roles = "admin")]
        public ActionResult VolListById(int id)
        {
            var vol = objApp.getVolListById(id);
            if (vol == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(vol);
            }
        }

        //Delete a particular application according to chosen id
        [Authorize(Roles = "admin")]
        public ActionResult DeleteApplication(int id)
        {
            var vol = objApp.getVolListById(id);
            if (vol == null)
            {
                return View("NotFound");
            }
            else
            {
                TempData["delID"] = id;
                return View(vol);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteApplication()
        {
            int Id = (int)TempData["delID"];
            //Selected value will be deleted from the database
            try
            {
                objApp.commitDelete(Id);
                return RedirectToAction("VolunteerIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}
