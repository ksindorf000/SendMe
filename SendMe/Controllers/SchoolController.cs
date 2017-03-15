using SendMe.Models;
using SendMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

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

            SchoolViewModel schoolVM = new SchoolViewModel(school);

            return View(schoolVM);
        }
    }
}
