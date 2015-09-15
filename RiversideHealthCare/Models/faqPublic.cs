using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class faqPublic
    {
        riversideLinqDataContext obj = new riversideLinqDataContext();

        public IQueryable<faq> getFaqs()
        {
            //select statement for all faqs from db
            var allFaqs = from x in obj.faqs select x;
            return allFaqs;
        }
        public faq getFaqsById(int _id)
        {
            //select statement for specific faq
            var faqsById = obj.faqs.SingleOrDefault(x => x.id == _id);
            return faqsById;
        }
    }
}