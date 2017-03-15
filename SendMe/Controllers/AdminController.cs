using SendMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace SendMe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /*********************************
         * INDEX: No Username
         ********************************/
        public ActionResult Index()
        {
            var userSchool = db.StuProfiles
                .Where(sp => sp.User == User)
                .Select(sp => sp.SchoolId)
                .FirstOrDefault();

            ViewBag.Students = db.StuProfiles
                .Where(s => s.SchoolId == userSchool)
                .ToList();



            return View();   
        }

    }
}
