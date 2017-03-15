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
            
            var trip = db.Trips.ToList();
            var model = new List<TripViewModel>();
            foreach (var item in trip)
            {
                TripViewModel test = new TripViewModel(item);
                model.Add(test);
           
            }
            return View(model);

        }
    }
}
