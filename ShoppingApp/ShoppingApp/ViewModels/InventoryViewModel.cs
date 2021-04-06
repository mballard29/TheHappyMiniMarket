using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace ShoppingApp.ViewModels
{
    public class InventoryViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Product> InventoryPage { get; set; }
        
        public AsyncCommand ShoppingCartCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Product> DeleteCommand { get; }
        public AsyncCommand<Product> AddCommand { get; }
        public Command PreviousCommand { get; }
        public Command NextCommand { get; }

        int Page;

        public InventoryViewModel()
        {
            InventoryPage = new ObservableRangeCollection<Product>();
            Page = 0;

            for (int i = 5 * Page; i <= (5 * Page) + 4; i++)
            {
                if (i < Inventory.Count)
                    InventoryPage.Add(new Product(Inventory[i]));
            }

            ShoppingCartCommand = new AsyncCommand(GoToShoppingCart);
            RefreshCommand = new AsyncCommand(Refresh);
            DeleteCommand = new AsyncCommand<Product>(Delete);
            AddCommand = new AsyncCommand<Product>(Add);
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

        async Task GoToShoppingCart()
        {
            await Shell.Current.GoToAsync("//ShoppingCartPage");
            ReloadPage();
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(1000);

            ReloadPage();

            IsBusy = false;
        }

        async Task Delete(Product product)
        {
            Inventory.Remove(Inventory.FirstOrDefault(x => x.Id.Equals(product.Id)));
            ReloadPage();
            await Application.Current.MainPage.DisplayAlert("Deleted", product.Name, "OK");
        }

        async Task Add(Product product)
        {
            var req_val = await Application.Current.MainPage.DisplayPromptAsync(
                product.Name, "How many would you like?", "SAVE", "CANCEL", placeholder: $"{product.Units} in stock", keyboard: Keyboard.Numeric);
            if (int.TryParse(req_val, out int val))
            {
                if (val < 0 || val > product.Units)
                    return;
                if (!Cart.Any(x => x.Id.Equals(product.Id)))
                {
                    Cart.Add(new Product(product));
                    Cart.FirstOrDefault(x => x.Id.Equals(product.Id)).Units = val;
                }
                else
                {
                    Cart.FirstOrDefault(x => x.Id.Equals(product.Id)).Units += val;
                }
                Inventory.FirstOrDefault(x => x.Id.Equals(product.Id)).Units -= val;
                if (Inventory.FirstOrDefault(x => x.Id.Equals(product.Id)).Units == 0)
                    Inventory.Remove(Inventory.FirstOrDefault(x => x.Id.Equals(product.Id)));
                await Application.Current.MainPage.DisplayAlert($"Added {product.Name} to Cart", $"{val} units", "OK");
                ReloadPage();
            }
        }

        public override void ReloadPage()
        {
            InventoryPage.Clear();
            for (int i = 5 * Page; i <= (5 * Page) + 4; i++)
            {
                if (i < Inventory.Count)
                    InventoryPage.Add(new Product(Inventory[i]));
            }
            OnPropertyChanged(nameof(Cart));
            OnPropertyChanged(nameof(Inventory));
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
            if (Page == ((int)Math.Ceiling(Inventory.Count / 5m) - 1))
                return;

            Page++;
            ReloadPage();
        }
    }
}
