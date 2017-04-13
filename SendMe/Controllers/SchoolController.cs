using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using SendMe.Helpers;
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
        [Route("School/{id}")]
        public ActionResult Index(int? id, string searchString)
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

            if (searchString != null)
            {
                schoolVM.Students.Clear();

                //Apply search string to users
                List<StuProfile> stuProf = db.StuProfiles
                    .Where(sp => 
                        sp.FirstName.Contains(searchString)
                        || sp.LastName.Contains(searchString)
                        || sp.User.UserName.Contains(searchString)
                        && sp.FirstName != "Admin"
                        && sp.FirstName != null
                    )
                    .ToList();

                foreach (StuProfile stu in stuProf)
                {
                    StudentViewModel student = new StudentViewModel(stu);
                    schoolVM.Students.Add(student);
                }

                //Apply search string to Trips
                List<Trip> trips = db.Trips
                    .Where(t => t.Title.Contains(searchString)
                            || t.Destination.Contains(searchString)
                            || t.DestinationCity.Contains(searchString)
                            || t.DestinationState.Contains(searchString)
                            || t.DestinationCountry.Contains(searchString))
                    .ToList();                

                foreach (Trip trip in trips)
                {
                    if (!schoolVM.Students.Any(s => s.User.Id == trip.Student.UserId))
                    {
                        StudentViewModel student = new StudentViewModel(trip.Student);
                        schoolVM.Students.Add(student);
                    }
                }
            }

            if (schoolVM.Students.Count < 1)
            {
                ViewBag.NullSearch = "No Results Found";
            }

            return View(schoolVM);
        }

        /*********************************
         * MANAGE: School
         ********************************/
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Manage(int? id)
        {
            string userId = User.Identity.GetUserId();
            int uSchId = db.StuProfiles
                .Where(sp => sp.UserId == userId)
                .Select(sp => sp.SchoolId)
                .SingleOrDefault();

            if (id == null || uSchId != id)
            {
                return RedirectToAction("Index", "School", new { id = id });
            }

            ViewBag.RefId = id.ToString();
            ViewBag.Type = "School";
            ViewBag.ReturnUrl = "../School/Manage/" + ViewBag.RefId;
            ViewBag.CoverPath = GetExistingImage("schCover", ViewBag.RefId);
            ViewBag.LogoPath = GetExistingImage("schLogo", ViewBag.RefId);

            var school = db.Schools.Find(id);

            return View(school);
        }

        //----------------------------
        //      Get Existing Img 
        //----------------------------
        public string GetExistingImage(string type, string refId)
        {
            Upload img = db.Uploads
                .Where(u => u.TypeRef == type && u.RefId == refId)
                .FirstOrDefault();

            string filePath = img.FilePath;

            return filePath;
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