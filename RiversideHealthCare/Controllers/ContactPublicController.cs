using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class ContactPublicController : Controller
    {
        //Instantiates an object of the VolunteerClass
        ContactClass objContact = new ContactClass();


        public ActionResult ContactIndex()
        {
            var dataObj = objContact.getContacts();
            return View(dataObj);
        }

        //This is Details view. When user wants to see the all information
        public ActionResult ContactDetail(int id)
        {
            var contact = objContact.getContactsById(id);
            if (contact == null)
            {
                return View("Error");
                //same as saying return Error();
            }
            else
            {
                return View(contact);
            }
        }

    }
}
