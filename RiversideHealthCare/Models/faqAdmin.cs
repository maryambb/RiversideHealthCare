using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class faqAdmin
    {
        riversideLinqDataContext obj = new riversideLinqDataContext();

        public IQueryable<faq> getFAQs()
        {
            //select statement for all faqs from db
            var allFAQs = from x in obj.faqs select x;
            return allFAQs;
        }
        public faq getFAQsByID(int _id)
        {
            //select statement for specific faq
            var allFAQsById = obj.faqs.SingleOrDefault(x => x.id == _id);
            return allFAQsById;
        }

        public bool commitInsert(faq qa)
        {
            using (obj)
            {
                //inserting into database
                obj.faqs.InsertOnSubmit(qa);
                obj.SubmitChanges();
                return true;
            }
        }
        public bool commitUpdate(int _id, string _question, string _answer)
        {
            using (obj)
            {
                //updating faq
                var objUpd = obj.faqs.Single(x => x.id == _id);
                objUpd.question = _question;
                objUpd.answer = _answer;
                obj.SubmitChanges();
                return true;
            }
        }
        public bool commitDelete(int _id)
        {
            using (obj)
            {
                //deleting faq
                var objDel = obj.faqs.Single(x => x.id == _id);
                obj.faqs.DeleteOnSubmit(objDel);
                obj.SubmitChanges();
                return true;
            }
        }
    }
}