using System.Linq;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class DestinationController : BaseController
    {
        [Route("Destination/{id}")]
        public ActionResult Index(string id)
        {
            var targetDestinationData = _webCityPageInfos.FirstOrDefault(x => x.ActionName == id);
            if(targetDestinationData == null)
                return RedirectToAction("Index", "Error");

            ViewBag.Title = id + " | " + Properties.Resources.SHARE_MY_COACH;

            var htmlText = targetDestinationData.HTMLPageContent;

            ViewBag.Content = htmlText;
            return View();
        }
    }
}