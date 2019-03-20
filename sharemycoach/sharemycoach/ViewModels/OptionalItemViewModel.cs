using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sharemycoach.ViewModels
{
    public class OptionalItemViewModel
    {
        public Guid Oid { get; set; }
        public string Name { get; set; }
        public string WebPrice { get; set; }
        public bool IsEquipmentType { get; set; }
        public Guid? Location { get; set; }
    }
}