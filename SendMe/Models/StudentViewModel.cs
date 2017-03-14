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
        public Trip ActiveTrip { get; set; }
        public School School { get; set; }
        public ApplicationUser User { get; set; }
        public string ProfPicPath { get; set; }

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

            ActiveTrip = Trips.Where(t => t.IsActive == true).FirstOrDefault();

            /*ProfPicPath = db.Uploads
                .Where(p => p.RefType == "ProfPic"
                && p.RefId == student.UserId)
                .FirstOrDefault();*/
        }
    }
}