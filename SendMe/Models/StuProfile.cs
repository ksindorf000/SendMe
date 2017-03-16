using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SendMe.Models
{
    public class StuProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string Year { get; set; }
        public string Speciality { get; set; }
        public int SchoolId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public StuProfile() { }

        public StuProfile(ApplicationUser user, int schoolId)
        {
            SchoolId = schoolId;
            UserId = user.Id;
        }
    }
}