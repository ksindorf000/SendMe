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
        ApplicationUser user = new ApplicationUser();

        /*********************************
         * INDEX: No Username
         ********************************/
        public ActionResult Index(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<StudentViewModel> vmList = new List<StudentViewModel>();
            List<StuProfile> stuList = db.StuProfiles
                .Where(s => s.SchoolId == id)
                .ToList();

            foreach (var student in stuList)
            {
                StudentViewModel sVM = new StudentViewModel(student);
                vmList.Add(sVM);
            };

            return View(vmList);
        }

        /*********************************
         * INDEX: Username
         ********************************/
        [Route("send/{username}")]
        public ActionResult Index(string username)
        {
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

