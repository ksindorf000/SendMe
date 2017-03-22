using SendMe.Helpers;
using SendMe.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SendMe.Controllers
{
    public class DonationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Payment(string stripeToken, int? amount, string Name, string Email, string Phone, int? tripId, string userName)
        {
            if (tripId == null)
            {
                return HttpNotFound();
            }

            ViewBag.TripId = tripId;
            ViewBag.Message = null;
            if (!string.IsNullOrEmpty(stripeToken))
            {
                var attachDonor = new Donor();

                //Check for an existing donor
                var existingDonor = db.Donors.Where(d => d.Email == Email || d.Phone == Phone).FirstOrDefault();

                //Add donor record if none existing
                if (existingDonor == null && (Name != null || Email != null))
                {
                    string name = (Name == "" ? "Anonymous" : Name);

                    Donor donorInfo = new Donor
                    { 
                        Name = name,
                        Email = Email,
                        Phone = Phone
                    };

                    db.Donors.Add(donorInfo);
                    db.SaveChanges();

                    attachDonor = donorInfo;
                }
                else
                {
                    attachDonor = existingDonor;
                }

                //Add donation record
                var donation = new Donation
                {
                    Amount = amount/100,
                    HaveThanked = false,
                    TripId = (int)tripId,
                    Donor = attachDonor
                };

                db.Donations.Add(donation);
                db.SaveChanges();

                if (ModelState.IsValid)
                {
                    var chargeRequest = new StripeChargeCreateOptions()
                    {
                        Amount = amount,
                        Currency = "USD",
                        SourceTokenOrExistingSourceId = stripeToken,
                        Description = "Donation"

                    };
                    var service = new StripeChargeService(WebConfigurationManager.AppSettings["PrivateKey"]);
                    var result = service.Create(chargeRequest);

                    if (result.Paid)
                    {
                        ViewBag.Message = "Payment Successful";

                        if (tripId != null)
                        {
                            //Update Percentage for trip
                            Trip trip = db.Trips.Find(tripId);
                            var donated = db.Donations
                                        .Where(d => d.TripId == trip.Id)
                                        .Sum(d => d.Amount);
                            double target = trip.TargetAmnt;
                            trip.PercentOfAmnt = (double)((donated / target) * 100);
                            db.Entry(trip).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        SendReceiptEmail(Email, amount, Name, tripId);

                    }
                    else
                    {
                        ViewBag.Message = result.FailureMessage;
                    }
                }
            }
            string paymentMessage = ViewBag.Message;
            return RedirectToAction(userName, new RouteValueDictionary(
            new { controller = "send", action = userName, paymentMsg = paymentMessage, email = Email}));

        }

        private static void SendReceiptEmail(string toEmail, int? amount, string name, int? tripId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Trip trip = db.Trips
                .SingleOrDefault(t => t.Id == tripId);

            DateTime date = DateTime.Today;           
            string dateFormat = "MMM d yyyy";
            string today = date.ToString(dateFormat);

            StuProfile student = trip.Student;
            StudentViewModel stuVM = new StudentViewModel(student);

            string picPath = stuVM.Upload.FilePath;

            string msg = "Thank you for helping send "
                + stuVM.Student.FirstName + " to " + trip.Destination + "! </br>"
                + "You donated $" + amount + " on " + today + ".";

            string body = "<table><tr><td style=\"padding: 20px\"><img src=\"" + picPath
                + "\" style = \"width: 150px; height: 150px; border-radius: 50%\" ></td >"
                + "<td style=\"padding: 20px: text-align: left\">" + msg + "</td></tr></table>";

            string fromEmail = ConfigurationManager.AppSettings["SendEmailsFrom"];

            string subj = "Donation Receipt from SendMe!";

            MailHelper.Execute(body, name, toEmail, "SendMe!", fromEmail, subj);
        }
    }
}
