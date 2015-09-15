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
    public class ContactAdminController : Controller
    {
        //-------------------------//
        //-----Contact  Part------// 


        ContactClass objContact = new ContactClass();

        [Authorize(Roles = "admin")]
        public ActionResult ContactIndex()
        {
            var contact = objContact.getContacts();
            return View(contact);
        }

        // GET: /Admin/Details
        //This is Details view. When user wants to see the all contacts
        [Authorize(Roles = "admin")]
        public ActionResult ContactDetails(int Id)
        {

            var contact = objContact.getContactsById(Id);
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


        // GET: /contact/Insert
        //Insert new contact into table
        [Authorize(Roles = "admin")]
        public ActionResult ContactInsert()
        {
            return View();
        }


        // POST: /contact/Insert
        // To handle file upload
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult ContactInsert(contact_list contact)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    objContact.commitInsert(contact);
                    return RedirectToAction("ContactIndex"); //On sucessful insert, redirect to the index view
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
        public ActionResult ContactUpdate(int Id)
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
        public ActionResult ContactUpdate(int Id, contact_list contact)
        {
            //If all the input were valid , then database will be updated
            if (ModelState.IsValid)
            {
                try
                {
                    objContact.commitUpdate(Id, contact.department, contact.detail,contact.location, contact.phone, contact.fax, contact.head);
                    return RedirectToAction("ContactIndex");
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
        public ActionResult ContactDelete(int Id)
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
        public ActionResult ContactDelete(int Id, contact_list contact)
        {
            //Selected value will be deleted from the database
            try
            {
                objContact.commitDelete(Id);
                return RedirectToAction("ContactIndex");
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