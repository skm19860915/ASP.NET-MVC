namespace sharemycoach.Models
{
    public class CompanyModel
    {
        public System.Guid Oid { get; set; }
        public double? GrandTotalRentalMiles { get; set; }
        public double? GrandTotalRentals { get; set; }
        public double? GrandTotalVehiclesAvailable { get; set; }
        public string WebfooterCopyright { get; set; }
        public bool? IsAllowSmartyStreets { get; set; }
        public string SmartyStreetsAuthorizationId { get; set; }
        public string SmartyStreetsAuthorizationToken { get; set; }
        public string SmartyStreetsWebsiteKey { get; set; }
        public string WebAllLocationsHTML { get; set; }
    }
}