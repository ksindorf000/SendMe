using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendMe.Models
{
    public class TripViewModel
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public int Id { get; set; }
        public Trip Trip { get; set; }
        public virtual StuProfile Student { get; set; }
        public List<Donation> Donations { get; set; }

        public TripViewModel ()
        {
        }

        public TripViewModel(Trip trip)
        {
            Trip = trip;
            Student = trip.Student;
            Donations = db.Donations
                .Where(d => d.TripId == trip.Id)
                .OrderByDescending(d => d.Created)
                .ToList();
        }

    }
}