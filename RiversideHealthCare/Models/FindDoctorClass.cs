using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class FindDoctorClass
    {
        //creating instance of linq object
        riversideLinqDataContext objDoctor = new riversideLinqDataContext();

        public IQueryable<findDoctor> getdoctors()
        {
            //variable with its value being the instance of linq object
            var allDoctors = objDoctor.findDoctors.Select(x => x);
            //return IQueryable<findDoctor> for data bound control to bind to return allDoctors
            return allDoctors;
        }

        public IQueryable<findDoctor> getdoctorsBySpeciality(string spl)
        {
            //variable with its value being the instance of linq object
            var allDoctors = from x in objDoctor.findDoctors
                             where x.speciality == spl
                             select x;
            //return IQueryable<findDoctor> for data bound control to bind to return allDoctors
            return allDoctors;
        }


        public findDoctor getDoctorsById(int _doctorId)
        {
            var allDoctors = objDoctor.findDoctors.SingleOrDefault(x => x.Id == _doctorId);
            return allDoctors;
        }

        //creating an instance of findDoctor table (Model) as a parameter
        public bool commitInsert(findDoctor doctor)
        {
            //to ensure all data will be disposed of when finished
            using (objDoctor)
            {

                //using our model to set table columns to new values being passed and providing it to the insert command
                objDoctor.findDoctors.InsertOnSubmit(doctor);
                //commit insert with db
                objDoctor.SubmitChanges();
                return true;
            }
        }

        public bool commitUpdate(int _Id, string _fname, string _lname, string _speciality, string _branch_id, string _gender)
        {
            using (objDoctor)
            {
                //Update table according to the parameters
                var objUpdatedoctor = objDoctor.findDoctors.Single(x => x.Id == _Id);
                //setting table cols to new values
                objUpdatedoctor.fname = _fname;
                objUpdatedoctor.lname = _lname;
                objUpdatedoctor.speciality = _speciality;
                objUpdatedoctor.branch_id = _branch_id;
                objUpdatedoctor.gender = _gender;
               
                //commit update against db
                objDoctor.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int _doctorId)
        {
            using (objDoctor)
            {
                var objDeletedoctor = objDoctor.findDoctors.Single(x => x.Id == _doctorId);
                //deleting from table
                objDoctor.findDoctors.DeleteOnSubmit(objDeletedoctor);
                //committing delete with db
                objDoctor.SubmitChanges();
                return true;
            }
        }
    }
}