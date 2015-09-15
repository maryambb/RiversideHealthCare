using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class locationPublic
    {
        riversideLinqDataContext location = new riversideLinqDataContext();

        public IEnumerable<location> getLocations()
        {
            //select statement for all locations from db
            var allLocations = from x in location.locations select x;
            return allLocations;
        }
        public location getLocationById(int _id)
        {
            //select statement for specific locations
            var locationById = location.locations.SingleOrDefault(x => x.id == _id);
            return locationById;
        }
    }
}