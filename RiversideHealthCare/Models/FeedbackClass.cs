using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{

    public class FeedbackClass
    {
        riversideLinqDataContext objFeed = new riversideLinqDataContext();

        public IQueryable<feedback> getFeedback()
        {

            var allFeedback = objFeed.feedbacks.Select(x => x).OrderByDescending(x => x.fb_id);
            return allFeedback;
        }

        public feedback getFeedbackById(int _fb_id)
        {
            var allFeedback = objFeed.feedbacks.SingleOrDefault(x => x.fb_id == _fb_id);
            return allFeedback;
        }

        public bool submitForm(feedback Feed)
        {
            using (objFeed)
            {
                objFeed.feedbacks.InsertOnSubmit(Feed);
                objFeed.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int _fb_id)
        {
            using (objFeed)
            {
                var objDeleteFeed = objFeed.feedbacks.Single(x => x.fb_id == _fb_id);
                objFeed.feedbacks.DeleteOnSubmit(objDeleteFeed);
                objFeed.SubmitChanges();
                return true;
            }
        }
    }
}