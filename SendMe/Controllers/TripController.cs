using SendMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SendMe.Controllers
{
    public class TripController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            
            var trip = db.Trips.Find(3);
            var model = new TripViewModel(trip);
            return View(model);
        }
    }
}
