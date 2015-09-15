using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiversideHealthCare.Models;
using System.Transactions;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using RiversideHealthCare.Filters;

namespace RiversideHealthCare.Controllers
{
    public class EventsAdminController : Controller
    {

        EventsClass objEvent = new EventsClass();

        [Authorize(Roles = "admin")]
        public ActionResult EventsAdminIndex()
        {
            var AllEvents = objEvent.getEvents();
            return View(AllEvents);
        }

        [Authorize(Roles = "admin")]
        public ActionResult EventDetail(int id)
        {

            var EventDetail = objEvent.getEventById(id);
            if (EventDetail == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(EventDetail);
            }
        }

        //---------Admin Insert -----------//

        [Authorize(Roles = "admin")]
        public ActionResult InsertEvent()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult InsertEvent(events Events)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objEvent.commitInsert(Events);
                    return RedirectToAction("EventsAdminIndex");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult UpdateEvent(int id)
        {
            var Event = objEvent.getEventById(id);
            return View(Event);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEvent(int id, events Event)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objEvent.commitUpdate(id, Event.ev_title, Event.ev_date, Event.ev_location, Event.ev_desc);
                    return RedirectToAction("EventsAdminIndex");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }


        //---------Admin Delete -----------//

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult EventDelete(int id, events Event)
        {
            try
            {
                objEvent.commitDelete(id);
                return RedirectToAction("EventsAdminIndex");
            }
            catch
            {
                return View("EventsAdminIndex");
            }

        }

    }
}