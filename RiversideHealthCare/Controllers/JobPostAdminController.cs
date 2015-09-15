using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Transactions;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using RiversideHealthCare.Filters;
using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class JobPostAdminController : Controller
    {

        //-------------------------//
        //-----Job Post Part------// 
        //-------------------------//

        JobPostClass objJobPost = new JobPostClass();

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var jobPosts = objJobPost.getJobPosts();
            return View(jobPosts);
        }

       
        //This is Details view. When user wants to see the all job posts
        [Authorize(Roles = "admin")]
        public ActionResult JobPostDetails(int id)
        {
            var job = objJobPost.getJobPostByID(id);
            if (job == null)
            {
                return View("NotFound");
                //same as saying return NotFound();
            }
            else
            {
                return View(job);
            }
        }


        [Authorize(Roles = "admin")]
        public ActionResult JobPostInsert()
        {
            ViewBag.categories = new JobPostClass().getJobCategories();
            return View();
        }

        //Insert new job post into table
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult JobPostInsert(JobPost jpos)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    objJobPost.commitInsert(jpos);
                    return RedirectToAction("Index"); //On sucessful insert, redirect to the index view
                }
                catch
                {
                    //Error handling, return to Donation view if something goes wrong
                    return View();
                }
            }
            return View();

        }


        [Authorize(Roles = "admin")]
        public ActionResult JobPostUpdate(int id)
        {
            //It will show the edit page according to the selected donation
            ViewBag.categories = new JobPostClass().getJobCategories();
            var jobPost = objJobPost.getJobPostByID(id);
            if (jobPost == null)
            {
                return View("NotFound");
            }
            return View(jobPost);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult JobPostUpdate(int id, JobPost jpos)
        {
            //If all the input were valid , then database will be updated
            if (ModelState.IsValid)
            {
                try
                {
                    objJobPost.CommitUpdate(id, jpos.JobCategory_id, jpos.title, jpos.description, jpos.date, jpos.email, jpos.status);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult JobPostDelete(int id)
        {
            var jpos = objJobPost.getJobPostByID(id);
            if (jpos == null)
            {
                return View("NotFound");
            }
            return View(jpos);
        }

        //Delete job post form table
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult JobPostDelete(int id, JobPost jpos)
        {

            try
            {
                objJobPost.commitDelete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        //-------------------------//
        //-----Applicant Part------// 
        //-------------------------//

        
        ApplicantClass objJobApp = new ApplicantClass();
        [Authorize(Roles = "admin")]
        public ActionResult Applicants()
        {
            var applicants = objJobApp.getApplicants();
            return View(applicants);
        }

        //This is Details view. When user wants to see the all job posts
        [Authorize(Roles = "admin")]
        public ActionResult ApplicantDetails(int id)
        {
            var app = objJobApp.getApplicantByID(id);
            if (app == null)
            {
                return View("NotFound");
                //same as saying return NotFound();
            }
            else
            {
                return View(app);
            }
        }



        [Authorize(Roles = "admin")]
        public ActionResult ApplicantUpdate(int id)
        {
            //It will show the edit page according to the selected donation
            var jobApp = objJobApp.getApplicantByID(id);
            if (jobApp == null)
            {
                return View("NotFound");
            }
            return View(jobApp);
        }


        // Here is for updating one applicant
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicantUpdate(int id, JobApplicant japp)
        {
            //If all the input were valid , then database will be updated
            if (ModelState.IsValid)
            {
                try
                {
                    objJobApp.CommitUpdate(id, japp.first_name, japp.last_name, japp.email, japp.phone, japp.status, japp.description);
                    return RedirectToAction("Applicants");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult ApplicantDelete(int id)
        {
            var japp = objJobApp.getApplicantByID(id);
            if (japp == null)
            {
                return View("NotFound");
            }
            return View(japp);
        }

        //Delete applicant form table
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicantDelete(int id, JobApplicant japp)
        {

            try
            {

                
                // deleter resume and cover letter files of that specific applicant from server

                var resumeName = japp.resume_path;
                var coverLetterName = japp.cover_letter_path;

                string fullPath1 = Server.MapPath("~/Content/applicantInfo/resume/"
                + resumeName);

                string fullPath2 = Server.MapPath("~/Content/applicantInfo/cover_letter/"
                + coverLetterName);

                if (System.IO.File.Exists(fullPath1))
                {
                    System.IO.File.Delete(Server.MapPath("~/Content/applicantInfo/resume/"
                + resumeName));
                }
                if (System.IO.File.Exists(fullPath2))
                {
                    System.IO.File.Delete(Server.MapPath("~/Content/applicantInfo/cover_letter/"
               + coverLetterName));
                }

                // delete the information of applicant from table
                objJobApp.commitDelete(id);
                return RedirectToAction("Applicants");
            }
            catch
            {
                return View();
            }

        }


    }
}
