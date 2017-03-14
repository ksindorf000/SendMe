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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string Year { get; set; }
        public string Speciality { get; set; }
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
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
            Bio = student.Bio;
            Year = student.Year;
            Speciality = student.Speciality;
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