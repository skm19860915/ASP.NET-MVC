using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class ShowMeHowController : BaseController
    {
        public ShowMeHowController()
        {
            ViewBag.Title = "Show Me How";
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}