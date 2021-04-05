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
    public class InventoryViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Product> InventoryPage { get; set; }
        
        public AsyncCommand ShoppingCartCommand { get; }
        public AsyncCommand RefreshCommand { get; }
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
                    InventoryPage.Add(Inventory[i]);
            }

            ShoppingCartCommand = new AsyncCommand(GoToShoppingCart);
            RefreshCommand = new AsyncCommand(Refresh);
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

        private void ReloadPage()
        {
            InventoryPage.Clear();
            for (int i = 5 * Page; i <= (5 * Page) + 4; i++)
            {
                if (i < Inventory.Count)
                    InventoryPage.Add(Inventory[i]);
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
