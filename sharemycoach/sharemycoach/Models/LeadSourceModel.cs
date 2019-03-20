using System;

namespace sharemycoach.Models
{
    public class LeadSourceModel
    {
        public Guid Oid { get; set; }
        public Guid? Location { get; set; }
        public string Name { get; set; }
    }
}