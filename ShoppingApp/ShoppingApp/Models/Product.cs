using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using ShoppingApp.Views;

namespace ShoppingApp.Models
{
    public class Product : ObservableObject
    {
        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private int units;
        public int Units
        {
            get => units;
            set
            {
                SetProperty(ref units, value);
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(ShoppingCartUnits));
                OnPropertyChanged(nameof(InventoryUnits));
            }
        }
        private decimal unitPrice;
        public decimal UnitPrice
        {
            get => unitPrice;
            set
            {
                if (value >= 0m)
                {
                    SetProperty(ref unitPrice, value);
                    OnPropertyChanged(nameof(Price));
                    OnPropertyChanged(nameof(ShoppingCartPrice));
                    OnPropertyChanged(nameof(InventoryPrice));
                }
            }
        }

        [JsonIgnore]
        public decimal Price
        {
            get => Units * UnitPrice;
        }

        public Product()
        {
            Id = Guid.NewGuid();
        }

        public Product(Product product)
        {
            Id = new Guid($"{product.Id}");
            Name = product.Name;
            Description = product.Description;
            Units = product.Units;
            UnitPrice = product.UnitPrice;
        }

        [JsonIgnore]
        public string ShoppingCartUnits
        {
            get => $"{Units} units";
        }

        [JsonIgnore]
        public string InventoryUnits
        {
            get => $"{Units} in stock";
        }

        [JsonIgnore]
        public string ShoppingCartPrice
        {
            get => $"${string.Format("{0:0.00}", Price)}";
        }

        [JsonIgnore]
        public string InventoryPrice
        {
            get => $"${string.Format("{0:0.00}", UnitPrice)} ea.";
        }
    }
}
