using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class WebMailController : Controller
    {
        // GET: WebMail
        public ActionResult Index()
        {
            return Redirect("http://www.ewebsitedev.com/IClient/login.aspx");
        }
    }
}