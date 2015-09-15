using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class HomeController : Controller
    {
        locationPublic objLoc = new locationPublic();
        public HomeController()
        {
            ViewData["locations"] = objLoc.getLocations();
        }
        public ActionResult Index(int id = 1)
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            //Load for first time takes optional parameter id=1
            //Load for 2nd time takes the id from user
            var obj = objLoc.getLocationById(id);
            return View(obj);
        }
        public ActionResult Map(int id = 1)
        {
            //Load for first time takes optional parameter id=1
            //Load for 2nd time takes the id from user
            var obj = objLoc.getLocationById(id);
            return View(obj);
        }
    }
}
