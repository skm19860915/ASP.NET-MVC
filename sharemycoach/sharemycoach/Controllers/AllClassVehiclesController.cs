using sharemycoach.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace sharemycoach.Controllers
{
    public class AllClassVehiclesController : BaseController
    {
        public AllClassVehiclesController()
        {
            var locationInfos = _wc.GetAllLocations(_token);
            ViewBag.LocationDatas = locationInfos;
        }

        [Route("{WebCityStaticPage}/AllClassVehicles/Location/{name}")]
        public ActionResult Index(string name)
        {
            return RedirectToAction("Location", new RouteValueDictionary(new { controller = "AllClassVehicles", action = "Location", id = name }));
        }

        #region New Solution
        [Route("AllClassVehicles/{id}")]
        public ActionResult Location(string id, string cls)
        {
            var locationInfo = GetTargetLocationInfo(id);

            if (string.IsNullOrEmpty(cls))
            {
                ViewBag.Title = "RV One Vehicle Per Class In " + locationInfo.City + " " + locationInfo.State + " | " + Properties.Resources.SHARE_MY_COACH;

                var foundClasses = GetAllClassesInLocation(locationInfo);
                if (foundClasses)
                    return View("AllClasses");

                return View("Error");
            }

            ViewBag.Title = "RV All Vehicles In " + locationInfo.City + " " + locationInfo.State + " | " + Properties.Resources.SHARE_MY_COACH;

            var foundVehicles = GetAllActiveVehiclesInLocation(locationInfo, cls);
            if (foundVehicles)
                return View("Index");

            return View("Error");
        }

        private bool GetAllClassesInLocation(LocationModel info)
        {
            if (info == null)
                return false;

            ViewBag.LocationInfo = info;
            ViewBag.API = info.WebGoogleMapJavaScriptAPIKey;

            var randomVehiclesInLocation = _wc.GetAllRandomVehicles(info.SequenceId ?? 0, _token);
            if (randomVehiclesInLocation == null || randomVehiclesInLocation.Count() < 1)
                return false;

            ViewBag.RandomVehiclesInLocation = randomVehiclesInLocation;

            return true;
        }

        private bool GetAllActiveVehiclesInLocation(LocationModel info, string cls)
        {
            if (info == null)
                return false;

            ViewBag.LocationInfo = info;
            var allActiveVehiclesInLocation = _wc.GetAllActiveVehicles(info.SequenceId ?? 0, _token);
            if (allActiveVehiclesInLocation == null || allActiveVehiclesInLocation.Count() < 1)
                return false;

            ViewBag.AllActiveVehiclesInLocation = allActiveVehiclesInLocation;
            ViewBag.API = info.WebGoogleMapJavaScriptAPIKey;

            ViewBag.Class = cls;
            return true;
        }
        #endregion

        #region Old Solution
        //[RandFeaturedViewModel]
        //[Route("AllClassVehicles/{id}")]
        //public ActionResult Location(string id, string cls)
        //{
        //    var locationInfo = GetTargetLocationInfo(id);

        //    if (string.IsNullOrEmpty(cls))
        //    {
        //        ViewBag.Title = "RV One Vehicle Per Class In " + locationInfo.City + " " + locationInfo.State + " | " + Properties.Resources.SHARE_MY_COACH;

        //        var foundClasses = GetAllClassesInCurrentLocation(locationInfo);
        //        if (foundClasses)
        //            return View("AllClasses");

        //        return View("Error");
        //    }

        //    ViewBag.Title = "RV All Vehicles In " + locationInfo.City + " " + locationInfo.State + " | " + Properties.Resources.SHARE_MY_COACH;

        //    var foundVehicles = GetAllVehiclesInCurrentLocation(locationInfo, cls);
        //    if (foundVehicles)
        //        return View("Index");

        //    return View("Error");
        //}

        //private bool GetAllVehiclesInCurrentLocation(LocationModel info, string cls)
        //{
        //    if (info == null)
        //        return false;

        //    ViewBag.LocationInfo = info;
        //    ViewBag.LocationSequenceId = info.SequenceId;
        //    var allFeaturedVehiclesInLocation = _wc.GetAllFeaturedVehicles(info.SequenceId ?? 0, _token);
        //    if (allFeaturedVehiclesInLocation == null || allFeaturedVehiclesInLocation.Count() < 1)
        //        return false;

        //    ViewBag.AllFeaturedVehiclesInLocation = allFeaturedVehiclesInLocation;
        //    ViewBag.WebLocationPageHTML = allFeaturedVehiclesInLocation.FirstOrDefault().WebLocationPageHTML;
        //    ViewBag.WebLocationPageHTMLBottom = allFeaturedVehiclesInLocation.FirstOrDefault().WebLocationPageHTMLBottom;
        //    ViewBag.API = info.WebGoogleMapJavaScriptAPIKey;

        //    ViewBag.Class = cls;
        //    return true;
        //}

        //private bool GetAllClassesInCurrentLocation(LocationModel info)
        //{
        //    if (info == null)
        //        return false;

        //    ViewBag.LocationInfo = info;
        //    ViewBag.API = info.WebGoogleMapJavaScriptAPIKey;
        //    var randFeaturesPerLocation = _wc.GetAllRandFeatureds(info.SequenceId ?? 0, _token);
        //    if (randFeaturesPerLocation == null || randFeaturesPerLocation.Count() < 1)
        //        return false;

        //    ViewBag.RandFeaturesInLocation = randFeaturesPerLocation;
        //    ViewBag.FeaturesCount = randFeaturesPerLocation.Count();

        //    return true;
        //}
        #endregion
    }
}