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
    public class DonationAdminController : Controller
    {
        //Instantiates an object of the donationClass
        DonationClass objDonAdmin = new DonationClass();
       
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var donations = objDonAdmin.getDonations();
            return View(donations);
        }

        //This is Details view. When admin wants to see the all donation records
        [Authorize(Roles = "admin")]
        public ActionResult DonationDetails(int id)
        {
            var donate = objDonAdmin.getDonationByID(id);
            if (donate == null)
            {
                return View("NotFound");
                
            }
            else
            {
                return View(donate);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult DonationInsert()
        {
            ViewBag.branches = new DonationClass().getBranches();
            return View();
        }

        //Inserts donation form into table
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DonationInsert(Donation don)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    objDonAdmin.commitInsert(don);
                    return RedirectToAction("Index"); //On sucessful insert, redirect to the index view
                }
                catch
                {
                    //Error handling, return to Donation view if something goes wrong
                    return View();
                }
            }
            return View();

        }


        [Authorize(Roles = "admin")]
        public ActionResult DonationUpdate(int id)
        {
            //It will show the edit page according to the selected donation
            var donation = objDonAdmin.getDonationByID(id);
            ViewBag.branches = new DonationClass().getBranches();
            if (donation == null)
            {
                return View("NotFound");
            }
            return View(donation);
        }

        
        //Update the information of donor
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DonationUpdate(int id, Donation don)
        {
            //If all the input were valid , then database will be updated
            if (ModelState.IsValid)
            {
                try
                {
                    objDonAdmin.CommitUpdate(id, don.first_name, don.last_name, don.country, don.province, don.city, don.city, don.email, don.phone, don.branch_id, don.date, don.amount);
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
        public ActionResult DonationDelete(int id)
        {
            var don = objDonAdmin.getDonationByID(id);
            if (don == null)
            {
                return View("NotFound");
            }
            return View(don);
        }

        //Delete donation form table
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DonationDelete(int id, Donation don)
        {

            try
            {
                objDonAdmin.commitDelete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

    }
}
