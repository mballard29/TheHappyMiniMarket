using MvvmHelpers;
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
    public class InventoryViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Product> InventoryPage { get; set; }
        
        public AsyncCommand ShoppingCartCommand { get; }
        public AsyncCommand RefreshCommand { get; }
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
            AddCommand = new AsyncCommand<Product>(Add);
            PreviousCommand = new Command(Previous);
            NextCommand = new Command(Next);
        }

        async Task GoToShoppingCart()
        {
            await Shell.Current.GoToAsync("//ShoppingCartPage");
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);

            ReloadPage();

            IsBusy = false;
        }

        async Task Add(Product product)
        {
            if (product == null)
                return;
            var reqVal = await Application.Current.MainPage.DisplayPromptAsync(product.Name, "How many would you like?", "SAVE", "CANCEL", keyboard: Keyboard.Numeric);
            if (int.TryParse(reqVal, out int val))
            {
                if (val > product.Units || val < 0)
                    return;
                if (Cart.Any(i => i.Id.Equals(product.Id)))
                {
                    Cart.FirstOrDefault(i => i.Id.Equals(product.Id)).Units += val;
                    product.Units -= val;
                    if (product.Units == 0)
                        Inventory.Remove(product);
                    await Application.Current.MainPage.DisplayAlert("Added More to Cart", product.Name, "OK");
                    OnPropertyChanged("Cart");
                    OnPropertyChanged("Inventory");
                }
                else
                {
                    Cart.Add(new Product(product));
                    Cart[Cart.Count - 1].Units = val;
                    product.Units = product.Units - val;
                    if (product.Units == 0)
                        Inventory.Remove(product);
                    await Application.Current.MainPage.DisplayAlert("Added to Cart", product.Name, "OK");
                    OnPropertyChanged("Cart");
                    OnPropertyChanged("Inventory");
                }
            }
        }

        private void ReloadPage()
        {
            InventoryPage.Clear();
            for (int i = 5 * Page; i <= (5 * Page) + 4; i++)
            {
                if (i < Inventory.Count)
                    InventoryPage.Add(new Product(Inventory[i]));
            }
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
