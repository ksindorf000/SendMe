using Microsoft.AspNet.Identity;
using SendMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SendMe.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /*********************************
         * INDEX: Username
         ********************************/
        [Route("send/{username}")]
        public ActionResult Index(string username)
        {
            ApplicationUser user = new ApplicationUser();

            StuProfile student = db.StuProfiles
                .Where(sp => sp.User.UserName == username)
                .FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");

            }

            StudentViewModel studentVM = new StudentViewModel(student);
            return View(studentVM);

        }
    }

}

