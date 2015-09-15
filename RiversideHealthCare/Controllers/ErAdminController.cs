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
    public class ErAdminController : Controller
    {
        ErAdmin obj = new ErAdmin();

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            ViewBag.currentTime = obj.getCurrentTime();
            var listOfPaitents = obj.getPaitents();
            return View(listOfPaitents);
        }

        [Authorize(Roles = "admin")]
        public ActionResult displayClock()
        {
            var time = obj.getCurrentTime();
            return View(time);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            //gets the id from the user and displays of the paitent
            var paitent = obj.getPaitentByID(id);
            if (paitent == null)
            {
                return View("Notfound");
            }
            else
            {
                return View(paitent);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Insert(ERWaitQueue p)
        {
            //Inserting into the table
            if (ModelState.IsValid)
            {
                try
                {
                    // obj.commitUpdateTime(p);
                    obj.commitInsertQueue(p);
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
            var p = obj.getPaitentByID(id);
            if (p == null)
            {
                return View("Notfound");
            }
            else
            {
                return View(p);
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Update(int id, ERWaitQueue p)
        {
            //updating a paitent based on id
            if (ModelState.IsValid)
            {
                try
                {
                    obj.commitUpdateQueue(id, p.fName, p.lName, p.condition);
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
            var p = obj.getPaitentByID(id);
            if (p == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(p);
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id, ERWaitQueue p)
        {
            //deletes a paitent based on id
            try
            {
                obj.commitDeleteQueue(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult UpdateTimer()
        {
            var p = obj.getTimeById( 1);
            if (p == null)
            {
                return View("Notfound");
            }
            else
            {

                return View(p);
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateTimer(int id, ERCurrentTime t)
        {
            //updating a time based on id
            if (ModelState.IsValid)
            {
                try
                {
                    obj.commitUpdateTime((TimeSpan)t.waitTime, 1);
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
        public ActionResult Notfound()
        {
            //Not Found Page
            return View();
        }
    }
}
