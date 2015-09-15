using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class EventsPublicController : Controller
    {

        EventsClass objEvents = new EventsClass();

        public ActionResult _EventsPublicIndex()
        {
            var Events = objEvents.getFeaturedEvents();
            return PartialView(Events);
        }

        public ActionResult AllEvents()
        {
            var allEvents = objEvents.getEventsByDate();
            return View(allEvents);
        }

        public ActionResult EventDetail(int id)
        {
            var EventDetail = objEvents.getEventById(id);
            if (EventDetail == null)
            {
                return View("Notfound");
            }
            else
            {
                return View(EventDetail);
            }
        }
    }
}
