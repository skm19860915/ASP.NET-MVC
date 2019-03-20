using sharemycoach.ViewModels;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class VehicleDetailForSaleController : BaseController
    {
        public VehicleDetailForSaleController()
        {
            ViewBag.Title = "36FT Pace Arrow 1240 - | " + Properties.Resources.SHARE_MY_COACH;
        }

        public ActionResult Index(string id)
        {
            VehicleSaleDetailViewModel detailSaleInfo = null;
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
                
            ViewBag.Info = detailSaleInfo;
            return View();
        }
    }
}