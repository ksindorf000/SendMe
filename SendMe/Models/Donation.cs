using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SendMe.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public double? Amount { get; set; }
        public bool HaveThanked { get; set; }
        public int TripId { get; set; }
        public int DonorId { get; set; }
        public string DonationMsg { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("DonorId")]
        public virtual Donor Donor { get; set; }
    }
}