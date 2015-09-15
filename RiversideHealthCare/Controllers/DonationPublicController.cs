using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class DonationPublicController : Controller
    {
        //Instantiates an object of the donationClass
        DonationClass objDon = new DonationClass();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DonationInsert()
        {
            ViewBag.branches = new DonationClass().getBranches();
            //IEnumerable<Branch> branches = new DonationClass().getBranches();
            return View();
        }

        //Inserts donation form into table
        [HttpPost]
        public ActionResult DonationInsert(Donation don)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    //these part of code is for creating the url which redirect to paypal (connect us to paypal)
                    string url = "https://www.sandbox.paypal.com/cgi-bin/webscr";
                    string cmd = "?cmd=";
                    string value = "_donations";
                    string business = "&business=info@riverside.webelementz.com";
                    string itm = "&item_name=Donate to Riverside HealthCare";
                    string currency = "&currency_code=CAD";
                    string amt = "&amount=" + don.amount.ToString();

                    string path = url + cmd + value + business + itm + currency + amt;
                    
                    
                    // Inserting the donor information into db
                    objDon.commitInsert(don);
                    //After inserting, redirect to the paypal 
                    return Redirect(path); 
                }
                catch
                {
                    //Error handling, return to Donation view if something goes wrong
                    return View();
                }
            }
            return View();

        }
    }
}
