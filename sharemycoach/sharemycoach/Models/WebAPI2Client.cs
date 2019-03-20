using sharemycoach.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace sharemycoach.Models
{
    public class WebAPI2Client
    {
        private string _RMX_SERVICE_PATH;

        public WebAPI2Client()
        {
            _RMX_SERVICE_PATH = System.Configuration.ConfigurationManager.AppSettings["RMXWebService"];
        }

        public IEnumerable<WebCityStaticPageViewModel> GetAllWebCityStaticPages(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("webcitystaticpage", "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<IEnumerable<WebCityStaticPageViewModel>>().Result;
        }

        public IEnumerable<LocationModel> GetAllLocations(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("location", "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<IEnumerable<LocationModel>>().Result;
        }

        public CompanyModel GetCompany(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("company", "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<CompanyModel>().Result;
        }

        public IEnumerable<RandFeaturedViewModel> GetAllRandFeatureds(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("randfeatured", "?token=" + token);
            if (resp == null)
                return null;
                
            return resp.Content.ReadAsAsync<IEnumerable<RandFeaturedViewModel>>().Result;
        }

        public IEnumerable<RandFeaturedViewModel> GetAllRandFeatureds(long id, string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("randfeatured/", id.ToString() + "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<IEnumerable<RandFeaturedViewModel>>().Result;
        }

        public IEnumerable<FeaturedVehicleViewModel> GetAllFeaturedVehicles(long id, string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("featuredvehicle/", id.ToString() + "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<IEnumerable<FeaturedVehicleViewModel>>().Result;
        }

        public ServiceListViewModel GetAllServiceList(Guid id, string tag, string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("servicelist/", id.ToString() + "?tag=" + tag + "&token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<ServiceListViewModel>().Result;
        }

        public IEnumerable<CampgroundModel> GetAllCampgrounds(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("campground", "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<IEnumerable<CampgroundModel>>().Result;
        }

        public IEnumerable<SaleFeaturedViewModel> GetAllSaleFeatureds(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("salefeatured", "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<IEnumerable<SaleFeaturedViewModel>>().Result;
        }

        public VehicleSaleDetailViewModel GetSaleDetail(string id, string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("saledetail/", id + "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<VehicleSaleDetailViewModel>().Result;
        }

        public IEnumerable<EventFlyerViewModel> GetEventFlyers(string id, string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("eventflyer/", id + "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<IEnumerable<EventFlyerViewModel>>().Result;
        }

        public VehicleDetailViewModel GetDetail(string id, string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("detail/", id + "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<VehicleDetailViewModel>().Result;
        }

        public List<string> GetAllFeatured(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("allfeatured", "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<List<string>>().Result;
        }

        public List<string> GetAllCityPage(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("allcitypage", "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<List<string>>().Result;
        }

        public MatchVehicleViewModel GetMatchVehicle(int id, string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var resp = GetResponseFromWebAPI("matchvehicle/", id.ToString() + "?token=" + token);
            if (resp == null)
                return null;

            return resp.Content.ReadAsAsync<MatchVehicleViewModel>().Result;
        }

        private HttpResponseMessage GetResponseFromWebAPI(string method, string param)
        {
            try
            {
                var client = new HttpClient() { BaseAddress = new Uri(_RMX_SERVICE_PATH) };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(method + param).Result;
                if (response.IsSuccessStatusCode)
                    return response;

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}