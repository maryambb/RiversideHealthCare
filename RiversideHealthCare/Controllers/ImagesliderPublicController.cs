using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class ImagesliderPublicController : Controller
    {
        ImagesliderClass ImagesliderObject = new ImagesliderClass();

        //display imageslider at index file with sliding images
        public ActionResult ImagesliderPublic()
        {
            var img = ImagesliderObject.getImages();
            return PartialView(img);
        }

    }
}
