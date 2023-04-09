using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class WorksController : BaseController
    {
        public WorksController()
        {
            ViewBag.Title = "RV How ShareMyCoach Works";
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}