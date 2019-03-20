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

            ViewBag.LocationInfos = _locationInfos.Where(x => x.IsLocationBehindOnPaying != true);
            ViewBag.WebAllLocationsHTML = _companyInfos.WebAllLocationsHTML;
        }

        [RandFeaturedViewModel]
        public ActionResult Index()
        {
            var allRandFeaturedVehicles = _wc.GetAllRandFeatureds(_token);
            if (allRandFeaturedVehicles == null || allRandFeaturedVehicles.Count() < 1)
                return RedirectToAction("Index", "Error");

            ViewBag.AllRandFeaturedVehicles = allRandFeaturedVehicles;
            return View();
        }
    }
}