using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiversideHealthCare.Models;
//REQUIRED TO SEND MAIL
using System.Net.Mail;

namespace RiversideHealthCare.Controllers
{
    public class FeedbackAdminController : Controller
    {
        //CREATING FEEDBACK OBJECT
        FeedbackClass objFeed = new FeedbackClass();

         [Authorize(Roles = "admin")]
        public ActionResult FeedbackAdminIndex()
        {
            var Feed = objFeed.getFeedback();
            return View(Feed);
        }

        //DELETE POST REQUEST
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult FeedbackAdminDelete(int id, feedback Feed)
        {
            try
            {
                
                objFeed.commitDelete(id);
                return RedirectToAction("FeedbackAdminIndex");
            }
            catch
            {
                return RedirectToAction("FeedbackAdminIndex");

            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult SendMail()
        {
            return PartialView();
        }
        /*
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult SendMail(RiversideHealthCare.Models.EmailValidation _objModelMail)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(_objModelMail.To);
                mail.From = new MailAddress(_objModelMail.From);
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "ssl://mail.stem.arvixe.com";
                smtp.Port = 465;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("test@egrabishtest.com", "egrabishtest");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return PartialView("_MailForm", _objModelMail);

            }
            else
            {
                return PartialView();
            }
         }*/
    }
}
