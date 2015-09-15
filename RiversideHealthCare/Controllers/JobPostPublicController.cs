using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class JobPostPublicController : Controller
    {

        JobPostClass objJobPost = new JobPostClass();
        public ActionResult Index()
        {
            ViewBag.categories = new JobPostClass().getJobCategories();
            var jobPosts = objJobPost.getAvailableJobPosts();
            return View(jobPosts);
        }

   

        //This is Details view. When user wants to see the all job posts
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

        //-------------------------//
        //-----Applicant Part------// 
        //-------------------------//

        ApplicantClass objJobApp = new ApplicantClass();
        public ActionResult ApplicantInsert(int id)
        {
            ViewBag.JobPost = new JobPostClass().getJobPostByID(id);
            return View();
        }

        //Insert new job post into table
        [HttpPost]
        public ActionResult ApplicantInsert( int id, JobApplicant japp, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {

            ViewBag.JobPost = new JobPostClass().getJobPostByID(id);
            if (ModelState.IsValid)
            {

               //Upload the resume and cover letter of applicant in to the server
                string resume = Path.GetFileName(file1.FileName);
                string cover_letter = Path.GetFileName(file2.FileName);
                string resume_ext = Path.GetExtension(file1.FileName);
                string cover_letter_ext = Path.GetExtension(file2.FileName);
                resume = DateTime.UtcNow.Ticks + resume;
                cover_letter = DateTime.UtcNow.Ticks + cover_letter;
                japp.resume_path = resume;
                japp.cover_letter_path = cover_letter;
                if (file1 != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/applicantInfo/resume/"), resume);
                    file1.SaveAs(path);
                }
                if (file2 != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/applicantInfo/cover_letter/"), cover_letter);
                    file2.SaveAs(path);
                }

                japp.JobPost_id = id;
                objJobApp.commitInsert(japp);
                return RedirectToAction("Index");
            }
               
            return View();

        }

    }
}
