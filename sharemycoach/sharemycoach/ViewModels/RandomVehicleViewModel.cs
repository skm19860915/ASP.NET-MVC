using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sharemycoach.ViewModels
{
    public class RandomVehicleViewModel
    {
        public Guid Oid { get; set; }
        public string QuickFindKeyWord { get; set; }
        public string NameOnWeb { get; set; }
        public Guid? Location { get; set; }
        public string DailyRate { get; set; }
        public int? ClassType { get; set; }
        public long? VehicleSequenceId { get; set; }
        public long? LocationSequenceId { get; set; }
        public string WebRegionalName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string WebUniqueId { get; set; }
        public Guid? InsurancePolicy { get; set; }
        public string WebBannerName { get; set; }
    }
}