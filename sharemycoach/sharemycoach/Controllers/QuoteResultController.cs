using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class QuoteResultController : BaseController
    {
        // GET: QuoteResult
        public ActionResult Success()
        {
            var infos = TempData["Infos"];
            ViewBag.Infos = infos;
            return View();
        }

        public ActionResult Fail()
        {
            return View();
        }
    }
}