using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class QuoteResultController : BaseController
    {
        public QuoteResultController()
        {
            ViewBag.Title = "Quote Result | " + Properties.Resources.SHARE_MY_COACH;
        }

        public ActionResult WebQuoteSuccess()
        {
            var infos = TempData["Infos"];
            ViewBag.Infos = infos;
            return View();
        }

        public ActionResult WebQuoteFail()
        {
            return View();
        }

        public ActionResult MobileQuoteSuccess()
        {
            return View();
        }

        public ActionResult MobileQuoteFail()
        {
            return View();
        }
    }
}