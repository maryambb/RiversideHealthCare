using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class NewsClass
    {
        //CREATE DB OBJECT
        riversideLinqDataContext objNews = new riversideLinqDataContext();

        public IQueryable<news> getNews()
        {
            //ORDER ALL NEWS BY MOST RECENT
            var allNews = objNews.news.Select(x => x).OrderByDescending(x => x.news_id);
            return allNews;
        }

        public news getNewsById(int _news_id)
        {
            var NewsDetail = objNews.news.SingleOrDefault(x => x.news_id == _news_id);
            return NewsDetail;
        }

        //SELECT 3 MOST RECENT NEWS POSTS
        public IQueryable<news> getTopNews()
        {
            IQueryable<news> allNews = objNews.news.Select(x => x);
            var topNews = allNews.OrderByDescending(x => x.news_id).Take(3);
            return topNews;
        }
        //INSERT
        public bool commitInsert(news News)
        {
            using (objNews)
            {
                objNews.news.InsertOnSubmit(News);
                objNews.SubmitChanges();
                return true;
            }
        }
        //UPDATE
        public bool commitUpdate(int _news_id, string _news_name, string _news_desc)
        {
            using (objNews)
            {
                var objUpNews = objNews.news.Single(x => x.news_id == _news_id);
                objUpNews.news_id = _news_id;
                objUpNews.news_name = _news_name;
                objUpNews.news_desc = _news_desc;

                objNews.SubmitChanges();
                return true;
            }
        }
        //DELETE
        public bool commitDelete(int _news_id)
        {
            using (objNews)
            {
                var objDeleteNews = objNews.news.Single(x => x.news_id == _news_id);
                objNews.news.DeleteOnSubmit(objDeleteNews);
                objNews.SubmitChanges();
                return true;
            }
        }
    }
}