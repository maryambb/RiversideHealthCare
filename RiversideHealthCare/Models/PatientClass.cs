using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class PatientClass
    {
        riversideLinqDataContext objLinq = new riversideLinqDataContext();
        // returning all patients
        public IQueryable<patient> getAllPatients()
        {
            var allPatients = objLinq.patients.Select(x => x);
            return allPatients;
        }

        //insert patient into db
        public bool commitInsert(patient pat)
        {
            using (objLinq)
            {
                objLinq.patients.InsertOnSubmit(pat);
                objLinq.SubmitChanges();
                return true;
            }
        }

        //get patient information by username
        public patient getPatientByUserName(string _username)
        {
            var allPatients = objLinq.patients.SingleOrDefault(x => x.user_name == _username);
            return allPatients;
        }

        //returning one patient by id
        public patient getPatientByID(int _id)
        {
            var allPatients = objLinq.patients.SingleOrDefault(x => x.id == _id);
            return allPatients;
        }

        //deleter patient from database
        public bool commitDelete(int _patId)
        {
            using (objLinq)
            {
                var objDelPatient = objLinq.patients.Single(x => x.id == _patId);
                objLinq.patients.DeleteOnSubmit(objDelPatient);
                objLinq.SubmitChanges();
                return true;
            }
        }
    }
}