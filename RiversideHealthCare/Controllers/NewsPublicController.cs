using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiversideHealthCare.Models;
using PagedList;
using PagedList.Mvc;


namespace RiversideHealthCare.Controllers
{
    public class NewsPublicController : Controller
    {
        NewsClass objNews = new NewsClass();

        public ActionResult _NewsIndex()
        {
            var News = objNews.getTopNews();
            return PartialView(News);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _AllNews(int? page)
        {
            var News = objNews.getNews();
            return PartialView(News.ToPagedList(page ?? 1, 3));
        }


        public ActionResult DetailIndex(int id)
        {
            return View();
        }

        public ActionResult _DetailNews(int id)
        {
            var NewsDetail = objNews.getNewsById(id);
            if (NewsDetail == null)
            {
                return PartialView("NotFound");
            }
            else
            {
                return PartialView(NewsDetail);
            }
        }
        
    }
}
