using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiversideHealthCare.Models;
namespace RiversideHealthCare.Controllers
{
    public class ErPublicController : Controller
    {
        //Creates an object of the ERPublic model
        ErPublic objER = new ErPublic();
        //Creates an object of the ERCurrentTime model
        ERCurrentTime currentTime = new ERCurrentTime();

        public ActionResult Index()
        {
            //Displays the time
            var obj = objER.getCurrentTime(1);
            return View(obj.FirstOrDefault());
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(ERWaitQueue paitent)
        {
            //Allows the user to input into queue
            if (ModelState.IsValid)
            {
                try
                {
                    objER.commitInsert(paitent);

                    return Redirect("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

    }
}
