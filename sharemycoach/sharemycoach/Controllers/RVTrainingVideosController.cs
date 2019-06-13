using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class RVTrainingVideosController : BaseController
    {
        public RVTrainingVideosController()
        {
            ViewBag.Title = "Private RV Training Video";
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}