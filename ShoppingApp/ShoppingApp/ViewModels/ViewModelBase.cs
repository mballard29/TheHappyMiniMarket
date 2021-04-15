using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.IO;
using Xamarin.Forms;
using Library.Models;

using System.Diagnostics;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;

namespace ShoppingApp.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        public static ObservableRangeCollection<Product> Cart { get; set; }
        public static ObservableRangeCollection<Product> Inventory { get; set; }

        protected static decimal subtotal;
        public virtual string Subtotal { get; }

        public ViewModelBase()
        {
            if (Cart == null)
            {
                Cart = new ObservableRangeCollection<Product>();
                CartInit();
            }
            if (Inventory == null)
            {
                Inventory = new ObservableRangeCollection<Product>();
                InventoryInit();
            }
        }

        public async void InventoryInit()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync("http://192.168.56.1/ShoppingAppAPI/inventory/").ConfigureAwait(false);
                var inventory = JsonConvert.DeserializeObject<IEnumerable<Product>>(response);
                Inventory.AddRange(inventory);
            }
            OnPropertyChanged(nameof(Inventory));
        }

        public async void CartInit()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync("http://192.168.56.1/ShoppingAppAPI/cart/").ConfigureAwait(false);
                var cart = JsonConvert.DeserializeObject<IEnumerable<Product>>(response);
                Cart.AddRange(cart);
            }
            OnPropertyChanged(nameof(Inventory));
        }

        public virtual void ReloadPage()
        {

        }

        Product selectedProduct;
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert(value.Name, $"Descripton:\n{ value.Description }", "OK");
                    value = null;
                }

                selectedProduct = value;
                OnPropertyChanged();
            }
        }
    }
}
