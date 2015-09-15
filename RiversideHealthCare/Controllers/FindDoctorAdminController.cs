using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using RiversideHealthCare.Filters;
using RiversideHealthCare.Models;
using System.IO;


namespace RiversideHealthCare.Controllers
{
    public class FindDoctorAdminController : Controller
    {
        FindDoctorClass objDoctor = new FindDoctorClass();

        [Authorize(Roles = "admin")]
        public ActionResult doctorIndex()
        {
            var doctor = objDoctor.getdoctors();
            return View(doctor);
        }

        // GET: /Admin/Details
        // to view all the details of find doctor table
        [Authorize(Roles = "admin")]
        public ActionResult doctorDetails(int Id)
        {

            var doctor = objDoctor.getDoctorsById(Id);
            if (doctor == null)
            {
                return View("Error");
            }
            else
            {
                return View(doctor);

            }
        }


        // GET: /doctor/Insert
        [Authorize(Roles = "admin")]
        public ActionResult doctorInsert()
        {
            return View();
        }


        // POST: /doctor/Insert
        // To handle file upload
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult doctorInsert(findDoctor doctor)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    objDoctor.commitInsert(doctor);
                    return RedirectToAction("doctorIndex"); //On sucessful insert, redirect to the index view
                }
                catch
                {
                    //Error handling, return to  view if something goes wrong
                    return View();
                }
            }
            return View();

        }
        // GET: /updated
        [Authorize(Roles = "admin")]
        public ActionResult doctorUpdate(int Id)
        {

            var doctor = objDoctor.getDoctorsById(Id);
            if (doctor == null)
            {
                return View("Error");
            }
            return View(doctor);
        }

        // POST: /Update
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult doctorUpdate(int Id, findDoctor doctor)
        {
            //If all the input were valid , then database will be updated
            if (ModelState.IsValid)
            {
                try
                {
                    objDoctor.commitUpdate(Id, doctor.fname, doctor.lname, doctor.speciality, doctor.branch_id, doctor.gender);
                    return RedirectToAction("doctorIndex");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: /Delete
        [Authorize(Roles = "admin")]
        public ActionResult doctorDelete(int Id)
        {

            //It shows Delete view according to the selected id
            var doctor = objDoctor.getDoctorsById(Id);
            if (doctor == null)
            {
                return View("Error");
            }
            return View(doctor);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult doctorDelete(int Id, findDoctor doctor)
        {
            //Selected value will be deleted from the database
            try
            {
                objDoctor.commitDelete(Id);
                return RedirectToAction("doctorIndex");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "admin")]
        public ActionResult Error()
        {
            //shows the Error view
            return View();
        }

    }
}