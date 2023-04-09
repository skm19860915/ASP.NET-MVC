﻿using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class CampgroundController : BaseController
    {
        public CampgroundController()
        {
            ViewBag.Title = "RV Campgrounds Map | " + Properties.Resources.SHARE_MY_COACH;
        }

        public ActionResult Index(string location)
        {
            if (string.IsNullOrEmpty(location))
                return View();

            var locationInfo = GetTargetLocationInfo(location);
            if (locationInfo == null)
                return RedirectToAction("Index", "Error");

            ViewBag.SearchLocationSequenceId = locationInfo.SequenceId;
            ViewBag.api = locationInfo.WebGoogleMapJavaScriptAPIKey;
            var dirNames = GetCustomerDirectoryList();
            ViewBag.DirNames = dirNames;

            return View("Specific");
        }

        public ActionResult Specific()
        {
            ViewBag.SearchLocationSequenceId = 0;
            var dirNames = GetCustomerDirectoryList();
            ViewBag.DirNames = dirNames;
            return View();
        }
    }
}