using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sharemycoach.ViewModels
{
    public class OtherVehicleViewModel
    {
        public Guid Oid { get; set; }
        public string WebUniqueId { get; set; }
        public string NameOnWeb { get; set; }
        public string DailyRate { get; set; }
        public Guid? InsurancePolicy { get; set; }
    }
}