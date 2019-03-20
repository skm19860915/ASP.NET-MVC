using sharemycoach.Models;
using System.Linq;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class VehicleRentalsController : BaseController
    {
        public VehicleRentalsController()
        {
            ViewBag.Title = "RV One Vehicle Per Class In Given Location";
            ViewBag.LocationDatas = _locationInfos;
        }

        public ActionResult Index(string typeid = "All")
        {
            var id = "CA-Stanton";
            var locationInfo = GetTargetLocationInfo(id);
            if(locationInfo == null)
                return RedirectToAction("Index", "Error");

            var foundVehicles = GetAllVehiclesInCurrentLocation(locationInfo, typeid);
            if (foundVehicles)
                return View();

            return View("Error");
        }

        private bool GetAllVehiclesInCurrentLocation(LocationModel info, string cls)
        {
            if (info == null)
                return false;

            ViewBag.LocationInfo = info;
            var allFeaturedVehiclesInLocation = _wc.GetAllFeaturedVehicles(info.SequenceId ?? 0, _token);
            if (allFeaturedVehiclesInLocation == null || allFeaturedVehiclesInLocation.Count() < 1)
                return false;

            ViewBag.AllFeaturedVehiclesInLocation = allFeaturedVehiclesInLocation;
            ViewBag.WebLocationPageHTML = allFeaturedVehiclesInLocation.FirstOrDefault().WebLocationPageHTML;

            ViewBag.Class = cls;
            return true;
        }
    }
}