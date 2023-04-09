using sharemycoach.ViewModels;
using System.Net;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace sharemycoach.Controllers
{
    public class VehicleDetailController : BaseController
    {
        #region New Solution
        private DetailInfoViewModel detailInfo;

        public ActionResult Index(string id, bool? isDetailWithCalendar)
        {
            detailInfo = _wc.GetDetailInfo(id, false, _token);

            if (detailInfo == null)
            {
                detailInfo = _wc.GetDetailInfo(id, true, _token);
                if (detailInfo == null)
                    return RedirectToAction("Index", "Error");
            }

            ViewBag.Title = detailInfo.NameOnWeb + " - " + detailInfo.LocationName + " - RV Rentals | " + Properties.Resources.SHARE_MY_COACH;
            ViewBag.Info = detailInfo;
            ViewBag.IsDetailWithCalendar = isDetailWithCalendar;
            var urls = GetPhotoUrls(detailInfo);
            ViewBag.Urls = urls;

            return View();
        }

        private List<string> GetPhotoUrls(DetailInfoViewModel info)
        {
            var rootImageURL = System.Configuration.ConfigurationManager.AppSettings["RMXImageRootURL"];
            var urls = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                var url = string.Concat(rootImageURL, info.Location, '/', info.Oid, '/', info.WebUniqueId, "_", i, "_0.jpg");
                HttpWebResponse response = null;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    if (response != null)
                    {
                        urls.Add(url);
                    }
                }
                catch (WebException e)
                {
                }
                finally
                {
                    if (response != null)
                        response.Close();
                }
            }

            return urls;
        }
        #endregion

        #region Old Solution
        //private VehicleDetailViewModel detailInfo;

        //public ActionResult Index(string id, bool? isDetailWithCalendar)
        //{
        //    detailInfo = _wc.GetDetail(id, false, _token);

        //    if (detailInfo == null)
        //    {
        //        detailInfo = _wc.GetDetail(id, true, _token);
        //        if (detailInfo == null)
        //            return RedirectToAction("Index", "Error");
        //    }

        //    GetDetailInfo(detailInfo);
        //    ViewBag.IsDetailWithCalendar = isDetailWithCalendar;
        //    return View();
        //}

        //public void GetDetailInfo(VehicleDetailViewModel info)
        //{
        //    ViewBag.Title = info.NameOnWeb + " - " + info.DBAName + " - RV Rentals | " + Properties.Resources.SHARE_MY_COACH;
        //    TempData["Detail"] = info;
        //    TempData.Keep();
        //    ViewBag.Info = info;
        //}
        #endregion
    }
}