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

namespace RiversideHealthCare.Controllers
{
    public class locationAdminController : Controller
    {
        //Creates an object of the locationAdmin model
        locationAdmin obj = new locationAdmin();
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            //displays all Locations
            var location = obj.getLocations();
            return View(location);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            //gets the id from the user and displays the location
            var location = obj.getLocationsByID(id);
            if (location == null)
            {
                return View("Notfound");
            }
            else
            {
                return View(location);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Insert(location loc)
        {
            //Inserting into the table
            if (ModelState.IsValid)
            {
                try
                {
                    obj.commitInsert(loc);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Update(int id)
        {
            var location = obj.getLocationsByID(id);
            if (location == null)
            {
                return View("Notfound");
            }
            else
            {
                return View(location);
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Update(int id, location loc)
        {
            //updating a location based on id
            if (ModelState.IsValid)
            {
                try
                {
                    obj.commitUpdate(id, loc.name, loc.address, loc.postalCode, loc.country, loc.phone);
                    return RedirectToAction("Details/" + id);
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }


        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var location = obj.getLocationsByID(id);
            if (location == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(location);
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id, faq qa)
        {
            //deletes an location based on id
            try
            {
                obj.commitDelete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Notfound()
        {
            return View();
        }


    }
}
