using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RiversideHealthCare.Models;


namespace RiversideHealthCare.Controllers
{
    public class AddContentPublicController : Controller
    {
        AddContentClass objContent = new AddContentClass();

        public ActionResult Index()
        {
            return View();
        }

        //Setting partialview for navigation here
        public ActionResult _PartialNav()
        {
            var nav = objContent.getNewPage();
            if (nav == null)
            {
                return PartialView("Error");
            }
            else
            {
                return PartialView(nav);

            }
        }

        public ActionResult TemplatePublic(int id)
        {
            var nav = objContent.getNewPagesById(id);
            if (nav == null)
            {
                return View("Error");
            }
            else
            {
                return View(nav);

            }
        }

        public ActionResult Error()
        {
            //shows the Error view
            return View();
        }

    }
}
