using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using SendMe.Models;
using SendMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace SendMe.Controllers
{
    public class SchoolController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /*********************************
         * INDEX: School
         ********************************/
        [Route("school/{id}")]
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            School school = db.Schools.Find(id);

            if (school == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userId = User.Identity.GetUserId();
            ViewBag.UserSchId = db.StuProfiles
                .Where(sp => sp.UserId == userId)
                .Select(sp => sp.SchoolId)
                .SingleOrDefault();

            SchoolViewModel schoolVM = new SchoolViewModel(school);

            return View(schoolVM);
        }

        /*********************************
         * MANAGE: School
         ********************************/
        [Authorize(Roles = "Admin")]
        public ActionResult Manage(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "School", new { id = id });
            }

            ViewBag.RefId = id;
            ViewBag.Type = "School";

            var school = db.Schools.Find(id);

            return View(school);
        }

        /*********************************
        * SAVE MANAGE: School
        ********************************/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "Id,Name,Website")] School model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                return View();
            }

            School updateSch = db.Schools.Find(model.Id);
            
            if (model.Name != null)
            {
                updateSch.Name = model.Name;
            }

            if (model.Website != null)
            {
                updateSch.Website = model.Website;
            }

            db.Entry(updateSch).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "School", new { id = model.Id });
        }

        /*********************************
        * Get new LatLong of School
        ********************************/
        public string GetAddressLatLong(string address)
        {
            string baseUrl = "http://maps.googleapis.com/maps/api/geocode/json?address=" +
            address + "&sensor=false";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            JsonResult json = new JsonResult
            {
                Data = client.GetAsync(baseUrl).Result
            };

            string jsonStr = new JavaScriptSerializer().Serialize(json.Data);

            var obj = JObject.Parse(jsonStr);

            string rLat = (string)obj.SelectToken("geometry.location.lat");
            string rLong = (string)obj.SelectToken("geometry.location.lng");

            string LatLong = rLat + rLong;
            return LatLong;

        }
    }
}