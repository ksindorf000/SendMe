using SendMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SendMe.Collections
{
    public class SchoolCollection : List<SelectListItem>
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public SchoolCollection()
        {
            var sList = db.Schools.ToList();

            foreach (var item in sList)
            {
                this.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }            
        }
    }
}