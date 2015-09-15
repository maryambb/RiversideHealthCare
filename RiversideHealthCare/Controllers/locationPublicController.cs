using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiversideHealthCare.Models;
namespace RiversideHealthCare.Controllers
{
    public class locationPublicController : Controller
    {
        //Creates an object of the locationPublic model
        locationPublic objLoc = new locationPublic();
        public ActionResult Index(int id = 1)
        {
            //first time visit, takes id as 1 for initial location
            //loads second time, return the new location bases on id
            var obj = objLoc.getLocationById(id);
            return View(obj);
        }

    }
}
