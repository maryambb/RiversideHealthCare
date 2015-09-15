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
    public class faqAdminController : Controller
    {
        //Creates an object of the faqAdmin model
        faqAdmin obj = new faqAdmin();

        //displays all FAQs
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var qa = obj.getFAQs();
            return View(qa);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            //gets the id from the user and displays the answer
            var qa = obj.getFAQsByID(id);
            if (qa == null)
            {
                return View("Notfound");
            }
            else
            {
                return View(qa);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Insert(faq qa)
        {
            //Inserting into the table
            if (ModelState.IsValid)
            {
                try
                {
                    obj.commitInsert(qa);
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
            var qa = obj.getFAQsByID(id);
            if (qa == null)
            {
                return View("Notfound");
            }
            else
            {
                return View(qa);
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Update(int id, faq qa)
        {
            //updating a faq based on id
            if (ModelState.IsValid)
            {
                try
                {
                    obj.commitUpdate(id, qa.question, qa.answer);
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
            var qa = obj.getFAQsByID(id);
            if (qa == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(qa);
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id, faq qa)
        {
            //deletes an faq based on id
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
            //Not Found Page
            return View();
        }
    }
}
