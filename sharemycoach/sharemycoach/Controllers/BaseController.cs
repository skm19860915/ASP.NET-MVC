using sharemycoach.Models;
using sharemycoach.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class BaseController : Controller
    {
        public WebAPI2Client _wc;
        public CompanyModel _companyInfos;
        public IEnumerable<LocationModel> _locationInfos;
        public IEnumerable<WebCityStaticPageViewModel> _webCityPageInfos;
        public string _token;

        public BaseController()
        {
            _wc = new WebAPI2Client();
            _token = GetTokenParameter();

            var webCityPageInfos = _wc.GetAllWebCityStaticPages(_token);

            _webCityPageInfos = webCityPageInfos;
            
            if(webCityPageInfos != null)
            {
                var rentalDestinationList = webCityPageInfos.Where(x => x.IsPointOfInterest == true).ToList();
                var rentalEventList = webCityPageInfos.Where(x => x.IsEvent == true).ToList();

                ViewBag.DestinationList = rentalDestinationList;
                ViewBag.EventList = rentalEventList;
            }
            else
            {
                ViewBag.DestinationList = null;
                ViewBag.EventList = null;
            }

            var locationInfos = _wc.GetAllLocations(_token);
            ViewBag.RightList = locationInfos;

            _locationInfos = locationInfos;

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
            var locations = _locationInfos;
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