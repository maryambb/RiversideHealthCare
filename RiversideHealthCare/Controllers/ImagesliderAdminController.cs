using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class ImagesliderAdminController : Controller
    {

        ImagesliderClass objImg = new ImagesliderClass();

        [Authorize(Roles = "admin")]
        public ActionResult ImagesliderIndex()
        {
            // Getting all the images and returning them into the Index view for represent
            ViewBag.Message = "Image Slider Listing";
            var img = objImg.getImages();
            return View(img);
        }

        // GET: /Admin/Details
        [Authorize(Roles = "admin")]
        public ActionResult ImagesliderDetails(int ImageId)
        {

            //Get one image by it's id and if it is available , return it to Details view for represent ,else return error view 
            var img = objImg.getImagesById(ImageId);
            if (img == null)
            {
                return View("Error");
            }
            else
            {
                return View(img);

            }
        }


        // GET: /Insert
        [Authorize(Roles = "admin")]
        public ActionResult ImagesliderInsert()
        {
            return View();
        }


        // POST: /Insert
        // To handle file upload
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult ImagesliderInsert(Imageslider img, HttpPostedFileBase file)
        {
            //Will check all the data regarding their validation , and if they were valid ,they are inserted into database and then redirecting to the index view .
            /*this is for image upload*/
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/images/imageslider/")
                                                          + file.FileName);
                    img.Image = file.FileName;
                }
                objImg.commitInsert(img);
                return RedirectToAction("ImagesliderIndex");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    objImg.commitInsert(img);
                    return RedirectToAction("ImagesliderIndex");
                }
                catch
                {
                    return View(img);
                }
            }
            return View(img);
        }

        // GET: /updated
        [Authorize(Roles = "admin")]
        public ActionResult ImagesliderUpdate(int ImageId)
        {
            //It will show the edit page according to the selected image
            var img = objImg.getImagesById(ImageId);
            if (img == null)
            {
                return View("Error");
            }
            return View(img);
        }

        // POST: /Update
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult ImagesliderUpdate(int ImageId, Imageslider img, HttpPostedFileBase file)
        {
            //Will check all the data regarding their validation , and if they were valid ,they are updated into database and then redirecting to the index view .
            /*this is for image upload*/
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/images/imageslider/")
                                                          + file.FileName);
                    img.Image = file.FileName;
                }
                objImg.commitUpdate(ImageId, img.Name, img.Image, img.Comment, img.Link);
                return RedirectToAction("ImagesliderIndex");
            }

            return View();
        }

        // GET: /Delete
        [Authorize(Roles = "admin")]
        public ActionResult ImagesliderDelete(int ImageId)
        {

            //It shows Delete view according to the selected image
            var img = objImg.getImagesById(ImageId);
            if (img == null)
            {
                return View("Error");
            }
            return View(img);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult ImagesliderDelete(int ImageId, Imageslider img)
        {
            //Selected image will be deleted from the database
            try
            {
                objImg.commitDelete(ImageId);
                return RedirectToAction("ImagesliderIndex");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Error()
        {
            //shows the Error view
            return View();
        }

    }
}
