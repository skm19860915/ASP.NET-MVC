using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sharemycoach.Controllers.Reviews
{
    public class ReviewsController : Controller
    {
        [Route("Reviews")]
        public ActionResult Index()
        {
            return View();
        }
    }
}