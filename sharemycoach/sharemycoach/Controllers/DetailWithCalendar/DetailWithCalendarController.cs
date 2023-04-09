using sharemycoach.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sharemycoach.Controllers.DetailWithCalendar
{
    public class DetailWithCalendarController : BaseController
    {
        public ActionResult Index(string id)
        {
            var webUniqueIdsForDetailWithCalendar = _wc.GetVehicleKeysForExportingToDetailWithCalendar(id, _token);
            if (webUniqueIdsForDetailWithCalendar == null || webUniqueIdsForDetailWithCalendar.Count() < 1)
                return View("NotFoundVehlcle");

            if (webUniqueIdsForDetailWithCalendar.Count() == 1)
            {
                var webUniqueId = webUniqueIdsForDetailWithCalendar.FirstOrDefault();
                var detailInfo = _wc.GetDetailInfo(webUniqueId, false, _token);
                if (detailInfo == null)
                {
                    detailInfo = _wc.GetDetailInfo(webUniqueId, true, _token);
                    if (detailInfo == null)
                        return RedirectToAction("Index", "Error");
                }

                ViewBag.Title = detailInfo.NameOnWeb + " - " + detailInfo.LocationName + " - RV Rentals | " + Properties.Resources.SHARE_MY_COACH;
                ViewBag.Info = detailInfo;
                ViewBag.IsDetailWithCalendar = true;
                return View();
            }

            var infosForDetailWithCalendar = _wc.GetInfosForDetailWithCalendar(id, _token);
            ViewBag.Infos = infosForDetailWithCalendar;

            return View("ListPage");
        }
    }
}