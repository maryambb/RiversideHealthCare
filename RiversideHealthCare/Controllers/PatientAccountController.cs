using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using RiversideHealthCare.Filters;
using RiversideHealthCare.Models;


namespace RiversideHealthCare.Controllers
{
    public class PatientAccountController : Controller
    {
        PatientClass objPat = new PatientClass();

        // GET: /PatientAccount/
        [Authorize(Roles = "patient")]
        public ActionResult Index()
        {
            string user_name = User.Identity.Name.ToString();
            ViewBag.categories = new JobPostClass().getJobCategories();
            return View(objPat.getPatientByUserName(user_name));
        }

        //
        // GET: /PatientAccount/PatientProfile
        [Authorize(Roles = "patient")]
        public ActionResult PatientProfile()
        {
            string user_name = User.Identity.Name.ToString();
            return View(objPat.getPatientByUserName(user_name));
        }

        //
        // GET: /PatientAccount/UpdatePatient
        [Authorize(Roles = "patient")]
        public ActionResult UpdatePatient(int id)
        {

            //Get the patient from database based on the selected doctor id
            var objPatient = objPat.getPatientByID(id);
            if (objPatient == null)
            {
                return View("NotFound");
            }
            else
            {
                UpdatePatientModel objUpPat = new UpdatePatientModel();
                

                objUpPat.health_card = objPatient.health_card;
                objUpPat.first_name = objPatient.first_name;
                objUpPat.last_name = objPatient.last_name;
                objUpPat.birth_date = objPatient.birth_date;
                objUpPat.gender = objPatient.gender;
                objUpPat.email = objPatient.email;
                objUpPat.phone = objPatient.phone;
                objUpPat.address = objPatient.address;
                objUpPat.city = objPatient.city;
                objUpPat.province = objPatient.province;
                objUpPat.postal_code = objPatient.postal_code;
                
                return View(objUpPat);
            }
        }

        //Upadet the information of patient (patient can update it through her/his account)
        // POST: /PatientAccount/UpdatePatient
        [HttpPost]
        [Authorize(Roles = "patient")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePatient(int id, UpdatePatientModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    riversideLinqDataContext objLinq = new riversideLinqDataContext();
                    patient objPatient = objLinq.patients.Single(x => x.id == id);
                    objPatient.health_card = model.health_card;
                    objPatient.first_name = model.first_name;
                    objPatient.last_name = model.last_name;
                    objPatient.birth_date = model.birth_date;
                    objPatient.gender = model.gender;
                    objPatient.email = model.email;
                    objPatient.phone = model.phone;
                    objPatient.address = model.address;
                    objPatient.city = model.city;
                    objPatient.province = model.province;
                    objPatient.postal_code = model.postal_code;
                    

                    objLinq.SubmitChanges();

                    return RedirectToAction("PatientProfile");
                }
                catch
                {
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }
}
