using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class ErAdmin
    {
        riversideLinqDataContext obj = new riversideLinqDataContext();

        public IQueryable<ERWaitQueue> getPaitents()
        {
            var allPaitents = from x in obj.ERWaitQueues select x;
            return allPaitents;
        }
        public ERWaitQueue getPaitentByID(int _id)
        {
            var allPaitentsById = obj.ERWaitQueues.SingleOrDefault(x => x.id == _id);
            return allPaitentsById;
        }
        public ERCurrentTime getTimeById(int _id)
        {
            var timeById = obj.ERCurrentTimes.SingleOrDefault(x => x.id == _id);
            return timeById;
        }
        public string getCurrentTime()
        {
            int id = 1;
            var currentTime = obj.ERCurrentTimes.SingleOrDefault(x => x.id == id).waitTime;
            return currentTime.ToString();

        }
        public bool commitInsertQueue(ERWaitQueue paitent)
        {
            using (obj)
            {
                obj.ERWaitQueues.InsertOnSubmit(paitent);
                obj.SubmitChanges();
                //update the timer
                commitUpdateOnInsert(paitent.condition);
                return true;
            }

        }
        public bool commitUpdateQueue(int _id, string _fName, string _lName, int _condition)
        {
            using (obj)
            {
                var objUpd = obj.ERWaitQueues.Single(x => x.id == _id);
                objUpd.fName = _fName;
                objUpd.lName = _lName;
                objUpd.condition = _condition;
                obj.SubmitChanges();
                return true;
            }
        }
        public bool commitDeleteQueue(int _id)
        {
            using (obj)
            {
                var objDel = obj.ERWaitQueues.Single(x => x.id == _id);
                obj.ERWaitQueues.DeleteOnSubmit(objDel);
                obj.SubmitChanges();
                //update the timer
                commitUpdateTimeOnDelete(objDel.condition);
                return true;
            }
        }
        public bool commitUpdateTime(TimeSpan time, int _id = 1)
        {
            using (obj)
            {
                var objUpd = obj.ERCurrentTimes.Single(x => x.id == _id);
                objUpd.waitTime = time;
                obj.SubmitChanges();
                return true;
            }
        }
        public bool commitUpdateOnInsert(int severityLevel = 1)
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
        public bool commitUpdateTimeOnDelete(int severityLevel)
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
                    var newTime = oldTime.Subtract(paitentTimer);
                    objUpd.waitTime = newTime;
                }
                else if (severityLevel == 2)
                {
                    paitentTimer = TimeSpan.FromMinutes(20);
                    var newTime = oldTime.Subtract(paitentTimer);
                    objUpd.waitTime = newTime;
                }
                else
                {
                    paitentTimer = TimeSpan.FromMinutes(30);
                    var newTime = oldTime.Subtract(paitentTimer);
                    objUpd.waitTime = newTime;
                }

                obj.SubmitChanges();
                return true;

            }
        }
    }
}