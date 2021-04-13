using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.IO;
using Xamarin.Forms;
using Library.Models;

using System.Diagnostics;

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
            }
            if (Inventory == null)
            {
                Inventory = new ObservableRangeCollection<Product>();
            }
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
