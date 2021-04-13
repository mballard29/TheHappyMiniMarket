using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ShoppingApp
{
    class WebRequestHandler
    {
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000/ShoppingApp" : "http://localhost:5000/ShoppingApp";
        public static string InventoryUrl = $"{BaseAddress}/inventory/";

        // This method must be in a class in a platform project, even if
        // the HttpClient object is constructed in a shared project.
        public HttpClientHandler GetInsecureHandler()
        {           
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        private HttpClient Client { get; }

        public WebRequestHandler()
        {
            //#if DEBUG
                HttpClientHandler insecureHandler = GetInsecureHandler();
                    Client = new HttpClient(insecureHandler);
            //#else
            //    HttpClient client = new HttpClient();
            //#endif
        }

        public async Task<string> Get(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync(url).ConfigureAwait(false);
                    Debug.WriteLine($"Response is: {response}");
                    return response;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get info from Api: {e}");
            }

            return null;
        }
    }
}
