using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class EventsClass
    {
        //CREATE DB OBJECT
        riversideLinqDataContext objEvents = new riversideLinqDataContext();
        //GET ALL EVENTS AND ORDER DESCENDING BY ID - MOST RECENT
        public IQueryable<events> getEvents()
        {
            var allEvents = objEvents.events.Select(x => x).OrderByDescending(x => x.ev_id);
            return allEvents;
        }
        //GET ALL EVENTS AND ORDER ASCENDING BY DATE
        public IQueryable<events> getEventsByDate()
        {
            var eventsByDate = objEvents.events.Select(x => x).OrderBy(x => x.ev_date);
            return eventsByDate;
        }
        //GET ALL EVENTS AND ORDER ASCENDING BY DATE - SELECT TOP 5
        public IQueryable<events> getFeaturedEvents()
        {
            var FeaturedEvents = objEvents.events.Select(x => x).OrderBy(x => x.ev_date).Take(5);
            return FeaturedEvents;
        }
        //GET EVENTS BY ID - FOR UPDATE
        public events getEventById(int _ev_id)
        {
            var EventDetail = objEvents.events.SingleOrDefault(x => x.ev_id == _ev_id);
            return EventDetail;
        }
        //DELETE
        public bool commitDelete(int _ev_id)
        {

            using (objEvents)
            {

                var objDeleteEvent = objEvents.events.Single(x => x.ev_id == _ev_id);
                objEvents.events.DeleteOnSubmit(objDeleteEvent);
                objEvents.SubmitChanges();
                return true;
            }
        }
        //INSERT
        public bool commitInsert(events ev)
        {
            using (objEvents)
            {
                objEvents.events.InsertOnSubmit(ev);
                objEvents.SubmitChanges();
                return true;
            }
        }
        //UPDATE
        public bool commitUpdate(int _ev_id, string _ev_title, DateTime _ev_date, string _ev_location, string _ev_desc)
        {

            using (objEvents)
            {

                var objUpEvent = objEvents.events.Single(x => x.ev_id == _ev_id);
                objUpEvent.ev_id = _ev_id;
                objUpEvent.ev_title = _ev_title;
                objUpEvent.ev_date = _ev_date;
                objUpEvent.ev_location = _ev_location;
                objUpEvent.ev_desc = _ev_desc;

                objEvents.SubmitChanges();
                return true;
            }
        }
    }
}