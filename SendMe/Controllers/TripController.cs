using Microsoft.AspNet.Identity;
using SendMe.Models;
using System;
using System.Collections.Generic;
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
        //      Post Trip
        //----------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trip formData)
        {
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
                StuId = stuId
            };

            return RedirectToAction("Index", "Manage");
        }
    }
}
