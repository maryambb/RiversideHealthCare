using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class FeedbackPublicController : Controller
    {
        //CREATE NEW FEEDBACK CLASS OBJECT
        FeedbackClass objFeed = new FeedbackClass();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _FeedbackPublicForm()
        {
            return PartialView();
        }

        //--------FORM SUBMISSION POST------------//
        [HttpPost]
        public ActionResult _FeedbackPublicForm(feedback Feed)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objFeed.submitForm(Feed);
                    //DISPLAYS IN CONFIRM BOX WHEN FEEDBACK SUBMIT IS SUCCESSFUL    
                    @ViewBag.message = "Thank you " + Feed.fb_fname + " " + Feed.fb_lname + ", your feedback has been submitted! ";
                    return View();
                }
                catch
                {
                    //IF FORM SUBMISSION NOT SUCCESSFUL RETURN FORM VIEW
                    return View();
                }
            }
            return View();
        }

    }
}
