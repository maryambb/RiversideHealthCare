using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class faqPublicController : Controller
    {
        //Creates an object of the faqPublic model
        faqPublic objFAQ = new faqPublic();

        public ActionResult Index()
        {
            //Displays the all the FAQs
            var obj = objFAQ.getFaqs();
            return View(obj);
        }
        public ActionResult details(int id)
        {
            //Displays the speific answer
            var answer = objFAQ.getFaqsById(id);
            return View(answer);
        }
    }
}
