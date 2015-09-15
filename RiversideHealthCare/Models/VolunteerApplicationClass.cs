using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class VolunteerApplicationClass
    {
        // creating an instance of linq object
        riversideLinqDataContext objApp = new riversideLinqDataContext();
        public IQueryable<volunteer_application_form> getApplications()
        {
            //variable with its value being the instance of linq object 
            var allApplications = objApp.volunteer_application_forms.Select(x => x);
            //return IQueryable<volunteer_application_form> for data bound control to bind to return allapplications
            return allApplications;
        }

        public volunteer_application_form getApplicationsByID(int _id)
        {
            var allApplication = objApp.volunteer_application_forms.SingleOrDefault(x => x.Id == _id);
            return allApplication;
        }

        public volunteer_application_form getVolListById(int id)
        {
            var selectList = objApp.volunteer_application_forms.SingleOrDefault(x => x.Id == id);
            return selectList;
        }

        //deleting applications
        public bool commitDelete(int id)
        {
            using (objApp)
            {
                var objDelApp = objApp.volunteer_application_forms.Single(x => x.Id == id);
                objApp.volunteer_application_forms.DeleteOnSubmit(objDelApp);
                objApp.SubmitChanges();
                return true;
            }
        }
    }
}