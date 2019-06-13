using System;
using System.Web.Mvc;

namespace sharemycoach.Models
{
    public class VehicleMediaItemModel : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            base.OnResultExecuting(filterContext);
        }

        public Guid? Vehicle { get; set; }
        public byte[] Photo { get; set; }
        public int? PhotoCode { get; set; }
    }
}