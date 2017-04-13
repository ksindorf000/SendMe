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

        ApplicationDbContext db = new ApplicationDbContext();

        public SchoolViewModel() { }

        //----------------------------------------
        //      School with Students
        //----------------------------------------
        public SchoolViewModel(School school)
        {

            List<StuProfile> spList = db.StuProfiles
                .Where(
                    sp => sp.SchoolId == school.Id
                    && sp.FirstName != "Admin"
                    && sp.FirstName != null
                    )
                .OrderBy(sp => sp.LastName)
                .ToList();

            Students = new List<StudentViewModel>();

            foreach(var student in spList)
            {
                var userRoles = db.Roles.Where(r => r.Users.Select(u => u.UserId).Contains(student.UserId));
                if (userRoles.Count() < 1)
                {
                    var sVM = new StudentViewModel(student);
                    Students.Add(sVM);
                }
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

        //----------------------------------------
        //      School without Students
        //----------------------------------------
        public SchoolViewModel(int? id)
        {            
            School = db.Schools.Find((int)id);

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