using sharemycoach.Models;
using sharemycoach.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class VehicleQuoteController : BaseController
    {
        public VehicleQuoteController()
        {
            ViewBag.Title = "RV Vehicle Quote";
        }
        public ActionResult Index(string id)
        {
            var oldId = GetOldVehicleIdFromOldUrl(id);
            var matchVehicle = _wc.GetMatchVehicle(oldId, _token);
            if (matchVehicle == null)
                return RedirectToAction("Index", "Error");

            if (matchVehicle.IsActive == true)
            {
                var webUniqueId = matchVehicle.WebUniqueId;
                var detailInformation = _wc.GetDetail(webUniqueId, false, _token);
                if (detailInformation == null)
                    return RedirectToAction("Index", "Error");
                    
                GetCheckOutInfo(detailInformation);
            }
            else
                return RedirectToAction("Index", "Home");
            
            return View();
        }

        private void GetCheckOutInfo(VehicleDetailViewModel info)
        {
            ViewBag.Info = info;
            var serviceList = _wc.GetAllServiceList(info.Location ?? Guid.Empty, "sharemycoach.com", _token);
            var optionalItemList = GetSortOptionalItemList(serviceList);
            var optionalItemListInSelectedLocation = optionalItemList.Where(x => x.Location == info.Location).ToList();
            ViewBag.OptionalItemList = optionalItemListInSelectedLocation;
            ViewBag.InsuranceCompanies = serviceList.LocationInsuranceCompanyList;
            ViewBag.LeadSources = serviceList.LeadSourceList;
        }

        private List<OptionalItemViewModel> GetSortOptionalItemList(ServiceListViewModel model)
        {
            var equipments = model.EquipmentTypeList;
            var fees = model.FeeList;
            if (equipments != null && equipments.Count() > 0)
            {
                var arr = new List<OptionalItemViewModel>();
                foreach (var e in equipments)
                {
                    var record1 = new OptionalItemViewModel()
                    {
                        Oid = e.Oid,
                        Name = e.Name,
                        WebPrice = e.WebPrice,
                        Location = e.Location,
                        IsEquipmentType = true
                    };
                    arr.Add(record1);
                }
                if (fees != null && fees.Count() > 0)
                {
                    foreach (var f in fees)
                    {
                        var record2 = new OptionalItemViewModel()
                        {
                            Oid = f.Oid,
                            Name = f.Name,
                            WebPrice = f.WebPrice,
                            Location = f.Location,
                            IsEquipmentType = false
                        };
                        arr.Add(record2);
                    }
                }
                return arr.OrderBy(x => x.Name).ToList();
            }
            return null;
        }
    }
}