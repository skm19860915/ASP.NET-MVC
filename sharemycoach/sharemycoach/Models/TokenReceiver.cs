using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace sharemycoach.Models
{
    public class TokenReceiver
    {
        private string _TOKEN_SERVICE_PATH;

        public TokenReceiver()
        {
            _TOKEN_SERVICE_PATH = "http://localhost:2124/api/";
        }

        public string GetRMXToken()
        {
            //var resp = GetResponseFromTokenService("values");
            //if (resp == null)
            //    return null;

            //var token = resp.Content.ReadAsAsync<string>().Result;

            var token = Properties.Resources.RMX_WEBAPI_TOKEN_STRING;
            return token;
        }

        private HttpResponseMessage GetResponseFromTokenService(string method)
        {
            try
            {
                var client = new HttpClient() { BaseAddress = new Uri(_TOKEN_SERVICE_PATH) };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = client.GetAsync(method).Result;
                if (message.IsSuccessStatusCode)
                    return message;

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}