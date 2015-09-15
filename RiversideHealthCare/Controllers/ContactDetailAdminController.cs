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
    public class ContactDetailAdminController : Controller
    {
        ContactDetailClass objContact = new ContactDetailClass();

        [Authorize(Roles = "admin")]
        public ActionResult ContactDetailIndex()
        {
            var contact = objContact.getContacts();

            return View(contact);
        }

        // GET: /Admin/Details
        [Authorize(Roles = "admin")]
        public ActionResult ContactDetails(int Id)
        {

            var contact = objContact.getContactsById(Id);
            if (contact == null)
            {
                return View("Error");
            }
            else
            {
                return View(contact);

            }
        }


        // GET: /contactDetail/Insert
        [Authorize(Roles = "admin")]
        public ActionResult ContactDetailInsert()
        {
            return View();
        }


        // POST: /contact/Insert
        // To handle file upload
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult ContactDetailInsert(contact_detail contact)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    objContact.commitInsert(contact);
                    return RedirectToAction("ContactDetailIndex"); //On sucessful insert, redirect to the index view
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
        public ActionResult ContactDetailUpdate(int Id)
        {

            var contact = objContact.getContactsById(Id);
            if (contact == null)
            {
                return View("Error");
            }
            return View(contact);
        }

        // POST: /Update
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult ContactDetailUpdate(int Id, contact_detail contact)
        {
            //If all the input were valid , then database will be updated
            if (ModelState.IsValid)
            {
                try
                {
                    objContact.commitUpdate(Id, contact.name, contact.phone, (int)contact.department_id);
                    return RedirectToAction("ContactDetailIndex");
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
        public ActionResult ContactDetailDelete(int Id)
        {

            //It shows Delete view according to the selected id
            var contact = objContact.getContactsById(Id);
            if (contact == null)
            {
                return View("Error");
            }
            return View(contact);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult ContactDetailDelete(int Id, contact_detail contact)
        {
            //Selected value will be deleted from the database
            try
            {
                objContact.commitDelete(Id);
                return RedirectToAction("ContactDetailIndex");
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

    }
}