using sharemycoach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sharemycoach.ViewModels
{
    public class DetailInfoViewModel
    {
        public Guid Oid { get; set; }
        public string WebUniqueId { get; set; }
        public string QuickFindKeyWord { get; set; }
        public Guid? Location { get; set; }
        public string LocationName { get; set; }
        public Guid? VehicleClass { get; set; }
        public string NameOnWeb { get; set; }
        public string DailyRate { get; set; }
        public string YouTubeID { get; set; }
        public bool? NoTowing { get; set; }
        public int? Belts { get; set; }
        public int? Length { get; set; }
        public bool? NoDogs { get; set; }
        public int? FuelType { get; set; }
        public string Model { get; set; }
        public bool? SmokingAllowed { get; set; }
        public string Make { get; set; }
        public string Year { get; set; }
        public int? Children { get; set; }
        public int? Adolescents { get; set; }
        public int? Adults { get; set; }
        public string WebDescription { get; set; }
        public string OtherCostsDesc { get; set; }
        public long? VehicleSequenceId { get; set; }
        public decimal? WebPrepFee { get; set; }
        public decimal? WebCleaningFee { get; set; }
        public decimal? WebRefundableSecurityDeposit { get; set; }
        public int? WebGeneratorFreeHours { get; set; }
        public int? WebIncludesTheseMilesFreePerDay { get; set; }
        public string WebTransportedID { get; set; }
        public Guid? InsurancePolicy { get; set; }
        public bool? ShowForSale { get; set; }
        public DateTime? ForSaleOn { get; set; }
        public int? SalePrice { get; set; }
        //location
        public bool? isShowCalendarOnWeb { get; set; }
        public bool? IsCalendarWithBookings { get; set; }
        public string PrimaryPhone { get; set; }
        public long? LocationSequenceId { get; set; }
        public bool? IsLocationBehindOnPaying { get; set; }
        public int? MinimumNumberOfTimeInterval { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        // vehicleclass
        public string ClassName { get; set; }
        public int? ClassType { get; set; }
        // rental list
        public List<RentalModel> RentalList { get; set; }
        // amenity list
        public List<AmenityViewModel> AmenityList { get; set; }
        // sort vehicle list
        public List<VehicleInfoViewModel> VehicleList { get; set; }
        // random other vehicle list
        public List<OtherVehicleViewModel> OtherVehicles { get; set; }
    }
}