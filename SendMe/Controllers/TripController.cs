using Microsoft.AspNet.Identity;
using SendMe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                Destination = formData.Destination,
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
            string userId = User.Identity.GetUserId();
            int stuId = db.StuProfiles
                        .Where(sp => sp.UserId == userId)
                        .Select(sp => sp.Id)
                        .FirstOrDefault();

            Trip updateTrip = db.Trips
                .Where(t => t.StuId == stuId)
                .FirstOrDefault();

            updateTrip.Title = formData.Title;
            updateTrip.Desc = formData.Desc;
            updateTrip.Dates = formData.Dates;
            updateTrip.Deadline = formData.Deadline;
            updateTrip.Destination = formData.Destination;
            updateTrip.TargetAmnt = formData.TargetAmnt;
            updateTrip.StuId = stuId;

            db.Entry(updateTrip).State = EntityState.Modified;
            db.SaveChanges();

            string returnUrl = "../send/" + userName;

            return RedirectToAction(returnUrl);
        }

        //------------------------------------
        //      Render Trip Form Partial
        //------------------------------------
        public PartialViewResult RenderPartial(Trip trip)
        {
            return PartialView("_CreateTrip", trip);
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

            //Send Email to Admin


            return RedirectToAction(returnUrl);
        }
    }
}
