using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiversideHealthCare.Models;
using System.Transactions;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using RiversideHealthCare.Filters;
//IO REQUIRED FOR IMG UPLOAD
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace RiversideHealthCare.Controllers
{
    public class NewsController : Controller
    {
        //NEWS CLASS OBJECT
        NewsClass objNews = new NewsClass();

        //---------ADMIN NEWS -----------//
        [Authorize(Roles = "admin")]
        public ActionResult NewsAdminIndex(int? page)
        {
            var News = objNews.getNews();
            return View(News.ToPagedList(page ?? 1, 3));
        }

        [Authorize(Roles = "admin")]
        public ActionResult DetailNews(int id)
        {

            var NewsDetail = objNews.getNewsById(id);
            if (NewsDetail == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(NewsDetail);
            }
        }

        //---------Admin Insert -----------//
        [Authorize(Roles = "admin")]
        public ActionResult InsertNews()
        {
            return View();
        }

        //POST - INSERT CLICKED
        //FILEBASE FILE OBJECT TO BRING FILE FROM BROWSER TO CONTROLLER
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult InsertNews(news News, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/news/") + file.FileName);
                    News.news_img = file.FileName;
                }
                objNews.commitInsert(News);
                return RedirectToAction("NewsAdminIndex");
            }

            return View(News);
        }

        //---------ADMIN UPDATE -----------//
        [Authorize(Roles = "admin")]
        public ActionResult UpdateNews(int id)
        {
            var News = objNews.getNewsById(id);
            return View(News);
        }

        //POST - UPDATE CLICKED
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateNews(int id, news News)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.commitUpdate(id, News.news_name, News.news_desc);
                    return RedirectToAction("NewsAdminIndex");
                }
                //IF ISSUE W. UPDATE DISPLAY UPDATE VIEW
                catch
                {
                    return View();
                }
            }
            return View();
        }


        //---------Admin Delete -----------//

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult NewsAdminDelete(int id, news News)
        {
            try
            {
                objNews.commitDelete(id);
                //IF DELETE = SUCCESS DISPLAY NEWS ADMIN INDEX
                return RedirectToAction("NewsAdminIndex");
            }
            catch
            {
                return View();
            }

        }

    }
}
