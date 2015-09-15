using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class ContactClass
    {
        //creating instance of linq object
        riversideLinqDataContext objContact = new riversideLinqDataContext();

        public IQueryable<contact_list> getContacts()
        {
            //variable with its value being the instance of linq object
            var allContacts = objContact.contact_lists.Select(x => x).OrderBy(x => x.department);
            //return IQueryable<contact_list> for data bound control to bind to return allContacts
            return allContacts;
        }

        public contact_list getContactsById(int _contactId)
        {
            var allContacts = objContact.contact_lists.SingleOrDefault(x => x.Id == _contactId);
            return allContacts;
        }

        //creating an instance of findDoctor table (Model) as a parameter
        public bool commitInsert(contact_list contact)
        {
            using (objContact)
            {
                //using our model to set table columns to new values being passed and providing it to the insert command
                objContact.contact_lists.InsertOnSubmit(contact);
                //commit insert with db
                objContact.SubmitChanges();
                return true;
            }
        }

        public bool commitUpdate(int _Id, string _department, string _detail, string _location, string _phone, string _fax, string _head)
        {
            using (objContact)
            {
                var objUpdatecontact = objContact.contact_lists.Single(x => x.Id == _Id);
                //setting table cols to new values
                objUpdatecontact.department = _department;
                objUpdatecontact.detail = _detail;
                objUpdatecontact.location = _location;
                objUpdatecontact.phone = _phone;
                objUpdatecontact.fax = _fax;
                objUpdatecontact.head = _head;
              
                
                //commit update against db
                objContact.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int _contactId)
        {
            using (objContact)
            {
                var objDeletecontact = objContact.contact_lists.Single(x => x.Id == _contactId);
                //deleting from table
                objContact.contact_lists.DeleteOnSubmit(objDeletecontact);
                //committing delete with db
                objContact.SubmitChanges();
                return true;
            }
        }

    }
}