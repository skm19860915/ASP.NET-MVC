using sharemycoach.Models;
using sharemycoach.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data;

namespace sharemycoach.Controllers
{
    public class AllClassVehiclesController : BaseController
    {
        public AllClassVehiclesController()
        {
            var locationInfos = _wc.GetAllLocations(_token);
            ViewBag.LocationDatas = locationInfos;
        }

        [RandFeaturedViewModel]
        [Route("AllClassVehicles/{id}")]
        public ActionResult Location(string id, string cls)
        {
            var locationInfo = GetTargetLocationInfo(id);

            if (string.IsNullOrEmpty(cls))
            {
                ViewBag.Title = "RV One Vehicle Per Class In " + locationInfo.City + " " + locationInfo.State + " | " + Properties.Resources.SHARE_MY_COACH;

                var foundClasses = GetAllClassesInCurrentLocation(locationInfo);
                if (foundClasses)
                    return View("AllClasses");
                    
                return View("Error");
            }

            ViewBag.Title = "RV All Vehicles In " + locationInfo.City + " " + locationInfo.State + " | " + Properties.Resources.SHARE_MY_COACH;

            var foundVehicles = GetAllVehiclesInCurrentLocation(locationInfo, cls);
            if (foundVehicles)
                return View("Index");
                
            return View("Error");
        }

        [Route("{WebCityStaticPage}/AllClassVehicles/Location/{name}")]
        public ActionResult Index(string name)
        {
            return RedirectToAction("Location", new RouteValueDictionary(new { controller = "AllClassVehicles", action = "Location", id = name }));
        }

        private bool GetAllVehiclesInCurrentLocation(LocationModel info, string cls)
        {
            if (info == null)
                return false;

            ViewBag.LocationInfo = info;
            ViewBag.LocationSequenceId = info.SequenceId;
            var allFeaturedVehiclesInLocation = _wc.GetAllFeaturedVehicles(info.SequenceId ?? 0, _token);
            if (allFeaturedVehiclesInLocation == null || allFeaturedVehiclesInLocation.Count() < 1)
                return false;

            ViewBag.AllFeaturedVehiclesInLocation = allFeaturedVehiclesInLocation;
            ViewBag.WebLocationPageHTML = allFeaturedVehiclesInLocation.FirstOrDefault().WebLocationPageHTML;
            ViewBag.WebLocationPageHTMLBottom = allFeaturedVehiclesInLocation.FirstOrDefault().WebLocationPageHTMLBottom;
            ViewBag.API = info.WebGoogleMapJavaScriptAPIKey;

            ViewBag.Class = cls;
            return true;
        }

        private bool GetAllClassesInCurrentLocation(LocationModel info)
        {
            if (info == null)
                return false;

            ViewBag.LocationInfo = info;
            var randFeaturesPerLocation = _wc.GetAllRandFeatureds(info.SequenceId ?? 0, _token);
            if (randFeaturesPerLocation == null || randFeaturesPerLocation.Count() < 1)
                return false;

            ViewBag.RandFeaturesInLocation = randFeaturesPerLocation;
            ViewBag.FeaturesCount = randFeaturesPerLocation.Count();
            ViewBag.API = info.WebGoogleMapJavaScriptAPIKey;
            return true;
        }

        public void GetAllVehicleListInLocation(LocationModel locationInfo, string cls)
        {
            var className = string.Empty;
            switch (cls)
            {
                case "A":
                    className = cls;
                    break;
                case "B":
                    className = cls;
                    break;
                case "C":
                    className = cls;
                    break;
                case "TH":
                    className = cls;
                    break;
                case "TT":
                    className = cls;
                    break;
                case "UT":
                    className = cls;
                    break;
                default:
                    className = "All";
                    break;
            }

            if (locationInfo == null)
                return;

            var state = locationInfo.State;
            if (string.IsNullOrEmpty(state))
                return;

            var city = locationInfo.City;
            if (string.IsNullOrEmpty(city))
                return;

            if (string.IsNullOrEmpty(cls))
                return;

            ViewBag.WebMapPhoto = locationInfo.WebMapPhoto;
            ViewBag.WebPrimaryBuildingPhoto = locationInfo.WebPrimaryBuildingPhoto;

            if (ViewBag.WebMapPhoto == null || ViewBag.WebPrimaryBuildingPhoto == null)
                return;

            if (locationInfo.Oid == null)
                return;

            var parameter = state + "-" + city + cls;

            ViewBag.Url = parameter;
        }

        
    }
}