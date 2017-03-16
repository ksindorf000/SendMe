using Microsoft.AspNet.Identity;
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

        
        /*********************************
         * INDEX: Username
         ********************************/
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

            var currentUser = User.Identity.GetUserId();

            StuProfile student = db.StuProfiles
                .Where(sp => sp.User.UserName == username)
                .FirstOrDefault();
           
            Trip currentTrip = db.Trips
                .Where(t => t.Student.UserId == currentUser)
                .FirstOrDefault();

            if (student == null)
            {
                return RedirectToAction("Index", "Home");

            }

            TripViewModel tripVM = new TripViewModel(currentTrip);
            StudentViewModel studentVM = new StudentViewModel(student);

            return View(new Tuple<StudentViewModel, TripViewModel>(studentVM, tripVM));
        }


    }
}



