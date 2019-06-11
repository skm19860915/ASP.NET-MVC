using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class PageController : BaseController
    {
        private string _pattern;

        public ActionResult Index(string id)
        {
            if(id.Equals("privacypolicy.aspx", System.StringComparison.InvariantCultureIgnoreCase))
                return View("PrivacyPolicy");

            _pattern = Regex.Match(id, @"RV-Rentals-([A-Za-z\-]+)\.aspx$", RegexOptions.IgnoreCase).Groups[1].Value;
            if (string.IsNullOrEmpty(_pattern))
            {
                _pattern = Regex.Match(id, @"([A-Za-z\-]+)\-RV-Rentals.aspx$", RegexOptions.IgnoreCase).Groups[1].Value;
                if (string.IsNullOrEmpty(_pattern))
                    _pattern = Regex.Match(id, @"([A-Za-z\-]+)\.aspx$", RegexOptions.IgnoreCase).Groups[1].Value;
            }

            if (string.IsNullOrEmpty(_pattern))
                return RedirectToAction("Index", "Error");

            var webCityPageInfos = _wc.GetAllWebCityStaticPages(_token);
            var cityPage = webCityPageInfos.FirstOrDefault(x => x.ActionName.ToLower().Contains(_pattern.ToLower()));
            if (cityPage == null)
                return RedirectToAction("Index", "Error");

            ViewBag.Content = cityPage.HTMLPageContent;
            return View();
        }
    }
}