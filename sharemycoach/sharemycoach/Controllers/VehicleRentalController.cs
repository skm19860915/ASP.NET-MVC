using sharemycoach.Models;
using sharemycoach.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class VehicleRentalController : BaseController
    {
        public VehicleRentalController()
        {
            ViewBag.Title = "RV In Details";
        }

        public ActionResult Index(string id)
        {
            var oldId = GetOldVehicleIdFromOldUrl(id);
            var matchVehicle = _wc.GetMatchVehicle(oldId, _token);
            if (matchVehicle == null)
                return RedirectToAction("Index", "Error");
                
            if (matchVehicle.IsActive == true)
            {
                var webUniqueId = matchVehicle.WebUniqueId;
                var detailInfo = _wc.GetDetail(webUniqueId, false, _token);
                if (detailInfo == null)
                    return RedirectToAction("Index", "Error");
                    
                GetDetailInfo(detailInfo);
                return View("Index");
            }
            var locationOid = matchVehicle.Location;
            var locationInfos = _wc.GetAllLocations(_token);
            var locationInfo = locationInfos.FirstOrDefault(x => x.Oid == locationOid);
            if (locationInfo == null)
                return RedirectToAction("Index", "Error");
                
            var vehicleClass = matchVehicle.VehicleClass;

            var foundSimilarVehicles = GetAllSimilarVehiclesInCurrentLocation(locationInfo, vehicleClass);
            if (foundSimilarVehicles)
                return View("Other");

            return View("Error");
        }

        private bool GetAllSimilarVehiclesInCurrentLocation(LocationModel info, Guid? cls)
        {
            if (info == null)
                return false;

            ViewBag.LocationInfo = info;
            var allFeaturesInCurrentLocation = _wc.GetAllFeaturedVehicles(info.SequenceId ?? 0, _token);
            if (allFeaturesInCurrentLocation == null || allFeaturesInCurrentLocation.Count() < 1)
                return false;

            var similarFeaturesInCurrentLocation = allFeaturesInCurrentLocation.Where(x => x.VehicleClass == cls);
            if (similarFeaturesInCurrentLocation == null || similarFeaturesInCurrentLocation.Count() < 1)
                return false;

            ViewBag.SimilarFeaturesInLocation = similarFeaturesInCurrentLocation;
            return true;
        }

        public void GetDetailInfo(VehicleDetailViewModel info)
        {
            ViewBag.Title = info.NameOnWeb + " - " + info.DBAName + " - RV Rentals";
            TempData["Detail"] = info;
            TempData.Keep();
            ViewBag.Info = info;
        }
    }
}