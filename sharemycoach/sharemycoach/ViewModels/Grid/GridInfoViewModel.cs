using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sharemycoach.ViewModels.Grid
{
    public class GridInfoViewModel
    {
        public string QuickFindKeyWord { get; set; }
        public string NameOnWeb { get; set; }
        public string Model { get; set; }
        public int? Adults { get; set; }
        public int? Adolescents { get; set; }
        public int? Children { get; set; }
        public string DailyRate { get; set; }
        public string WebUniqueId { get; set; }
        public string ClassName { get; set; }
    }
}