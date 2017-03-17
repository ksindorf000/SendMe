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

            StuProfile student = db.StuProfiles
                .Where(sp => sp.User.UserName == username)
                .FirstOrDefault();

            if (student == null)
            {
                return RedirectToAction("Index", "Home");

            }

            StudentViewModel studentVM = new StudentViewModel(student);

            return View(studentVM);
        }


    }
}



