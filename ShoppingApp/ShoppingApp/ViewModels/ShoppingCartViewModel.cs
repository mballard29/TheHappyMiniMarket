﻿using MvvmHelpers;
using MvvmHelpers.Commands;
using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace ShoppingApp.ViewModels
{
    public class ShoppingCartViewModel : ViewModelBase
    {        
        public ObservableRangeCollection<Product> CartPage { get; set; }

        public AsyncCommand InventoryCommand { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Product> DeleteCommand { get; }
        public Command PreviousCommand { get; }
        public Command NextCommand { get; }

        int Page;

        public ShoppingCartViewModel()
        {
            CartPage = new ObservableRangeCollection<Product>();
            Page = 0;

            for (int i = 5 * Page; i <= (5 * Page) + 4; i++)
            {
                if (i < Cart.Count)
                    CartPage.Add(new Product(Cart[i]));
            }

            InventoryCommand = new AsyncCommand(GoToInventory);
            RefreshCommand = new AsyncCommand(Refresh);
            DeleteCommand = new AsyncCommand<Product>(Delete);
            PreviousCommand = new Command(Previous);
            NextCommand = new Command(Next);
        }

        public override string Subtotal
        {
            get
            {
                decimal sub = 0m;
                foreach (Product x in Cart)
                    sub += x.Price;
                subtotal = sub;
                return $"Cart Subtotal: {sub}";

            }
        }

        async Task GoToInventory()
        {
            await Shell.Current.GoToAsync("//InventoryPage");
            ReloadPage();
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);

            ReloadPage();

            IsBusy = false;
        }

        async Task Delete(Product product)
        {
            Cart.Remove(Cart.FirstOrDefault(x => x.Id.Equals(product.Id)));
            ReloadPage();
            OnPropertyChanged(nameof(Cart));
            OnPropertyChanged(nameof(Inventory));
            await Application.Current.MainPage.DisplayAlert("Deleted", product.Name, "OK");
        }

        public override void ReloadPage()
        {
            CartPage.Clear();
            for (int i = 5 * Page; i <= (5 * Page) + 4; i++)
            {
                if (i < Cart.Count)
                    CartPage.Add(new Product(Cart[i]));
            }
            OnPropertyChanged(nameof(Subtotal));
        }

        private void Previous()
        {
            if (Page == 0)
                return;

            Page--;
            ReloadPage();
        }

        private void Next()
        {
            if (Page == ((int)Math.Ceiling(Cart.Count / 5m) - 1))
                return;

            Page++;
            ReloadPage();
        }
    }
}
