using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Library.Models;
using Command = MvvmHelpers.Commands.Command;

namespace ShoppingApp.ViewModels
{
    public class ShoppingCartViewModel : ViewModelBase
    {        
        public ObservableRangeCollection<Product> CartPage { get; set; }

        public AsyncCommand InventoryCommand { get; }
        public AsyncCommand CheckoutCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Product> DeleteCommand { get; }
        public AsyncCommand<Product> EditCommand { get; }
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
            CheckoutCommand = new AsyncCommand(Checkout);
            RefreshCommand = new AsyncCommand(Refresh);
            DeleteCommand = new AsyncCommand<Product>(Delete);
            EditCommand = new AsyncCommand<Product>(Edit);
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
                return $"Cart Subtotal: ${string.Format("{0:0.00}", sub)}";
            }
        }

        async Task GoToInventory()
        {
            await Shell.Current.GoToAsync("//InventoryPage");
            ReloadPage();
        }

        async Task Checkout()
        {
            ReloadPage();
            await Shell.Current.GoToAsync("//ReceiptPage");
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
            Cart.Remove(Cart.FirstOrDefault(x => x.Id.Equals(product.Id)));
            if (Inventory.Any(x => x.Id.Equals(product.Id)))
                Inventory.FirstOrDefault(x => x.Id.Equals(product.Id)).Units += product.Units;
            ReloadPage();
            await Application.Current.MainPage.DisplayAlert("Deleted", product.Name, "OK");
        }

        async Task Edit(Product product)
        {
            var req_val = await Application.Current.MainPage.DisplayPromptAsync(
                product.Name, "How many would you like?", "SAVE", "CANCEL", placeholder: $"You currently have {product.Units} units", keyboard: Keyboard.Numeric);
            if (int.TryParse(req_val, out int val))
            {
                if (val > Inventory.FirstOrDefault(x => x.Id.Equals(product.Id))?.Units || val < 0)
                    return;
                if (Inventory.Any(x => x.Id.Equals(product.Id)))
                {
                    Inventory.FirstOrDefault(x => x.Id.Equals(product.Id)).Units -= (val - product.Units);
                    Cart.FirstOrDefault(x => x.Id.Equals(product.Id)).Units = val;
                    if (Inventory.FirstOrDefault(x => x.Id.Equals(product.Id))?.Units == 0)
                        Inventory.Remove(Inventory.FirstOrDefault(x => x.Id.Equals(product.Id)));
                    if (Cart.FirstOrDefault(x => x.Id.Equals(product.Id))?.Units == 0)
                        Cart.Remove(Cart.FirstOrDefault(x => x.Id.Equals(product.Id)));
                    ReloadPage();
                }
                else
                {
                    if (val > product.Units)
                        return;
                    else
                    {
                        Inventory.Add(new Product(product));
                        Inventory.FirstOrDefault(x => x.Id.Equals(product.Id)).Units = (product.Units - val);
                        Cart.FirstOrDefault(x => x.Id.Equals(product.Id)).Units = val;
                        if (Cart.FirstOrDefault(x => x.Id.Equals(product.Id))?.Units == 0)
                            Cart.Remove(Cart.FirstOrDefault(x => x.Id.Equals(product.Id)));
                        ReloadPage();
                    }
                }
                await Application.Current.MainPage.DisplayAlert("Edited", val > product.Units ? $"+{val - product.Units} units of {product.Name}" : $"-{val - product.Units} units of {product.Name}", "OK");
            }
        }

        public override void ReloadPage()
        {
            CartPage.Clear();
            for (int i = 5 * Page; i <= (5 * Page) + 4; i++)
            {
                if (i < Cart.Count)
                    CartPage.Add(new Product(Cart[i]));
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
            if (Page == ((int)Math.Ceiling(Cart.Count / 5m) - 1))
                return;

            Page++;
            ReloadPage();
        }
    }
}
