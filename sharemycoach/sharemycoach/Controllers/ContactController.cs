using CaptchaMvc.HtmlHelpers;
using sharemycoach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class ContactController : BaseController
    {
        public ContactController()
        {
            ViewBag.Title = "RV Contact Us";

            var locationInfos = _locationInfos;
            var list = new List<MapModel>();
            foreach (var item in locationInfos)
            {
                if(item.IsLocationBehindOnPaying == null || item.IsLocationBehindOnPaying == false)
                {
                    var record = new MapModel()
                    {
                        LocationOid = item.Oid,
                        Latitude = item.Latitude ?? 0,
                        Longitude = item.Longitude ?? 0,
                        Address = item.Address,
                        City = item.City,
                        State = item.State,
                        PrimaryPhone = item.PrimaryPhone,
                        OrganizationName = item.OrganizationName,
                        Zip = item.Zip,
                    };
                    list.Add(record);
                }
            }
            ViewBag.ContactInfos = list;
            ViewBag.AllLocationInfos = locationInfos;
        }

        public ActionResult Index(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                ViewBag.SearchLocationOid = null;
                return View();
            }

            var locationInfo = GetTargetLocationInfo(location);
            if (locationInfo == null)
                return RedirectToAction("Index", "Error");

            ViewBag.SearchLocationOid = locationInfo.Oid;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index()
        {
            var contactName = string.Format("{0}", Request.Form["contact_name"]);
            var contactEmail = string.Format("{0}", Request.Form["contact_email"]);
            var contactPhone = string.Format("{0}", Request.Form["contact_phone"]);
            var contactSubject = string.Format("{0}", Request.Form["contact_subject"]);
            var contactComment = string.Format("{0}", Request.Form["contact_comment"]);
            var location = string.Format("{0}", Request.Form["location"]);

            if (ModelState.IsValid && this.IsCaptchaValid("Captcha is not valid"))
            {
                var contactModel = new ContactModel()
                {
                    Name = contactName,
                    Email = contactEmail,
                    Phone = contactPhone,
                    Subject = contactSubject,
                    Comment = contactComment,
                    Location = Guid.Parse(location),
                };

                var client = new HttpClient();
                var endPoint = System.Configuration.ConfigurationManager.AppSettings["RMXWebService"] + "contact";
                var response = await client.PostAsJsonAsync(endPoint, contactModel);
                if (response.IsSuccessStatusCode)
                {
                    var selectedLocationInfos = _locationInfos.FirstOrDefault(x => x.Oid == Guid.Parse(location));
                    ViewBag.SelectedLocationInfos = selectedLocationInfos;
                    return View("Success");
                }

                return View("Index");
            }

            ViewBag.ErrMessage = "Error: Captcha is not valid.";
            ViewBag.ContactName = contactName;
            ViewBag.ContactEmail = contactEmail;
            ViewBag.ContactPhone = contactPhone;
            ViewBag.ContactSubject = contactSubject;
            ViewBag.ContactComment = contactComment;
            return View("Index");
        }
    }
}