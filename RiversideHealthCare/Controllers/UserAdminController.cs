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
    public class UserAdminController : Controller
    {
        
        
        PatientClass objPat = new PatientClass();
        DoctorClass objDoc = new DoctorClass();
        departmentAndBranchClass objDept = new departmentAndBranchClass();

        // GET: /Admin/First page
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/ListDoctor  ----> returning the list of doctors
        [Authorize(Roles = "admin")]
        public ActionResult ListDoctor()
        {
            return View(objDoc.getAllDoctors());
        }



        //
        // GET: /Admin/ListPatient  ----> returning the list of patients
        [Authorize(Roles = "admin")]
        public ActionResult ListPatient()
        {
            return View(objPat.getAllPatients());
        }

        //
        // GET: /Admin/DoctorDetail   ----> returning the details of specific doctor
        [Authorize(Roles = "admin")]
        public ActionResult DoctorDetail(int id)
        {
            return View(objDoc.getDoctorByID(id));
        }

        //
        // GET: /Admin/PatientDetail    ----> returning the details of specific patient
        [Authorize(Roles = "admin")]
        public ActionResult PatientDetail(int id)
        {
            return View(objPat.getPatientByID(id));
        }


        //
        // GET: /Admin/UpdateDoctor  ----> update information of specific doctor
        [Authorize(Roles = "admin")]
        public ActionResult UpdateDoctor(int id)
        {
            //Pass doctor id to view using viewbag
            ViewBag.doctor_id = id;
            //Get all departments from Dapartment table and pass it to view using viewbag
            ViewBag.DepartmentList = objDept.getDepartmentList();

            //Get specific doctor based on the selected doctor id
            var objDoctor = objDoc.getDoctorByID(id);
            if (objDoctor == null)
            {
                return View("NotFound");
            }
            else
            {
                UpdateDoctorModel objUpDoc = new UpdateDoctorModel();
                objUpDoc.department_name = objDoctor.department_name;
                objUpDoc.first_name = objDoctor.first_name;
                objUpDoc.last_name = objDoctor.last_name;
                objUpDoc.email = objDoctor.email;
                objUpDoc.phone = objDoctor.phone;
                objUpDoc.specialty = objDoctor.specialty;
                objUpDoc.bio = objDoctor.bio;
                objUpDoc.photo_path = objDoctor.photo_path;
                
                return View(objUpDoc);
            }
        }


        // Updating the information of doctor
        // POST: /Admin/UpdateDoctor
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDoctor(int id, UpdateDoctorModel udModel, HttpPostedFileBase image)
        {
            ViewBag.doctorId = id;
            if (ModelState.IsValid)
            {
                try
                {

                    riversideLinqDataContext objLinq = new riversideLinqDataContext();
                    doctor objDoctor = objLinq.doctors.Single(x => x.id == id);
                    // Update the image of doctor
                    if (image != null)
                    {
                        
                        //Delete old photo
                        var oldImage = objDoctor.photo_path;
                        string fullPath = Server.MapPath("~/Content/images/doctors/"
                        + oldImage);

                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(Server.MapPath("~/Content/images/doctors/"
                        + oldImage));

                        }
                        
                        //Upload new photo
                        string doctor_photo = Path.GetFileName(image.FileName);
                        string image_ext = Path.GetExtension(image.FileName);

                        doctor_photo = DateTime.UtcNow.Ticks + doctor_photo;
                        objDoctor.photo_path = doctor_photo;

                        string path = Path.Combine(Server.MapPath("~/Content/images/doctors/"), doctor_photo);
                        image.SaveAs(path);
                    }

                    objDoctor.department_name = udModel.department_name;
                    objDoctor.first_name = udModel.first_name;
                    objDoctor.last_name = udModel.last_name;
                    objDoctor.email = udModel.email;
                    objDoctor.phone = udModel.phone;
                    objDoctor.specialty = udModel.specialty;
                    objDoctor.bio = udModel.bio;
                   

                    objLinq.SubmitChanges();

                    return RedirectToAction("ListDoctor");
                }
                catch
                {
                    return View(udModel);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(udModel);
        }

        //
        // GET: /Admin/UpdatePatient
        [Authorize(Roles = "admin")]
        public ActionResult UpdatePatient(int id)
        {
            ViewBag.patientId = id;

            //Get specific patient based on the selected patient id
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


        //Update the information of patient
        // POST: /Admin/UpdatePatient
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePatient(int id, UpdatePatientModel model)
        {
            ViewBag.patientId = id;
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

                    //return RedirectToAction("PatientDetail/" + id);
                    return RedirectToAction("ListPatient");
                }
                catch
                {
                    return View(model);
                }
            }
            return View(model);
        }


        //
        // GET: /Admin/CreateDoctor
        [Authorize(Roles = "admin")]
        public ActionResult CreateDoctor()
        {
            //Get all departments from Dapartment table and pass it to view using viewbag
            ViewBag.DepartmentList = objDept.getDepartmentList();
            return View();
        }


        //
        // POST: /Admin/CreateDoctor
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDoctor(CreateDoctorModel model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {

                
                    doctor objDoctor = new doctor();
                   // Upload the image of doctor on server
                    if (image != null)
                    {
                        string doctor_photo = Path.GetFileName(image.FileName);
                        string image_ext = Path.GetExtension(image.FileName);

                        doctor_photo = DateTime.UtcNow.Ticks + doctor_photo;
                        objDoctor.photo_path = doctor_photo;

                        string path = Path.Combine(Server.MapPath("~/Content/images/doctors/"), doctor_photo);
                        image.SaveAs(path);
                    }
         
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);

                    //Add a role to the new user
                    Roles.AddUserToRole(model.UserName, "Doctor");

                    //Create doctor object based on user input
                    
                    objDoctor.user_name = model.UserName;
                    objDoctor.department_name = model.department_name;
                    objDoctor.first_name = model.first_name;
                    objDoctor.last_name = model.last_name;
                    objDoctor.email = model.email;
                    objDoctor.phone = model.phone;
                    objDoctor.specialty = model.specialty;
                    objDoctor.bio = model.bio;
                    
           

                    //Insert doctor object to database doctor table

                    objDoc.commitInsert(objDoctor);

                    return RedirectToAction("ListDoctor", "UserAdmin");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(model);
        }


        // GET: /Admin/CreatePatient
        [Authorize(Roles = "admin")]
        public ActionResult CreatePatient()
        {
            return View();
        }


        //
        // POST: /Admin/CreatePatent
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePatient(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {

                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    //Add a role to the new user
                    Roles.AddUserToRole(model.UserName, "patient");

                    //Create a patient object from user input

                    patient objPatient = new patient();
                    objPatient.user_name = model.UserName;
                    objPatient.health_card = model.health_card;
                    objPatient.first_name = model.first_name;
                    objPatient.last_name = model.last_name;
                    objPatient.birth_date = Convert.ToDateTime(model.bith_date);
                    objPatient.gender = model.gender;
                    objPatient.email = model.email;
                    objPatient.phone = model.phone;
                    objPatient.address = model.address;
                    objPatient.postal_code = model.postal_code;
                    objPatient.city = model.city;
                    objPatient.province = model.province;

                    //Insert patient into database patient table using patientClass
                    PatientClass objPat = new PatientClass();
                    objPat.commitInsert(objPatient);

                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult PatientDelete(int id)
        {
            var patient = objPat.getPatientByID(id);
            if (patient == null)
            {
                return View("NotFound");
            }
            return View(patient);
        }

        //Delete job post form table
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult PatientDelete(int id, patient pat)
        {
            var UserName = pat.user_name;
            try
            {
                objPat.commitDelete(id);
                Roles.RemoveUserFromRole(UserName, "patient");
                Membership.DeleteUser(UserName);
                return RedirectToAction("ListPatient");
            }
            catch
            {
                return View();
            }

        }

        [Authorize(Roles = "admin")]
        public ActionResult DoctorDelete(int id)
        {
            var doctor = objDoc.getDoctorByID(id);
            if (doctor == null)
            {
                return View("NotFound");
            }
            return View(doctor);
        }

        //Delete doctor form table and user information
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DoctorDelete(int id, doctor doc)
        {
            var UserName = doc.user_name;
            try
            {

                // Deleting the photo of this doctor
                var image = doc.photo_path;
                string fullPath = Server.MapPath("~/Content/images/doctors/"
                + image);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(Server.MapPath("~/Content/images/doctors/"
                + image));

                }
                
                // Deleting Doctor from doctor table and remove all profile and acount ...
                objDoc.commitDelete(id);
                Roles.RemoveUserFromRole(UserName, "doctor");
                Membership.DeleteUser(UserName);
                return RedirectToAction("ListDoctor");
            }
            catch
            {
                return View();
            }

        }







        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

    }
}
