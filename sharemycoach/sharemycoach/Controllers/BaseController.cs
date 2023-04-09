using sharemycoach.Models;
using sharemycoach.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Data;
using System.Timers;
using System.Xml;

namespace sharemycoach.Controllers
{
    public class BaseController : Controller
    {
        public WebAPI2Client _wc;
        public string _token;
        public CompanyModel _companyInfos;
        private IEnumerable<WebCityStaticPageTitleViewModel> destinationTitles;
        private IEnumerable<WebCityStaticPageTitleViewModel> eventTitles;

        #region SiteMap Solution
        //public static Timer timer = new Timer(1000);
        //public DateTime lastTriggerDate = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
        #endregion

        public BaseController()
        {
            #region SiteMap Solution
            //timer.Enabled = true;
            //timer.Interval = 1000;
            //timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            //timer.Start();
            #endregion

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

        #region SiteMap Solution
        //private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    if (DateTime.Now.Hour == 0)
        //    {
        //        if (DateTime.Now.Subtract(lastTriggerDate).Days > 0)
        //        {
        //            lastTriggerDate = DateTime.Now;
        //            GenerateSitemapFile();
        //        }
        //    }
        //}

        //private void GenerateSitemapFile()
        //{
        //    var allFeaturedVehicles = _wc.GetAllFeatured(_token);
        //    var allCityPages = _wc.GetAllCityPage(_token);
        //    try
        //    {
        //        var path = Server.MapPath("~");
        //        var fileName = "DynamicSitemap.xml";

        //        var file = new FileInfo(@"" + path + fileName);
        //        if (file.Exists)
        //            file.Delete();

        //        var rootPath = System.Configuration.ConfigurationManager.AppSettings["WebSite"];
        //        var now = DateTime.Now.ToString("o");

        //        using (XmlTextWriter writer = new XmlTextWriter(@"" + path + fileName, System.Text.Encoding.UTF8))
        //        {
        //            writer.WriteStartDocument(true);
        //            writer.Formatting = Formatting.Indented;
        //            writer.Indentation = 2;
        //            writer.WriteStartElement("urlset");
        //            writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        //            writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        //            writer.WriteAttributeString("xmlns:xhtml", "http://www.w3.org/1999/xhtml");
        //            writer.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

        //            foreach (var item in allFeaturedVehicles)
        //            {
        //                var detailPath = "VehicleDetail/" + item;
        //                CreateNode(rootPath + detailPath, now, "daily", "0.7500", writer);
        //            }
        //            foreach (var item in allFeaturedVehicles)
        //            {
        //                var quotePath = "CheckOut/" + item;
        //                CreateNode(rootPath + quotePath, now, "daily", "0.7500", writer);
        //            }
        //            foreach (var item in allCityPages)
        //            {
        //                CreateNode(rootPath + item, now, "daily", "0.6000", writer);
        //            }
        //            writer.WriteEndElement();
        //            writer.WriteEndDocument();
        //            writer.Flush();
        //            writer.Close();
        //        }
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //}

        //private void CreateNode(string loc, string lastmod, string changefreq, string priority, XmlTextWriter writer)
        //{
        //    writer.WriteStartElement("url");
        //    writer.WriteStartElement("loc");
        //    writer.WriteString(loc);
        //    writer.WriteEndElement();
        //    writer.WriteStartElement("lastmod");
        //    writer.WriteString(lastmod);
        //    writer.WriteEndElement();
        //    writer.WriteStartElement("changefreq");
        //    writer.WriteString(changefreq);
        //    writer.WriteEndElement();
        //    writer.WriteStartElement("priority");
        //    writer.WriteString(priority);
        //    writer.WriteEndElement();
        //    writer.WriteEndElement();
        //}
        #endregion
    }
}