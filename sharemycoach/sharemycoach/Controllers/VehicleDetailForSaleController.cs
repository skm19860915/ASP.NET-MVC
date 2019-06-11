using sharemycoach.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace sharemycoach.Controllers
{
    public class VehicleDetailForSaleController : BaseController
    {
        private VehicleSaleDetailViewModel detailSaleInfo;

        public ActionResult Index(string id)
        {
            var extParam = id.Substring(id.Length - 5);
            if (extParam.Equals(".aspx"))
            {
                var oldId = GetOldVehicleIdFromOldUrl(id);
                var matchVehicle = _wc.GetMatchVehicle(oldId, _token);
                if (matchVehicle == null)
                    return RedirectToAction("Index", "Error");

                if (matchVehicle.IsActive == true)
                {
                    var webUniqueId = matchVehicle.WebUniqueId;
                    detailSaleInfo = _wc.GetSaleDetail(webUniqueId, _token);
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                detailSaleInfo = _wc.GetSaleDetail(id, _token);

            if (detailSaleInfo == null)
                return RedirectToAction("Index", "Error");

            ViewBag.Title = detailSaleInfo.NameOnWeb + " - RV Rentals | " + Properties.Resources.SHARE_MY_COACH;
                
            ViewBag.Info = detailSaleInfo;
            return View();
        }

        private List<string> GetDateTimeValues(List<DateTime> dateList)
        {
            var stringList = new List<string>();
            foreach (var item in dateList)
            {
                stringList.Add(string.Format("{0:yyyy-MM-dd}", item));
            }
            return stringList;
        }
    }
}