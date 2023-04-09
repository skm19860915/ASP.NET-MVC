using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class AboutUsController : BaseController
    {
        public AboutUsController()
        {
            ViewBag.Title = "RV Rental About Us | " + Properties.Resources.SHARE_MY_COACH;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}