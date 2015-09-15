using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class OrderClass
    {
        //creating instance of linq object
        riversideLinqDataContext objOrd = new riversideLinqDataContext();


        public bool commitInsert(Order ord)
        {
            using (objOrd)
            {
                objOrd.Orders.InsertOnSubmit(ord);
                //commit insert with db
                objOrd.SubmitChanges();
                return true;
            }
        }


    }
}