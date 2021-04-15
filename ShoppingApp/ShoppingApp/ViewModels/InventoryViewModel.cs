using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Library.Models;
using Command = MvvmHelpers.Commands.Command;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

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

        private HttpClient Client { get; }

        public InventoryViewModel()
        {
            Client = new HttpClient();
            InventoryPage = new ObservableRangeCollection<Product>();
            Page = 0;

            ReloadPage();

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
                return $"Cart Subtotal: ${string.Format("{0:0.00}", sub)}";
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
                var api_product = new Product { Id = product.Id, Name = product.Name, Description = product.Description, Units = val, UnitPrice = product.UnitPrice };
                var json = JsonConvert.SerializeObject(api_product);
                if (!Cart.Any(x => x.Id.Equals(product.Id)))                            //first time add, update inventory item, delete if need
                {
                    await Client.PostAsync("http://192.168.56.1/ShoppingAppAPI/cart/", new StringContent(json, Encoding.UTF8, "application/json"));
                    Cart.Add(new Product(product));                                         // POST
                    Cart.FirstOrDefault(x => x.Id.Equals(product.Id)).Units = val;          // PUT
                }
                else                                                                    //update cart item, update inventory item, delete if needed
                { 
                    Cart.FirstOrDefault(x => x.Id.Equals(product.Id)).Units += val;         // PUT
                }
                Inventory.FirstOrDefault(x => x.Id.Equals(product.Id)).Units -= val;
                if (Inventory.FirstOrDefault(x => x.Id.Equals(product.Id)).Units == 0)
                    Inventory.Remove(Inventory.FirstOrDefault(x => x.Id.Equals(product.Id)));   // DELETE
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
