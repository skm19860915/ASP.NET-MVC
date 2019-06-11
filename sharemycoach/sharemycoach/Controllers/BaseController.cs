using sharemycoach.Models;
using sharemycoach.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Data;

namespace sharemycoach.Controllers
{
    public class BaseController : Controller
    {
        public WebAPI2Client _wc;
        public string _token;
        public CompanyModel _companyInfos;
        private IEnumerable<WebCityStaticPageTitleViewModel> destinationTitles;
        private IEnumerable<WebCityStaticPageTitleViewModel> eventTitles;

        public BaseController()
        {
            _wc = new WebAPI2Client();
            _token = GetTokenParameter();

            var webCityPageInfos = _wc.GetAllWebCityStaticPages(_token);

            if(webCityPageInfos != null)
            {
                destinationTitles = from w in webCityPageInfos
                                      where w.IsPointOfInterest == true
                                      select new WebCityStaticPageTitleViewModel
                                      {
                                          Location = w.Location,
                                          MetaTitle = w.MetaTitle,
                                          ControllerName = w.ControllerName,
                                          ActionName = w.ActionName
                                      };

                eventTitles = from w in webCityPageInfos
                                where w.IsEvent == true
                                select new WebCityStaticPageTitleViewModel
                                {
                                    Location = w.Location,
                                    MetaTitle = w.MetaTitle,
                                    ControllerName = w.ControllerName,
                                    ActionName = w.ActionName
                                };
            }

            ViewBag.DestinationList = destinationTitles;
            ViewBag.EventList = eventTitles;

            var locationBaseInfos = _wc.GetAllBaseInfosOfLocation(_token);
            ViewBag.RightList = locationBaseInfos;

            var companyInfos = _wc.GetCompany(_token);
            _companyInfos = companyInfos;
            var footerData = companyInfos.WebfooterCopyright;
            ViewBag.FooterData = footerData;
        }

        private string GetTokenParameter()
        {
            var receiver = new TokenReceiver();
            var token = receiver.GetRMXToken();
            return token;
        }

        public string GetCustomerDirectoryList()
        {
            var rootPath = HttpContext.Server.MapPath("~");
            var dirs = new DirectoryInfo(rootPath + "UserContent").GetDirectories();
            string dirNameList = null;
            foreach(var dir in dirs)
            {
                dirNameList += dir.Name + ",";
            }
            return dirNameList;
        }

        public LocationModel GetTargetLocationInfo(string location)
        {
            var locations = _wc.GetAllLocations(_token);
            var param = location.Split('-').ToList();
            var locationInfo = locations.FirstOrDefault(x => x.State == param[0] && x.City == param[1]);
            return locationInfo;
        }

        public int GetOldVehicleIdFromOldUrl(string id)
        {
            var parameters = id.Split('-').ToList();
            if (parameters == null || parameters.Count() < 1)
                return -1;
                
            var result = parameters.FirstOrDefault();
            var oldId = Regex.Match(result, @"\d+").Value;
            return Convert.ToInt32(oldId);
        }
    }
}