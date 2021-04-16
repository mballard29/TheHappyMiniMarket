using Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Services
{
    public class CartService
    {
        static HttpClient Client { get; }

        static CartService()
        {
            Client = new HttpClient();
        }


        //CREATE
        public static async Task AddProduct(Product product, int units)
        {
            var api_product = new Product { Id = product.Id, Name = product.Name, Description = product.Description, Units = units, UnitPrice = product.UnitPrice };
            var json = JsonConvert.SerializeObject(api_product);
            await Client.PostAsync("http://192.168.56.1/ShoppingAppAPI/cart/", new StringContent(json, Encoding.UTF8, "application/json"));
        }

        //GET/{id}
        public static async Task<Product> GetProduct(Product product)
        {
            var response = await Client.GetStringAsync($"http://192.168.56.1/ShoppingAppAPI/cart/{product.Id}").ConfigureAwait(false);
            var ret = JsonConvert.DeserializeObject<Product>(response);
            return ret;
        }

        //UPDATE
        public static async Task UpdateProduct(Product product, int units)
        {
            var api_product = new Product { Id = product.Id, Name = product.Name, Description = product.Description, Units = units, UnitPrice = product.UnitPrice };
            var json = JsonConvert.SerializeObject(api_product);
            await Client.PutAsync($"http://192.168.56.1/ShoppingAppAPI/cart/{product.Id}", new StringContent(json, Encoding.UTF8, "application/json"));
        }



        //DELETE
        public static async Task DeleteProduct(Product product)
        {
            await Client.DeleteAsync($"http://192.168.56.1/ShoppingAppAPI/cart/{product.Id}");
        }

        public static async Task ClearProducts()
        {
            await Client.DeleteAsync($"http://192.168.56.1/ShoppingAppAPI/cart/clear");
        }
    }
}
