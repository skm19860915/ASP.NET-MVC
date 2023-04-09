using CaptchaMvc.HtmlHelpers;
using sharemycoach.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class TestimonialsController : BaseController
    {
        public TestimonialsController()
        {
            ViewBag.Title = "RV Testimonials";
            var locationInfos = _wc.GetAllLocations(_token);
            ViewBag.LocationInfos = locationInfos;
        }

        public ActionResult Index(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                ViewBag.SearchLocationOid = null;

                return View();
            }

            var locationInfo = GetTargetLocationInfo(location);
            if(locationInfo == null)
                return RedirectToAction("Index", "Error");

            ViewBag.SearchLocationOid = locationInfo.Oid;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(Guid location)
        {
            if(!this.IsCaptchaValid("Captcha is not valid"))
            {
                ViewBag.ErrMessage = "Error: captcha is not valid.";
                ViewBag.SuccessMessage = "";
                return View();
            }

            ViewBag.ErrMessage = "";
            var name = string.Format("{0}", Request.Form["name"]);
            var city = string.Format("{0}", Request.Form["city"]);
            var country = string.Format("{0}", Request.Form["country"]);
            var state = string.Format("{0}", Request.Form["state"]);
            var testimonial = string.Format("{0}", Request.Form["testimonial"]);
            var oid = location;

            var testimonialModel = new TestimonialModel()
            {
                Name = name,
                City = city,
                Country = country,
                State = state,
                Testimonial = testimonial,
                Location = oid,
                Photo = null
            };

            var client = new HttpClient();
            var endPoint = System.Configuration.ConfigurationManager.AppSettings["RMXWebService"] + "testimonial";
            var response = await client.PostAsJsonAsync(endPoint, testimonialModel);
            if (response.IsSuccessStatusCode)
                ViewBag.SuccessMessage = "Successful sent email";
            else
                ViewBag.SuccessMessage = "Send Failed email";

            return View();
        }
    }
}