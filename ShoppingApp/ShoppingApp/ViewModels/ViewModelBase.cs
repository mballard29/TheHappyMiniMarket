using MvvmHelpers;
using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShoppingApp.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        public ObservableRangeCollection<Product> Cart { get; set; }
        public ObservableRangeCollection<Product> Inventory { get; set; }

        public ViewModelBase()
        {
            Cart = new ObservableRangeCollection<Product>
            {
                new Product { Name = "Bread", Description = "Sunbeam Giant Enriched Bread", Units = 10, UnitPrice = 2.97m },
                new Product { Name = "Creamer", Description = "International Delight Caramel Macchiato Creamer, 32 oz.", Units = 10, UnitPrice = 2.77m },
                new Product { Name = "Juice", Description = "AriZona Kiwi Mucho Mango Juie Cocktail, 128 fl. oz.", Units = 10, UnitPrice = 2.96m },
                new Product { Name = "Pasta", Description = "Barilla Classic Blue Box Spaghetti Pasta, 16 oz.", Units = 10, UnitPrice = 1.28m },
                new Product { Name = "Marinara Sauce", Description = "Prego Pasta Sauce, Traditional Italian Tomato Sauce", Units = 10, UnitPrice = 1.88m },
                new Product { Name = "Popcorn", Description = "Orville Redenbacher's Original Gourmet Yellow Popcorn Kernels", Units = 10, UnitPrice = 4.98m },
                new Product { Name = "Potato Chips", Description = "Cape Code Potato Chips, Original Kettle Cooked Chips, 14 oz.", Units = 10, UnitPrice = 3.98m }
            };
            Inventory = new ObservableRangeCollection<Product>
            {
                new Product { Name = "Bread", Description = "Sunbeam Giant Enriched Bread", Units = 10, UnitPrice = 2.97m },
                new Product { Name = "Creamer", Description = "International Delight Caramel Macchiato Creamer, 32 oz.", Units = 10, UnitPrice = 2.77m },
                new Product { Name = "Juice", Description = "AriZona Kiwi Mucho Mango Juie Cocktail, 128 fl. oz.", Units = 10, UnitPrice = 2.96m },
                new Product { Name = "Pasta", Description = "Barilla Classic Blue Box Spaghetti Pasta, 16 oz.", Units = 10, UnitPrice = 1.28m },
                new Product { Name = "Marinara Sauce", Description = "Prego Pasta Sauce, Traditional Italian Tomato Sauce", Units = 10, UnitPrice = 1.88m },
                new Product { Name = "Popcorn", Description = "Orville Redenbacher's Original Gourmet Yellow Popcorn Kernels", Units = 10, UnitPrice = 4.98m },
                new Product { Name = "Potato Chips", Description = "Cape Code Potato Chips, Original Kettle Cooked Chips, 14 oz.", Units = 10, UnitPrice = 3.98m }
            };
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
