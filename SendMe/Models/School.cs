using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendMe.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string LatLong { get; set; }
        public string EmailDomain { get; set; }
    }
}