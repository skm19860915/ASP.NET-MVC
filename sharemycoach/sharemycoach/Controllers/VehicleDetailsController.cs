﻿using sharemycoach.Models;
using sharemycoach.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class VehicleDetailsController : BaseController
    {
        public ActionResult Index(int id)
        {
            var matchVehicle = _wc.GetMatchVehicle(id, _token);
            if (matchVehicle == null)
                return RedirectToAction("Index", "Error");

            if (matchVehicle.IsActive == true)
            {
                var webUniqueId = matchVehicle.WebUniqueId;
                var detailInfo = _wc.GetDetail(webUniqueId, _token);
                if (detailInfo == null)
                    return RedirectToAction("Index", "Error");

                GetDetailInfo(detailInfo);
                return View("Index");
            }

            var locationOid = matchVehicle.Location;
            var locationInfo = _locationInfos.FirstOrDefault(x => x.Oid == locationOid);
            if (locationInfo == null)
                return RedirectToAction("Index", "Error");

            var vehicleClass = matchVehicle.VehicleClass;
            var foundSimilarVehicles = GetAllSimilarVehiclesInCurrentLocation(locationInfo, vehicleClass);
            if (foundSimilarVehicles)
                return View("Other");

            return View("Error");
        }

        private bool GetAllSimilarVehiclesInCurrentLocation(LocationModel info, Guid? cls)
        {
            if (info == null)
                return false;

            ViewBag.LocationInfo = info;
            var allFeaturesInCurrentLocation = _wc.GetAllFeaturedVehicles(info.SequenceId ?? 0, _token);
            if (allFeaturesInCurrentLocation == null || allFeaturesInCurrentLocation.Count() < 1)
                return false;

            var similarFeaturesInCurrentLocation = allFeaturesInCurrentLocation.Where(x => x.VehicleClass == cls);
            if (similarFeaturesInCurrentLocation == null || similarFeaturesInCurrentLocation.Count() < 1)
                return false;

            ViewBag.SimilarFeaturesInLocation = similarFeaturesInCurrentLocation;
            return true;
        }

        private void GetDetailInfo(VehicleDetailViewModel info)
        {
            ViewBag.Title = info.NameOnWeb + " - " + info.DBAName + " - RV Rentals";
            TempData["Detail"] = info;
            TempData.Keep();
            ViewBag.Info = info;
        }
    }
}