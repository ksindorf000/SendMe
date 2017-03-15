using Microsoft.AspNet.Identity;
using SendMe.Models;
using SendMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
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
         * INDEX: School
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

            var school = new SchoolViewModel(id);

            return View(school);
        }
    }
}
