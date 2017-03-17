using SendMe.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;

namespace SendMe.Controllers
{
    public class DonationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Payment(string stripeToken, int? amount, string Name, string Email, string Phone, int? tripId)
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
                        ViewBag.Message = "Payment Successful!";

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
                        //var myMessage = new SendGrid.SendGridMessage();
                        //myMessage.AddTo("test@sendgrid.com");
                        //myMessage.From = new MailAddress("you@youremail.com", "First Last");
                        //myMessage.Subject = "Sending with SendGrid is Fun";
                        //myMessage.Text = "and easy to do anywhere, even with C#";

                        //var transportWeb = new SendGrid.Web("SENDGRID_APIKEY");
                        //transportWeb.DeliverAsync(myMessage);
                    }
                    else
                    {
                        ViewBag.Message = result.FailureMessage;
                    }


                }
            }
            return View(ViewBag.TripId);

        }
    }
}
