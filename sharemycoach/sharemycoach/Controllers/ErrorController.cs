using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "RV 404 Page | " + Properties.Resources.SHARE_MY_COACH;
            return View();
        }
    }
}