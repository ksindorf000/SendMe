﻿using SendMe.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SendMe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.DisplayFootLogo = false;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        //----------------------------
        //    Send Contact Emails
        //---------------------------- 
        [HttpPost]
        public ActionResult Contact(string name, string email, string contactMsg)
        {
            string messageBody = "<p>" + this.Request.Form["contactMsg"] + "</p>";
            string fromEmail = this.Request.Form["email"];
            string fromName = this.Request.Form["name"];

            string emailSubject = "SendMe! Contact form submission";

            string toEmail = WebConfigurationManager.AppSettings["SendEmailsFrom"];

            MailHelper.Execute(messageBody, "SendMeAdmin", toEmail, fromName, fromEmail, emailSubject);

            return RedirectToAction("Contact");
        }
    }
}