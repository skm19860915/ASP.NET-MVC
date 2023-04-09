using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class NewsLetterController : BaseController
    {

        public NewsLetterController()
        {
            ViewBag.Title = "RV News Letter";
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}