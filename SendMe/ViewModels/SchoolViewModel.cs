using SendMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendMe.ViewModels
{
    public class SchoolViewModel
    {
        public School School { get; set; }
        public List<StudentViewModel> Students { get; set; }
        public Upload CoverImg { get; set; }
        public Upload LogoImg { get; set; }

        public SchoolViewModel() { }

        public SchoolViewModel(School school)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            List<StuProfile> spList = db.StuProfiles
                .Where(sp => sp.SchoolId == school.Id)
                .ToList();
            Students = new List<StudentViewModel>();
            foreach(var student in spList)
            {
                var sVM = new StudentViewModel(student);
                Students.Add(sVM);
            }

            School = school;

            if (School != null)
            {
                CoverImg = db.Uploads
                    .Where(u => u.TypeRef == "schCover" && u.RefId == School.Id.ToString())
                    .FirstOrDefault();

                LogoImg = db.Uploads
                    .Where(u => u.TypeRef == "schLogo" && u.RefId == School.Id.ToString())
                    .FirstOrDefault();
            }
        }

    }
}