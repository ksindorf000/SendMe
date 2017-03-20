using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendMe.Models
{
    public class StudentViewModel
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public int Id { get; set; }
        public StuProfile Student { get; set; }
        public List<Trip> Trips { get; set; }
        public TripViewModel ActiveTrip { get; set; }
        public School School { get; set; }
        public ApplicationUser User { get; set; }
        public Upload Upload { get; set; }

        public StudentViewModel()
        {

        }

        public StudentViewModel(StuProfile student)
        {
            Student = student;
            School = db.Schools.Find(student.SchoolId);
            User = student.User;

            Trips = db.Trips
                .Where(t => t.Student.UserId == student.UserId)
                .ToList();

            if (Trips.Count > 0)
            {
                Trip activeTrip = Trips
                        .Where(t => t.IsActive == true
                          && t.StuId == student.Id).FirstOrDefault();
                if (activeTrip != null)
                {
                    ActiveTrip = new TripViewModel(activeTrip);
                }
            }

            Upload = db.Uploads
                .Where(p => p.TypeRef == "ProfPic"
                && p.RefId == student.UserId)
                .FirstOrDefault();
        }
    }
}