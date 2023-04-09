using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class OptionalEquipmentController : BaseController
    {
        public OptionalEquipmentController()
        {
            ViewBag.Title = "RV Optional Equipment";
        }

        // OptionalEquipment?location={location}
        public ActionResult Index(string location)
        {
            if (string.IsNullOrEmpty(location))
                return View();

            var locationInfo = GetTargetLocationInfo(location);
            if(locationInfo == null)
                return RedirectToAction("Index", "Error");

            ViewBag.SearchLocationSequenceId = locationInfo.SequenceId;
            var dirNames = GetCustomerDirectoryList();
            ViewBag.DirNames = dirNames;
            return View("Specific");
        }

        // OptionalEquipment.aspx?location={location}
        public ActionResult Specific()
        {
            ViewBag.SearchLocationSequenceId = 0;
            var dirNames = GetCustomerDirectoryList();
            ViewBag.DirNames = dirNames;
            return View();
        }
    }
}