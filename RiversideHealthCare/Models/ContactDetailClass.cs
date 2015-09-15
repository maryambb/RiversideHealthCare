using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class ContactDetailClass
    {
        //creating instance of linq object
        riversideLinqDataContext objContact = new riversideLinqDataContext();

        public IQueryable<contact_detail> getContacts()
        {

            //variable with its value being the instance of linq object
            var allContacts = objContact.contact_details.Select(x => x);
            //return IQueryable<findDoctor> for data bound control to bind to return allDoctors
            return allContacts;
        }

        public contact_detail getContactsById(int _contactId)
        {
            var allContacts = objContact.contact_details.SingleOrDefault(x => x.Id == _contactId);
            return allContacts;
        }

        public IQueryable<contact_detail> getallContactsById(int _departmentId)
        {
            var allContacts = (from x in objContact.contact_details where x.department_id == _departmentId select x);
            return allContacts;
        }

        //creating an instance of findDoctor table (Model) as a parameter
        public bool commitInsert(contact_detail contact)
        {
            using (objContact)
            {
                objContact.contact_details.InsertOnSubmit(contact);
                //commit insert with db
                objContact.SubmitChanges();
                return true;
            }
        }

        public bool commitUpdate(int _Id, string _name,  string _phone, int _department_id)
        {
            using (objContact)
            {
                //using our model to set table columns to new values being passed and providing it to the insert command
                var objUpdatecontact = objContact.contact_details.Single(x => x.Id == _Id);
                //setting table cols to new values

                objUpdatecontact.name = _name;
                objUpdatecontact.phone = _phone;
                objUpdatecontact.department_id = _department_id;
                
                //commit update against db
                objContact.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int _contactId)
        {
            using (objContact)
            {
                var objDeletecontact = objContact.contact_details.Single(x => x.Id == _contactId);
                //deleting from table
                objContact.contact_details.DeleteOnSubmit(objDeletecontact);
                //committing delete with db
                objContact.SubmitChanges();
                return true;
            }
        }


    }
}