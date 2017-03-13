using Microsoft.AspNet.Identity;
using SendMe.Models;
using System;
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
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                user = db.Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefault();

                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }

        /*********************************
         * INDEX: Username
         ********************************/
        [Route("send/{username}")]
        public ActionResult Index(string username)
        {
            user = db.Users
                .Where(u => u.UserName == username)
                .FirstOrDefault();

            return View(user);
        }
    }

}

