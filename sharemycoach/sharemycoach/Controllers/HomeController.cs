using sharemycoach.Models.Location;
using sharemycoach.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
            ViewBag.Title = "RV Rentals | Rent an RV | " + Properties.Resources.SHARE_MY_COACH;
            ViewBag.WebAllLocationsHTML = _companyInfos.WebAllLocationsHTML;
        }

        [RandFeaturedViewModel]
        public ActionResult Index()
        {
            var locationInfos = _wc.GetAllLocations(_token).Where(x => x.IsLocationBehindOnPaying != true);
            if (locationInfos == null || locationInfos.Count() < 1)
                return RedirectToAction("Index", "Error");

            var locationInfosOfHome = from l in locationInfos
                                        select new LocationBaseInfoModel
                                        {
                                            SequenceId = l.SequenceId,
                                            City = l.City,
                                            State = l.State,
                                            WebRegionalName = l.WebRegionalName
                                        };

            ViewBag.locationInfosOfHome = locationInfosOfHome;

            var allRandFeaturedVehicles = _wc.GetAllRandFeatureds(_token);
            if (allRandFeaturedVehicles == null || allRandFeaturedVehicles.Count() < 1)
                return RedirectToAction("Index", "Error");

            ViewBag.AllRandFeaturedVehicles = allRandFeaturedVehicles;
            return View();
        }
    }
}