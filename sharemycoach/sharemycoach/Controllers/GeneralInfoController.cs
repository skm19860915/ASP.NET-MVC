﻿using System.Web.Mvc;
using sharemycoach.Models;

namespace sharemycoach.Controllers
{
    public class GeneralInfoController : BaseController
    {
        public GeneralInfoController()
        {
            ViewBag.Title = "RV General Information";
        }

        public ActionResult Index(string location)
        {
            if (string.IsNullOrEmpty(location))
                return View();

            var locationInfo = GetTargetLocationInfo(location);
            if (locationInfo == null)
                return RedirectToAction("Index", "Error");

            ViewBag.SearchLocationSequenceId = locationInfo.SequenceId;
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