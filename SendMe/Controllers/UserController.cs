﻿using Microsoft.AspNet.Identity;
using SendMe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SendMe.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        
        //--------------------------------
        // INDEX: Username
        //--------------------------------
        [Route("send/{username}")]
        public ActionResult Index(string username, int? donationId)
        {   
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

            StuProfile student = db.StuProfiles
                .Where(sp => sp.User.UserName == username)
                .FirstOrDefault();

            if (student == null)
            {
                return RedirectToAction("Index", "Home");
            }

            StudentViewModel studentVM = new StudentViewModel(student);
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

            return View(studentVM);
        }

        //--------------------------------
        // Render Partial
        //--------------------------------
        public ActionResult RenderTripModal()
        {
            string userId = User.Identity.GetUserId();
            int stuId = db.StuProfiles
                .Where(sp => sp.UserId == userId)
                .Select(sp => sp.Id)
                .FirstOrDefault();

            Trip newTrip = new Trip()
            {
                StuId = stuId,
                IsActive = true,
                PercentOfAmnt = 0
            };

            return PartialView("_CreateTrip", newTrip);
        }       

    }
}



