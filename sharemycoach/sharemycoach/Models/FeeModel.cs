using System;

namespace sharemycoach.Models
{
    public class FeeModel
    {
        public Guid Oid { get; set; }
        public Guid? Location { get; set; }
        public string Name { get; set; }
        public string WebPrice { get; set; }
    }
}