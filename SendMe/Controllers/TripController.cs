using Microsoft.AspNet.Identity;
using SendMe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using SendMe.Helpers;
using System.Web.Mvc;

namespace SendMe.Controllers
{
    public class TripController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //----------------------------
        //      Trip Index
        //----------------------------
        public ActionResult Index()
        {

            var trip = db.Trips.ToList();
            var model = new List<TripViewModel>();
            foreach (var item in trip)
            {
                TripViewModel test = new TripViewModel(item);
                model.Add(test);

            }
            return View(model);
        }


        //------------------------------------
        //      Render Trip Form Partial
        //------------------------------------
        public PartialViewResult RenderPartial(Trip trip)
        {
            return PartialView("_CreateTrip", trip);
        }


        //----------------------------
        //      Create Trip
        //----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Trip formData)
        {
            string userName = User.Identity.Name;
            string userId = User.Identity.GetUserId();
            int stuId = db.StuProfiles
                        .Where(sp => sp.UserId == userId)
                        .Select(sp => sp.Id)
                        .FirstOrDefault();

            Trip newTrip = new Trip()
            {
                Title = formData.Title,
                Desc = formData.Desc,
                Dates = formData.Dates,
                Deadline = formData.Deadline,
                DestinationCountry = Request.Form["country"].ToString(),
                DestinationCity = Request.Form["city"].ToString(),
                DestinationState = Request.Form["state"].ToString(),
                Destination = Request.Form["country"].ToString() + ", " + Request.Form["city"].ToString(),
                TargetAmnt = formData.TargetAmnt,
                StuId = stuId,
                IsActive = true
            };

            db.Trips.Add(newTrip);

            db.SaveChanges();

            string returnUrl = "../send/" + userName;

            return RedirectToAction(returnUrl);
        }

        //----------------------------
        //      Update Trip
        //----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Update(Trip formData)
        {
            string userName = User.Identity.Name;
            string returnUrl = "../send/" + userName;

            string userId = User.Identity.GetUserId();
            int stuId = db.StuProfiles
                        .Where(sp => sp.UserId == userId)
                        .Select(sp => sp.Id)
                        .FirstOrDefault();

            Trip updateTrip = db.Trips.Find(formData.Id);
            if (updateTrip == null)
            {
                return RedirectToAction(returnUrl);
            }

            updateTrip.Title = formData.Title;
            updateTrip.Desc = formData.Desc;
            updateTrip.Dates = formData.Dates;
            updateTrip.Deadline = formData.Deadline;

            string country = Request.Form["country"].ToString();
            string city = Request.Form["city"].ToString();
            string state = Request.Form["state"].ToString();

            if (country != "")
            {
                updateTrip.DestinationCountry = country;
            }
            if (city != "")
            {
                updateTrip.DestinationCity = city;
            }
            if (state != "")
            {
                updateTrip.DestinationState = state;
            }
            
            updateTrip.TargetAmnt = formData.TargetAmnt;
            updateTrip.StuId = stuId;

            db.Entry(updateTrip).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction(returnUrl);
        }

        //----------------------------
        //      Cancel Trip
        //----------------------------        
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int? id)
        {
            string userName = User.Identity.Name;
            string returnUrl = "../send/" + userName;

            if (id == null)
            {
                return RedirectToAction(returnUrl);
            }

            string userId = User.Identity.GetUserId();

            Trip trip = db.Trips
                .Where(t => t.Id == id
                && t.Student.UserId == userId)
                .FirstOrDefault();

            if (trip == null || trip.Student.UserId != userId)
            {
                return RedirectToAction(returnUrl);
            }

            trip.IsActive = false;
            db.Entry(trip).State = EntityState.Modified;
            db.SaveChanges();

            SendAdminEmail(userId, "cancel", trip.Student.SchoolId);

            return RedirectToAction(returnUrl);
        }

        //----------------------------
        //      Make Trip Active
        //----------------------------        
        [HttpPost, ActionName("MakeActive")]
        [ValidateAntiForgeryToken]
        public ActionResult MakeActive(int? id)
        {
            string userName = User.Identity.Name;
            string returnUrl = "../send/" + userName;

            if (id == null)
            {
                return RedirectToAction(returnUrl);
            }

            string userId = User.Identity.GetUserId();

            Trip trip = db.Trips
                .Where(t => t.Id == id
                && t.Student.UserId == userId)
                .FirstOrDefault();

            if (trip == null || trip.Student.UserId != userId)
            {
                return RedirectToAction(returnUrl);
            }

            trip.IsActive = true;
            db.Entry(trip).State = EntityState.Modified;
            db.SaveChanges();

            SendAdminEmail(userId, "activate", trip.Student.SchoolId);

            return RedirectToAction(returnUrl);
        }

        //----------------------------
        //      Send Admin Emails
        //---------------------------- 
        private void SendAdminEmail(string userId, string type, int schId)
        {
            ApplicationUser admin = (from role in db.Roles
                                     where role.Name == "Admin"
                                     from userRoles in role.Users
                                     join user in db.Users
                                         on userRoles.UserId equals user.Id
                                     join sp in db.StuProfiles
                                         on user.Id equals sp.UserId
                                     where user.EmailConfirmed == true
                                     select user).FirstOrDefault();

            var adminProf = db.StuProfiles
                .SingleOrDefault(sp => sp.User.Email == admin.Email);

            string adminName = $"{ adminProf.FirstName} { adminProf.LastName}";

            string userEmail = db.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Email)
                .FirstOrDefault();

            string emailSubject = (type == "cancel") ? "Trip Cancellation" : "Trip Reactivated";

            var stuProf = db.StuProfiles
               .SingleOrDefault(sp => sp.UserId == userId);

            string stuName = $"{ stuProf.FirstName} { stuProf.LastName}";

            string messageBody = (type == "cancel") ?
                stuName + " has cancelled their trip! <a href=\"#\">Login to SendMe!</a> to see donor and student contact information."
                : stuName + " has reactivated a cancelled trip! <a href=\"#\">Login to SendMe!</a> to see donor and student contact information.";

            MailHelper.Execute(messageBody, adminName, admin.Email, stuName, userEmail, emailSubject);
        }
    }
}
