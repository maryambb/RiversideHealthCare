using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using System.Data.Linq;

namespace RiversideHealthCare.Models
{
    public class JobPostClass
    {
        // creating an instance of linq object
        riversideLinqDataContext objJob = new riversideLinqDataContext();
        public IQueryable<JobPost> getJobPosts()
        {
            //variable with its value being the instance of linq object 
            var allJobPosts = objJob.JobPosts.Select(x => x);
            //return IQueryable<JobPost> for data bound control to bind to return allJobPosts
            return allJobPosts;
        }

        public IQueryable<JobPost> getAvailableJobPosts()
        {
            //variable with its value being the instance of linq object 
            var allJobPosts = objJob.JobPosts.Where(x => x.status == "Available").Select(x => x);
            //return IQueryable<JobPost> for data bound control to bind to return allJobPosts
            return allJobPosts;
        }

        //get job post by id
        public JobPost getJobPostByID(int _id)
        {
            var allJobPost = objJob.JobPosts.SingleOrDefault(x => x.id == _id);
            return allJobPost;
        }

        //get all job categories
        public IQueryable<JobCategory> getJobCategories()
        {
            var allJobCategories = objJob.JobCategories.Select(x => x);
            return allJobCategories;

        }

        //get job category by id
        public JobCategory getJobCategoryByID(int _int)
        {
            var allJobCategory = objJob.JobCategories.SingleOrDefault(x => x.id == _int);
            return allJobCategory;
        }

        //creating an instance of JobPost table (Model) as a parameter
        public bool commitInsert(JobPost jpos)
        {
            //to ensure all data will be disposed of when finished
            using (objJob)
            {
                //using our model to set table columns to new values being passed and providing it to the insert command
                objJob.JobPosts.InsertOnSubmit(jpos);
                objJob.SubmitChanges();
                return true;
            }
        }

        //Updating job post
        public bool CommitUpdate(int _id, int _JobCategory_id, string _title, string _description, DateTime _date, string _email, string _status)
        {
            //Update table according to the parameters
            using (objJob)
            {
                var objUpJob = objJob.JobPosts.Single(x => x.id == _id);
                objUpJob.JobCategory_id = _JobCategory_id;
                objUpJob.title = _title;
                objUpJob.description = _description;
                objUpJob.date = _date;
                objUpJob.email = _email;
                objUpJob.status = _status;

                objJob.SubmitChanges();
                return true;
            }
        }

        //deleting job post
        public bool commitDelete(int _jobPostId)
        {
            using (objJob)
            {
                var objDelJobPost = objJob.JobPosts.Single(x => x.id == _jobPostId);
                objJob.JobPosts.DeleteOnSubmit(objDelJobPost);
                objJob.SubmitChanges();
                return true;
            }
        }
    }
}