using Library.Models;
using Newtonsoft.Json;
using ShoppingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryPage : ContentPage
    {
        public InventoryPage()
        {
            InitializeComponent();
            //BindingContext = new ViewModelBase();

            //var webhandler = new WebRequestHandler();
            //var ret = webhandler.Get($"http://10.0.2.2:5000/ShoppingApp/inventory").Result;
            //var api_inventory = JsonConvert.DeserializeObject<List<Product>>(ret);

            //if (ViewModelBase.Inventory != null)
            //    ViewModelBase.Inventory.AddRange(api_inventory);
            //else
            //{
            //    ViewModelBase.Inventory = new MvvmHelpers.ObservableRangeCollection<Product>();
            //    ViewModelBase.Inventory.AddRange(api_inventory);
            //}
        }
    }
}