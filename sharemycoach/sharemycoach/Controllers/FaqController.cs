using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class FaqController : BaseController
    {
        public FaqController()
        {
            ViewBag.Title = "RV Faq";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Specific()
        {
            var dirNames = GetCustomerDirectoryList();
            ViewBag.DirNames = dirNames;
            return View();
        }
    }
}