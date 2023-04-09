using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sharemycoach.ViewModels
{
    public class WebCityStaticPageTitleViewModel
    {
        public Guid? Location { get; set; }
        public string MetaTitle { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}