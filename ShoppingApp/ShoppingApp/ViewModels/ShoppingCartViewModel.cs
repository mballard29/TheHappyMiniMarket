using MvvmHelpers;
using MvvmHelpers.Commands;
using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingApp.ViewModels
{
    public class ShoppingCartViewModel : ViewModelBase
    {        
        public AsyncCommand InventoryCommand { get; set; }
        
        public string ItemPrice;
        public string Subtotal;

        public ShoppingCartViewModel()
        {
            InventoryCommand = new AsyncCommand(GoToInventory);
        }

        async Task GoToInventory()
        {
            await Shell.Current.GoToAsync("//InventoryPage");
        }
    }
}
