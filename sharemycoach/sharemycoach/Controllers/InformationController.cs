using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class InformationController : BaseController
    {
        public InformationController()
        {
            ViewBag.Title = "RV Our Information";
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}