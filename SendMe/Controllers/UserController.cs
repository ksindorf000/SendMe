using Microsoft.AspNet.Identity;
using SendMe.Helpers;
using SendMe.Models;
using SendMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace SendMe.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        //--------------------------------
        // INDEX 
        //--------------------------------
        [Route("send/{username}")]
        public ActionResult Index(string username, int? donationId, string paymentMsg, string email)
        {
            StuProfile student = db.StuProfiles
                .Where(sp => sp.User.UserName == username)
                .FirstOrDefault();
            StudentViewModel studentVM = new StudentViewModel(student);

            if (student == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.CurrentTotal = 0;

            if (studentVM.ActiveTrip != null)
            {
                ViewBag.TargetAmount = studentVM.ActiveTrip.Trip.TargetAmnt;
                if (studentVM.ActiveTrip.Donations.Count() != 0)
                {
                    foreach (var donation in studentVM.ActiveTrip.Donations)
                    {
                        ViewBag.CurrentTotal += (double)donation.Amount;
                    }
                }

                if (ViewBag.TargetAmount <= ViewBag.CurrentTotal)
                {
                    ViewBag.CurrentTotal = ViewBag.TargetAmount;
                }
            }
            else
            {
                ViewBag.Action = "Create";
            }

            if (donationId != null)
            {
                var donationModify =
                from donation in db.Donations
                .Where(d => d.Id == donationId)
                select donation;

                foreach (Donation donation in donationModify)
                {
                    donation.HaveThanked = true;
                }
                db.SaveChanges();
            }                       

            ViewBag.UserName = username;

            if(paymentMsg == "Payment Successful")
            {
                ViewBag.PaymentMsg = $"{paymentMsg}! An electronic receipt has been sent to {email}.";
            }
            else if(paymentMsg == null)
            {
                ViewBag.PaymentMsg = null;
            }
            else
            {
                ViewBag.PaymentMsg = $"There was a problem: {paymentMsg}";
            }
            ViewBag.Email = email;
                        
                return View(studentVM);
        }


        //-----------------------------------
        //      Send Thank You Email
        //-----------------------------------         
        [HttpPost]
        public ActionResult SendThankYou(int? donId, int? stuId, string thxMsg)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Donation donation = db.Donations.Find(donId);
            Trip trip = db.Trips.Find(donation.TripId);
            StuProfile stuProf = db.StuProfiles.Find(stuId);
            StudentViewModel student = new StudentViewModel(stuProf);
            string picPath = student.Upload.FilePath;

            string subj = "Thank you for helping send me to " + trip.Destination + "!";

            string body = "<table><tr><td style=\"padding: 20px\"><img src=\"" + picPath
                + "\" style = \"width: 150px; height: 150px; border-radius: 50%\" ></td >"
                + "<td style=\"padding: 20px: text-align: left\">" + thxMsg + "</td></tr></table>";

            string fromEmail = ConfigurationManager.AppSettings["SendEmailsFrom"];

            MailHelper.Execute(body, donation.Donor.Name, donation.Donor.Email, student.Student.FirstName, student.Student.User.Email, subj);

            donation.HaveThanked = true;
            db.Entry(donation).State = EntityState.Modified;
            db.SaveChanges();

            string returnUrl = "../send/" + student.User.UserName;

            return RedirectToAction(returnUrl);
        }

    }
}



