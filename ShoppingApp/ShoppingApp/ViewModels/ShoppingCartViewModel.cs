using MvvmHelpers;
using MvvmHelpers.Commands;
using ShoppingApp.Models;
using System;
using System.Collections.Generic;
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
                    CartPage.Add(Cart[i]);
            }

            InventoryCommand = new AsyncCommand(GoToInventory);
            RefreshCommand = new AsyncCommand(Refresh);
            PreviousCommand = new Command(Previous);
            NextCommand = new Command(Next);
        }        

        async Task GoToInventory()
        {
            await Shell.Current.GoToAsync("//InventoryPage");
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);

            ReloadPage();

            IsBusy = false;
        }

        private void ReloadPage()
        {
            CartPage.Clear();
            for (int i = 5 * Page; i <= (5 * Page) + 4; i++)
            {
                if (i < Cart.Count)
                    CartPage.Add(Cart[i]);
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
            if (Page == ((int)Math.Ceiling(Cart.Count / 5m) - 1))
                return;

            Page++;
            ReloadPage();
        }
    }
}
