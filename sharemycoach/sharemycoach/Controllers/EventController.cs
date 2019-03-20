﻿using System.Linq;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class EventController : BaseController
    {
        [Route("Event/{id}")]
        public ActionResult Index(string id)
        {
            var targetEventData = _webCityPageInfos.FirstOrDefault(x => x.ActionName == id);
            if(targetEventData == null)
                return RedirectToAction("Index", "Error");

            ViewBag.Title = id + " | " + Properties.Resources.SHARE_MY_COACH;

            var htmlText = targetEventData.HTMLPageContent;

            ViewBag.Content = htmlText;
            return View();
        }
    }
}