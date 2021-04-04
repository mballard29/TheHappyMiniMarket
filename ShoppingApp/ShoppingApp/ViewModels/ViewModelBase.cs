using MvvmHelpers;
using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        public ObservableRangeCollection<Product> Cart { get; set; }
        public ObservableRangeCollection<Product> Inventory { get; set; }

        public ViewModelBase()
        {
            Cart = new ObservableRangeCollection<Product>();
            Inventory = new ObservableRangeCollection<Product>();
        }
    }
}
