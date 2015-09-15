using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class AddContentClass
    {
        //creating instance of linq object
        riversideLinqDataContext objContent = new riversideLinqDataContext();

        public IQueryable<Navigation> getNewPage()
        {
            var allPages = objContent.Navigations.Select(x => x);
            return allPages;
        }

        public Navigation getNewPagesById(int _id)
        {
            var allContent = objContent.Navigations.SingleOrDefault(x => x.id == _id);
            return allContent;
        }

        //insert
        public bool addPage(Navigation nav)
        {
            using (objContent)
            {
                objContent.Navigations.InsertOnSubmit(nav);
                //commit insert with db
                objContent.SubmitChanges();
                return true;
            }
        }

        //update
        public bool updatePage(int _id, string _title, string _subtitle, string _intro, string _desc, string _contentText, string _contentImage)
        {
            using (objContent)
            {
                var objUpdatePage = objContent.Navigations.Single(x => x.id == _id);
                //setting table cols to new values
                objUpdatePage.title = _title;
                objUpdatePage.subtitle = _subtitle;
                objUpdatePage.intro = _intro;
                objUpdatePage.description = _desc;
                objUpdatePage.content_text = _contentText;
                objUpdatePage.content_image = _contentImage;
                //commit update against db
                objContent.SubmitChanges();
                return true;
            }
        }

        //delete
        public bool deletePage(int _id)
        {
            using (objContent)
            {
                var objDeletePage = objContent.Navigations.Single(x => x.id == _id);
                //deleting from table
                objContent.Navigations.DeleteOnSubmit(objDeletePage);
                //committing delete with db
                objContent.SubmitChanges();
                return true;
            }
        }


    }
}