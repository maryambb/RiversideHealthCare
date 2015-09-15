using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class locationAdmin
    {
        riversideLinqDataContext obj = new riversideLinqDataContext();

        public IQueryable<location> getLocations()
        {
            //select statement for all locations from db
            var allLocs = from x in obj.locations select x;
            return allLocs;
        }
        public location getLocationsByID(int _id)
        {
            //select statement for specific location
            var allLocsById = obj.locations.SingleOrDefault(x => x.id == _id);
            return allLocsById;
        }
        public bool commitInsert(location loc)
        {
            using (obj)
            {
                //inserting into database
                obj.locations.InsertOnSubmit(loc);
                obj.SubmitChanges();
                return true;
            }
        }
        public bool commitUpdate(int _id, string _name, string _address, string _postal,
            string _country, string _phone)
        {
            using (obj)
            {
                //updating location
                var objUpd = obj.locations.Single(x => x.id == _id);
                objUpd.name = _name;
                objUpd.address = _address;
                objUpd.postalCode = _postal;
                objUpd.country = _country;
                objUpd.phone = _phone;
                obj.SubmitChanges();
                return true;
            }
        }
        public bool commitDelete(int _id)
        {
            using (obj)
            {
                //deleting a location
                var objDel = obj.locations.Single(x => x.id == _id);
                obj.locations.DeleteOnSubmit(objDel);
                obj.SubmitChanges();
                return true;
            }
        }
    }
}