using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class ResourcesController : BaseController
    {
        public ResourcesController()
        {
            ViewBag.Title = "RV Resources | " + Properties.Resources.SHARE_MY_COACH;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}