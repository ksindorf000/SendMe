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

        public ActionResult Payment(string stripeToken, int? amount, string Name, string Email, string Phone)
        {
            ViewBag.Message = null;
            if (!string.IsNullOrEmpty(stripeToken))
            {
                var donorInfo = new Donor
                {
                    Name = Name,
                    Email = Email,
                    Phone = Phone
                };
                db.Donors.Add(donorInfo);
                db.SaveChanges();

                var donation = new Donation
                {
                    Amount = amount,
                    HaveThanked = false,
                    TripId = 0,
                    Donor = db.Donors.Where(d => d.Email == Email).FirstOrDefault()
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
                    }
                    else
                    {
                        ViewBag.Message = result.FailureMessage;
                    }


                }
            }
            return View();

        }
    }
}
