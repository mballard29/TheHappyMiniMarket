﻿using MvvmHelpers;
using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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
                Cart = new ObservableRangeCollection<Product>
                {
                    new Product { Name = "Marinara Sauce", Description = "Prego Pasta Sauce, Traditional Italian Tomato Sauce", Units = 10, UnitPrice = 1.88m },
                    new Product { Name = "Popcorn", Description = "Orville Redenbacher's Original Gourmet Yellow Popcorn Kernels", Units = 10, UnitPrice = 4.98m },
                    new Product { Name = "Potato Chips", Description = "Cape Code Potato Chips, Original Kettle Cooked Chips, 14 oz.", Units = 10, UnitPrice = 3.98m },
                    new Product { Name = "Pretzel Crisps", Description = "Snack Factory Pretzel Crisps - 3 oz.", Units = 10, UnitPrice = 1.33m },
                };
            }
            if(Inventory == null)
            {
                Inventory = new ObservableRangeCollection<Product>
                {
                    new Product { Name = "Apple", Description = "Gala Apple", Units = 10, UnitPrice = 1.18m },
                    new Product { Name = "Bell Pepper", Description = "Green Bell Pepper", Units = 10, UnitPrice = 0.74m },
                    new Product { Name = "Cantaloupe", Description = "Cantaloupe", Units = 10, UnitPrice = 2.38m },
                    new Product { Name = "Clemetine", Description = "Clementine", Units = 10, UnitPrice = 1.44m },
                    new Product { Name = "Coffee", Description = "Folgers Classic Roast Ground Coffee, Medium Roast", Units = 10, UnitPrice = 0.22m },
                    new Product { Name = "Onions", Description = "Yellow Onions", Units = 10, UnitPrice = 0.7m },
                    new Product { Name = "Pineapple", Description = "Pineapple", Units = 10, UnitPrice = 2.27m },
                    new Product { Name = "Tomatoes", Description = "Roma Tomatoes", Units = 10, UnitPrice = 0.29m },
                    new Product { Name = "Trail Mix", Description = "GourmetNut Power Up Mega Omega Trail Mix", Units = 10, UnitPrice = 0.36m },
                    new Product { Name = "Lemons", Description = "Lemons", Units = 10, UnitPrice = 0.54m }
                };
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
