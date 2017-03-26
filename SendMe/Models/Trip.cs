using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SendMe.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Destination { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationState { get; set; }
        public string DestinationCity { get; set; }
        public string DestLongLat { get; set; }
        public string Dates { get; set; }
        public DateTime Deadline { get; set; }
        public double TargetAmnt { get; set; }
        public double PercentOfAmnt { get; set; }
        public bool IsActive { get; set; }
        public int? StuId { get; set; }

        [ForeignKey("StuId")]
        public virtual StuProfile Student { get; set; }

    }
}