using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class LearnMoreController : BaseController
    {
        public LearnMoreController()
        {
            ViewBag.Title = "RV Learn More";
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}