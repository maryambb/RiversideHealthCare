using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class DoctorClass
    {

        riversideLinqDataContext objLinq = new riversideLinqDataContext();

        //returning all doctors
        public IQueryable<doctor> getAllDoctors()
        {
            var allDoctors = objLinq.doctors.Select(x => x);
            return allDoctors;
        }

        //returning one doctor with specific id
        public doctor getDoctorByID(int _id)
        {
            var doctor = objLinq.doctors.SingleOrDefault(x => x.id == _id);
            return doctor;
        }

        //returning one doctor with specific user name
        public doctor getDoctorByUserName(string _username)
        {
            var allDoctors = objLinq.doctors.SingleOrDefault(x => x.user_name == _username);
            return allDoctors;
        }

        //Inserting doctor into database
        public bool commitInsert(doctor doc)
        {
            using (objLinq)
            {
                objLinq.doctors.InsertOnSubmit(doc);
                objLinq.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int _docId)
        {
            using (objLinq)
            {
                var objDelDoctor = objLinq.doctors.Single(x => x.id == _docId);
                objLinq.doctors.DeleteOnSubmit(objDelDoctor);
                objLinq.SubmitChanges();
                return true;
            }
        }
      
    }
}

