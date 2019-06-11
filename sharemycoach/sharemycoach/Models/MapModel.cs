using System;

namespace sharemycoach.Models
{
    public class MapModel
    {
        public Guid LocationOid { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string OrganizationName { get; set; }
        public string PrimaryPhone { get; set; }
        public string Zip { get; set; }
        public string Fax { get; set; }
    }
}