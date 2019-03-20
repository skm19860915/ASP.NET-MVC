using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class RVOwnersController : BaseController
    {
        public RVOwnersController()
        {
            ViewBag.Title = "RV Owners | " + Properties.Resources.SHARE_MY_COACH;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}