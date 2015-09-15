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
    public class DoctorAccountController : Controller
    {
        DoctorClass objDoc = new DoctorClass();
        departmentAndBranchClass objDept = new departmentAndBranchClass();

        //
        // GET: /DoctorAccount/ --> Index is the first page after doctor login
        [Authorize(Roles = "doctor")]
        public ActionResult Index()
        {
            string username = User.Identity.Name.ToString();
            return View(objDoc.getDoctorByUserName(username));
        }

        //
        // GET: /DoctorAccount/ ---> doctor profile 
        [Authorize(Roles = "doctor")]
        public ActionResult DoctorProfile()
        {
            string username = User.Identity.Name.ToString();
            return View(objDoc.getDoctorByUserName(username));
        }

        //
        // GET: /DoctorAccount/UpdateDoctor
        [Authorize(Roles = "doctor")]
        public ActionResult UpdateDoctor(int id)
        {
            ViewBag.doctorId = id;
            //Get the department list from dapartment table and pass it to view using viewbag
            ViewBag.DepartmentList = objDept.getDepartmentList();

            //Get the doctor from database based on the selected doctor id
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


        //
        // POST: /DoctorAccount/UpdateDoctor
        [HttpPost]
        [Authorize(Roles = "doctor")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDoctor(int id, UpdateDoctorModel model, HttpPostedFileBase image)
        {
            ViewBag.doctorId = id;
            if (ModelState.IsValid)
            {
                try
                {
                    riversideLinqDataContext objLinq = new riversideLinqDataContext();
                    doctor objDoctor = objLinq.doctors.Single(x => x.id == id);

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

                    objDoctor.department_name = model.department_name;
                    objDoctor.first_name = model.first_name;
                    objDoctor.last_name = model.last_name;
                    objDoctor.email = model.email;
                    objDoctor.phone = model.phone;
                    objDoctor.specialty = model.specialty;
                    objDoctor.bio = model.bio;
                 

                    objLinq.SubmitChanges();

                    return RedirectToAction("DoctorProfile");
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
