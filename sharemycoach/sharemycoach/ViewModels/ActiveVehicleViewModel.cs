using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sharemycoach.ViewModels
{
    public class ActiveVehicleViewModel
    {
        public Guid Oid { get; set; }
        public string QuickFindKeyWord { get; set; }
        public string NameOnWeb { get; set; }
        public string DailyRate { get; set; }
        public Guid? VehicleClass { get; set; }
        public double? WebPriceGroup { get; set; }
        public long? VehicleSequenceId { get; set; }
        public int? Adolescents { get; set; }
        public int? Adults { get; set; }
        public int? Children { get; set; }
        public decimal? WebCleaningFee { get; set; }
        public decimal? WebPrepFee { get; set; }
        public decimal? WebRefundableSecurityDeposit { get; set; }
        public int? WebGeneratorFreeHours { get; set; }
        public int? WebGeneratorAddHoursEach { get; set; }
        public int? WebIncludesTheseMilesFreePerDay { get; set; }
        public bool? FeaturedVehicle { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string WebDescription { get; set; }
        public string WebUniqueId { get; set; }
        public Guid? InsurancePolicy { get; set; }
        public int? ClassType { get; set; }
        public Guid? Location { get; set; }
        public string WebRegionalName { get; set; }
        public bool? CalcByNights { get; set; }
        public int? MinimumNumberOfTimeInterval { get; set; }
        public bool? ShowForSale { get; set; }
        public DateTime? ForSaleOn { get; set; }
        public int? SalePrice { get; set; }

        public decimal? HigherRate { get; set; }
        public List<string> DateList { get; set; }
    }
}