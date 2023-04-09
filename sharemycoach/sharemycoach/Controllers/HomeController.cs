using sharemycoach.ViewModels.Location;
using System.Linq;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class HomeController : BaseController
    {
        #region New Solution
        public HomeController()
        {
            ViewBag.Title = "RV Rentals | Rent an RV | " + Properties.Resources.SHARE_MY_COACH;
            ViewBag.WebAllLocationsHTML = _companyInfos.WebAllLocationsHTML;
        }

        public ActionResult Index()
        {
            var locationInfos = _wc.GetAllLocations(_token).Where(x => x.IsLocationBehindOnPaying != true);
            if (locationInfos == null || locationInfos.Count() < 1)
                return RedirectToAction("Index", "Error");

            var locationInfosOfHome = from l in locationInfos
                                      select new LocationBaseInfoViewModel
                                      {
                                          SequenceId = l.SequenceId,
                                          City = l.City,
                                          State = l.State,
                                          WebRegionalName = l.WebRegionalName
                                      };

            ViewBag.locationInfosOfHome = locationInfosOfHome;

            var allRandomVehicles = _wc.GetAllRandomVehicles(_token);
            if (allRandomVehicles == null || allRandomVehicles.Count() < 1)
                return RedirectToAction("Index", "Error");

            ViewBag.AllRandomVehicles = allRandomVehicles;
            return View();
        }
        #endregion

        #region Old Solution
        //[RandFeaturedViewModel]
        //public ActionResult Index()
        //{
        //    var locationInfos = _wc.GetAllLocations(_token).Where(x => x.IsLocationBehindOnPaying != true);
        //    if (locationInfos == null || locationInfos.Count() < 1)
        //        return RedirectToAction("Index", "Error");

        //    var locationInfosOfHome = from l in locationInfos
        //                              select new LocationBaseInfoViewModel
        //                              {
        //                                  SequenceId = l.SequenceId,
        //                                  City = l.City,
        //                                  State = l.State,
        //                                  WebRegionalName = l.WebRegionalName
        //                              };

        //    ViewBag.locationInfosOfHome = locationInfosOfHome;

        //    var allRandFeaturedVehicles = _wc.GetAllRandFeatureds(_token);
        //    if (allRandFeaturedVehicles == null || allRandFeaturedVehicles.Count() < 1)
        //        return RedirectToAction("Index", "Error");

        //    ViewBag.AllRandFeaturedVehicles = allRandFeaturedVehicles;
        //    return View();
        //}
        #endregion
    }
}