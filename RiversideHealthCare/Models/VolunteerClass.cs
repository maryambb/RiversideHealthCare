using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class VolunteerClass
    {
        //creating instance of linq object
        riversideLinqDataContext objVol = new riversideLinqDataContext();

        public IQueryable<volunteer> getVolunteers()
        {
            //variable with its value being the instance of linq object
            var allVolunteers = objVol.volunteers.Select(x => x);
            //return IQueryable<volunteerr> for data bound control to bind to return allVolunteers
            return allVolunteers;
        }

        public IQueryable<volunteer_application_form> getApplications(int id)
        {
            //variable with its value being the instance of linq object
            var allVolunteers = (from x in objVol.volunteer_application_forms where x.oppID == id select x);
            //return IQueryable<volunteer_application_form> for data bound control to bind to return allApplications
            return allVolunteers;
        }

        public volunteer getVolunteerById(int _VolunteerId)
        {
            var allVolunteers = objVol.volunteers.SingleOrDefault(x => x.Id == _VolunteerId);
            return allVolunteers;
        }

        //creating an instance of volunteer table (Model) as a parameter
        public bool commitInsert(volunteer vol)
        {
            //to ensure all data will be disposed of when finished
            using (objVol)
            {
                //using our model to set table columns to new values being passed and providing it to the insert command
                objVol.volunteers.InsertOnSubmit(vol);
                //commit insert with db
                objVol.SubmitChanges();
                return true;
            }
        }

        public bool commitVoInsert(volunteer_application_form vol)
        {
            //to ensure all data will be disposed of when finished
            using (objVol)
            {
                //using our model to set table columns to new values being passed and providing it to the insert command
                objVol.volunteer_application_forms.InsertOnSubmit(vol);
                //commit insert with db
                objVol.SubmitChanges();
                return true;
            }
        }

        public bool commitUpdate(int _Id, string _position, string _location, string _requirement)
        {
            using (objVol)
            {
                var objUpdateVol = objVol.volunteers.Single(x => x.Id == _Id);
                //setting table cols to new values
                objUpdateVol.position = _position;
                objUpdateVol.location = _location;
                objUpdateVol.requirement = _requirement;

                //commit update against db
                objVol.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int _VolunteerId)
        {
            using (objVol)
            {
                var objDeleteVol = objVol.volunteers.Single(x => x.Id == _VolunteerId);
                //deleting from table
                objVol.volunteers.DeleteOnSubmit(objDeleteVol);
                //committing delete with db
                objVol.SubmitChanges();
                return true;
            }
        }


    }
}