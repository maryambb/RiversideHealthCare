using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class ErPublic
    {
        riversideLinqDataContext obj = new riversideLinqDataContext();

        //gets the current wait time for the ER
        public IQueryable<ERCurrentTime> getCurrentTime(int id = 1)
        {
            var currentTimer = from x in obj.ERCurrentTimes select x;
            return currentTimer;

        }

        //Inserting person into queue
        public bool commitInsert(ERWaitQueue x)
        {
            using (obj)
            {
                obj.ERWaitQueues.InsertOnSubmit(x);
                obj.SubmitChanges();
                commitUpdate(x.condition);
                return true;
            }
        }
        //Update current timer
        public bool commitUpdate(int severityLevel)
        {
            var _id = 1;
            using (obj)
            {
                var objUpd = obj.ERCurrentTimes.Single(x => x.id == _id);
                var oldTime = (TimeSpan)obj.ERCurrentTimes.Select(x => x.waitTime).Single();
                TimeSpan paitentTimer;
                if (severityLevel == 1)
                {
                    paitentTimer = TimeSpan.FromMinutes(10);
                    var newTime = oldTime.Add(paitentTimer);
                    objUpd.waitTime = newTime;
                }
                else if (severityLevel == 2)
                {
                    paitentTimer = TimeSpan.FromMinutes(20);
                    var newTime = oldTime.Add(paitentTimer);
                    objUpd.waitTime = newTime;
                }
                else
                {
                    paitentTimer = TimeSpan.FromMinutes(30);
                    var newTime = oldTime.Add(paitentTimer);
                    objUpd.waitTime = newTime;
                }

                obj.SubmitChanges();
                return true;

            }
        }
    }
}