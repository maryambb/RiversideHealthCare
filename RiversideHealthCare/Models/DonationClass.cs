using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using System.Data.Linq;

namespace RiversideHealthCare.Models
{
    public class DonationClass
    {
        // creating an instance of linq object
        riversideLinqDataContext objDon = new riversideLinqDataContext();

        //returning all donations
        public IQueryable<Donation> getDonations()
        {
            //variable with its value being the instance of linq object 
            var allDonations = objDon.Donations.Select(x => x);
            //return IQueryable<Donation> for data bound control to bind to return allDonations
            return allDonations;
        }

        //returning one donation by id
        public Donation getDonationByID(int _id)
        {
            var allDonation = objDon.Donations.SingleOrDefault(x => x.id == _id);
            return allDonation;
        }

        //returning all branches
        public IQueryable<Branch> getBranches()
        {
            var allBranches = objDon.Branches.Select(x => x);
            return allBranches;

        }

        //returning one branch by id
        public Branch getBranchByID(int _int)
        {
            var allBranch = objDon.Branches.SingleOrDefault(x => x.Id == _int);
            return allBranch;
        }
           
        //creating an instance of Donation table (Model) as a parameter
        public bool commitInsert(Donation don)
        {
            //to ensure all data will be disposed of when finished
            using (objDon)
            {
                //using our model to set table columns to new values being passed and providing it to the insert command
                objDon.Donations.InsertOnSubmit(don);
                objDon.SubmitChanges();
                return true;
            }
        }

        //updating donation
        public bool CommitUpdate(int _id, string _first_name, string _last_name, string _country, string _province, string _city, string _postal_code, string _email, string _phone, int _branch_id, DateTime _date, decimal _amount)
        {
            //Update table according to the parameters
            using (objDon)
            {
                var objUpDon = objDon.Donations.Single(x => x.id == _id);
                objUpDon.first_name = _first_name;
                objUpDon.last_name = _last_name;
                objUpDon.country = _country;
                objUpDon.province = _province;
                objUpDon.city = _city;
                objUpDon.postal_code = _postal_code;
                objUpDon.email = _email;
                objUpDon.phone = _phone;
                objUpDon.branch_id = _branch_id;
                objUpDon.amount = _amount;

                objDon.SubmitChanges();
                return true;
            }
        }

        //deleting donation
        public bool commitDelete(int _donationId)
        {
            using (objDon)
            {
                var objDelDonation = objDon.Donations.Single(x => x.id == _donationId);
                objDon.Donations.DeleteOnSubmit(objDelDonation);
                objDon.SubmitChanges();
                return true;
            }
        }
        
    }
}