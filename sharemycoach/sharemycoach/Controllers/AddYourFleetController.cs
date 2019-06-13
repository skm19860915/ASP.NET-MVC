using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class AddYourFleetController : BaseController
    {
        public AddYourFleetController()
        {
            ViewBag.Title = "RV Add Your Fleet | " + Properties.Resources.SHARE_MY_COACH;
        }

        public ActionResult Specific(string location)
        {
            var dirNames = GetCustomerDirectoryList();
            ViewBag.DirNames = dirNames;
            if (string.IsNullOrEmpty(location))
            {
                ViewBag.SearchLocationSequenceId = 0;
                return View();
            }

            var locationInfo = GetTargetLocationInfo(location);
            if (locationInfo == null)
                return RedirectToAction("Index", "Error");

            ViewBag.SearchLocationSequenceId = locationInfo.SequenceId;
            return View();
        }
    }
}