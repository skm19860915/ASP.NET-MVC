using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sharemycoach.ViewModels
{
    public class QuoteInfoViewModel
    {
        public DetailInfoViewModel detailInfo { get; set; }
        public decimal? HigherRate { get; set; }
        public int? OldVehicleID { get; set; }
        public string DBAName { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string WebGoogleMapJavaScriptAPIKey { get; set; }
        public Guid? Organization { get; set; }
        public string OrganizationName { get; set; }
        public string OutgoingUserName { get; set; }
        public string OutgoingServerName { get; set; }
        public int? OutgoingServerPort { get; set; }
        public string OutgoingPassword { get; set; }
        public string EmailAddress { get; set; }
        public string WebRegionalName { get; set; }
        public bool? CalcByNights { get; set; }
        public string WebQuoteEmailAddress { get; set; }
        public string EmailSupportRequestAddress { get; set; }
        public string FriendlyCompanyName { get; set; }
        public Guid ClassOid { get; set; }
    }
}