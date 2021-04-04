using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingApp.ViewModels
{
    public class InventoryViewModel : ViewModelBase
    {
        public AsyncCommand ShoppingCartCommand { get; }

        public InventoryViewModel()
        {
            ShoppingCartCommand = new AsyncCommand(GoToShoppingCart);
        }

        async Task GoToShoppingCart()
        {
            await Shell.Current.GoToAsync("//ShoppingCartPage");
        }
    }
}
