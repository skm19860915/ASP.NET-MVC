using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sharemycoach.ViewModels
{
    public class VehicleInfoViewModel
    {
        public string NameOnWeb { get; set; }
        public int ClassType { get; set; }
        public string WebUniqueId { get; set; }
        public Guid? InsurancePolicy { get; set; }
    }
}