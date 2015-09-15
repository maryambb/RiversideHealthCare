using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using System.Data.Linq;

namespace RiversideHealthCare.Models
{
    public class ApplicantClass
    {

        // creating an instance of linq object
        riversideLinqDataContext objApp = new riversideLinqDataContext();
        public IQueryable<JobApplicant> getApplicants()
        {
            //variable with its value being the instance of linq object 
            var allApplicants = objApp.JobApplicants.Select(x => x);
            //return IQueryable<JobPost> for data bound control to bind to return allJobPosts
            return allApplicants;
        }

        public JobApplicant getApplicantByID(int _id)
        {
            var allApplicant = objApp.JobApplicants.SingleOrDefault(x => x.id == _id);
            return allApplicant;
        }

        //creating an instance of JobApplicant table (Model) as a parameter
        public bool commitInsert(JobApplicant japp)
        {
            //to ensure all data will be disposed of when finished
            using (objApp)
            {
                //using our model to set table columns to new values being passed and providing it to the insert command
                objApp.JobApplicants.InsertOnSubmit(japp);
                objApp.SubmitChanges();
                return true;
            }
        }

        public bool CommitUpdate(int _id, string _first_name, string _last_name, string _email, string _phone, string _status, string _description)
        {
            //Update table according to the parameters
            using (objApp)
            {
                var objUpApp = objApp.JobApplicants.Single(x => x.id == _id);
                objUpApp.first_name = _first_name;
                objUpApp.last_name = _last_name;
                objUpApp.email = _email;
                objUpApp.phone = _phone;
                objUpApp.status = _status;
                objUpApp.description = _description;

                objApp.SubmitChanges();
                return true;
            }
        }
        public bool commitDelete(int _AppId)
        {
            using (objApp)
            {
                var objDelJobPost = objApp.JobApplicants.Single(x => x.id == _AppId);
                objApp.JobApplicants.DeleteOnSubmit(objDelJobPost);
                objApp.SubmitChanges();
                return true;
            }
        }

    }
}