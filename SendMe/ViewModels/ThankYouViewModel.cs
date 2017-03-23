using SendMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendMe.ViewModels
{
    public class ThankYouViewModel
    {
        public StudentViewModel Student { get; set; }
        public Donation Donation { get; set; }
        public string Message { get; set; }

        public ThankYouViewModel() { }

        public ThankYouViewModel(StudentViewModel student, int? donationId, string msg)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Student = student;
            Donation = db.Donations.Find(donationId);
            Message = msg;
        }


    }
}