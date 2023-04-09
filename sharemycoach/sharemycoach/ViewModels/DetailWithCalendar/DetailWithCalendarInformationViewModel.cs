using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sharemycoach.ViewModels.DetailWithCalendar
{
    public class DetailWithCalendarInformationViewModel
    {
        public string NameOnWeb { get; set; }
        public string WebUniqueId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public byte[] PrimaryPhoto { get; set; }
        public bool IsSquarePhoto { get; set; }
    }
}