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
    public class AddContentAdminController : Controller
    {
        AddContentClass objContent = new AddContentClass();

        [Authorize(Roles = "admin")]
        public ActionResult AddContentIndex()
        {
            // Getting all the add content and returning them into the Index view for representation
            ViewBag.Message = "Add New Page";
            var nav = objContent.getNewPage();
            return View(nav);
        }

        // GET: Show how the inserted page looks
        [Authorize(Roles = "admin")]
        public ActionResult Template1(int id)
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

        // GET: /Insert
        [Authorize(Roles = "admin")]
        public ActionResult AddContentInsert()
        {
            return View();
        }


        // POST: /Insert
        // To handle file upload
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddContentInsert(Navigation nav, HttpPostedFileBase file)
        {
            //Will check all the data regarding their validation , and if they were valid ,they are inserted into database and then redirecting to the index view .
            /*this is for image upload*/
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/images/addcontent/")
                                                          + file.FileName);
                    nav.content_image = file.FileName;
                }
                objContent.addPage(nav);
                return RedirectToAction("AddContentIndex");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    objContent.addPage(nav);
                    return RedirectToAction("AddContentIndex");
                }
                catch
                {
                    return View(nav);
                }
            }
            return View(nav);
        }

        // GET: /updated
        [Authorize(Roles = "admin")]
        public ActionResult AddContentUpdate(int id)
        {
            //It will show the edit page according to the selection
            var nav = objContent.getNewPagesById(id);
            if (nav == null)
            {
                return View("Error");
            }
            return View(nav);
        }

        // POST: /Update
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddContentUpdate(int id, Navigation nav, HttpPostedFileBase file)
        {
            //Will check all the data regarding their validation , and if they were valid ,they are updated into database and then redirecting to the index view .
            /*this is for image upload*/
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/images/addcontent/")
                                                          + file.FileName);
                    nav.content_image = file.FileName;
                }
                objContent.updatePage(id, nav.title, nav.subtitle, nav.intro, nav.description, nav.content_text, nav.content_image);
                return RedirectToAction("AddContentIndex");
            }

            return View();
        }

        // GET: /Delete
        [Authorize(Roles = "admin")]
        public ActionResult AddContentDelete(int id)
        {

            //It shows Delete view according to the selected image
            var nav = objContent.getNewPagesById(id);
            if (nav == null)
            {
                return View("Error");
            }
            return View(nav);
        }

        //posting to server to delete selection
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddContentDelete(int id, Navigation nav)
        {
            //Selection will be deleted from the database
            try
            {
                objContent.deletePage(id);
                return RedirectToAction("AddContentIndex");
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
