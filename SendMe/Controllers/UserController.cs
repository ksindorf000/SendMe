﻿using Microsoft.AspNet.Identity;
using SendMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SendMe.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /*********************************
         * INDEX: Username
         ********************************/
        [Route("send/{username}")]
        public ActionResult Index(string username, int? donationId)

        {
            ApplicationUser user = new ApplicationUser();

            StuProfile student = db.StuProfiles
                .Where(sp => sp.User.UserName == username)
                .FirstOrDefault();
            var currentUser = User.Identity.GetUserId();
            Trip currentTrip = db.Trips.Where(t => t.Student.UserId == currentUser).FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");

            }

            TripViewModel tripVM = new TripViewModel(currentTrip);
            StudentViewModel studentVM = new StudentViewModel(student);

            return View(new Tuple<StudentViewModel, TripViewModel>(studentVM, tripVM));

        }


    }
}



