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
        public Stripe.StripeCharge result;
        public ActionResult Payment(string stripeToken, int? amount, string Name, string Email, string Phone, int? tripId, string userName, string donationMsg)
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
                var existingDonor = db.Donors.Where(d => d.Email == Email).FirstOrDefault();

                //Add donor record if none existing
                if (existingDonor == null)
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

                if (ModelState.IsValid)
                {
                    Trip tripInfo = db.Trips.SingleOrDefault(t => t.Id == tripId);
                    //Create the charge request to send to stripe
                    var chargeRequest = new StripeChargeCreateOptions()
                    {
                        Amount = amount,
                        Currency = "USD",
                        SourceTokenOrExistingSourceId = stripeToken,
                        Description = $"Destination:{tripInfo.Destination} || Student Name:{ tripInfo.Student.FirstName},{ tripInfo.Student.LastName}",

                };
                    var service = new StripeChargeService(WebConfigurationManager.AppSettings["PrivateKey"]);
                    try
                    {
                        result = service.Create(chargeRequest);

                        if (result.Paid)
                        {
                            if (string.IsNullOrEmpty(donationMsg))
                            {
                                donationMsg = "No Message";
                            }
                                //Create a donation in Db
                                var donation = new Donation
                            {
                                Amount = amount / 100,
                                HaveThanked = false,
                                TripId = (int)tripId,
                                Donor = attachDonor,
                                Created = DateTime.Now,
                                DonationMsg = donationMsg,
                                
                            };
                            db.Donations.Add(donation);
                            db.SaveChanges();

                            ViewBag.Message = "Payment Successful";

                        if (tripId != null)
                        {
                            //Update Percentage for trip
                            Trip trip = db.Trips.Find(tripId);
                            var donated = db.Donations
                                        .Where(d => d.TripId == trip.Id)
                                        .Sum(d => d.Amount);
                            double target = trip.TargetAmnt;
                            double rawPercent = (double)((donated / target) * 100);
                            trip.PercentOfAmnt = Math.Round(rawPercent, 2);
                            db.Entry(trip).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                            //Send Receipt
                            //var transportWeb = new SendGrid.Web("SENDGRID_APIKEY");
                            //transportWeb.DeliverAsync(myMessage);
                            SendReceiptEmail(Email, amount, Name, tripId);

                        }
                        else
                        {
                            ViewBag.Message = result.FailureMessage;
                        }
                    }
                    catch (StripeException ex)
                    {
                        ViewBag.Message = ex.Message;

                    }
                }


            }
            string paymentMessage = ViewBag.Message;
            return RedirectToAction(userName, new RouteValueDictionary(
            new { controller = "send", action = userName, paymentMsg = paymentMessage, email = Email }));

        }

        //----------------------------
        //      Send Receipt Email
        //---------------------------- 
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
                + stuVM.Student.FirstName + " to " + trip.DestinationCountry + "! </br></br>"
                + "You donated $" + amount + " on " + today + ".";

            string body = "<table><tr><td style=\"padding: 20px\"><img src=\"" + picPath
                + "\" style = \"width: 150px; height: 150px; border-radius: 50%\" ></td >"
                + "<td style=\"padding: 20px; text-align: left\">" + msg + "</td></tr></table>";

            string fromEmail = ConfigurationManager.AppSettings["SendEmailsFrom"];

            string subj = "Donation Receipt from SendMe!";

            MailHelper.Execute(body, name, toEmail, "SendMe!", fromEmail, subj);
        }
    }
}
