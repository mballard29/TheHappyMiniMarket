using Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ShoppingApp.Services
{
    class ShoppingAppService
    {
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000/ShoppingApp" : "http://localhost:5000/ShoppingApp";
        public static string InventoryUrl = $"{BaseAddress}/inventory/";

        static HttpClient client;

        static ShoppingAppService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        public static Task<IEnumerable<Product>> GetInventory() =>
            GetAsync<IEnumerable<Product>>("inventory", "getinventory");

        static async Task<T> GetAsync<T>(string url, string key, int mins = 1, bool forceRefresh = false)
        {
            var json = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }

        //static Random random = new Random();
        //public static async Task AddCoffee(string name, string roaster)
        //{
        //    var image = "https://www.yesplz.coffee/app/uploads/2020/11/emptybag-min.png";
        //    var coffee = new Coffee
        //    {
        //        Name = name,
        //        Roaster = roaster,
        //        Image = image,
        //        Id = random.Next(0, 10000)
        //    };

        //    var json = JsonConvert.SerializeObject(coffee);
        //    var content =
        //        new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await client.PostAsync("api/Coffee", content);

        //    if (!response.IsSuccessStatusCode)
        //    {

        //    }
        //}

        //public static async Task RemoveCoffee(int id)
        //{
        //    var response = await client.DeleteAsync($"api/Coffee/{id}");
        //    if (!response.IsSuccessStatusCode)
        //    {

        //    }
        //}
    }
}