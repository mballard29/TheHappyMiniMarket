using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

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
                if (value >= 0)
                {
                    units = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Price");
                }
            }
        }
        private decimal unitPrice;
        public decimal UnitPrice
        {
            get => unitPrice;
            set
            {
                if (value >= 0)
                {
                    unitPrice = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Price");
                }
            }
        }
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
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Units = product.Units;
            UnitPrice = product.UnitPrice;
        }

        public string ShoppingCartUnits
        {
            get => $"{Units} units";
        }

        public string InventoryUnits
        {
            get => $"{Units} in stock";
        }

        public string InventoryPrice
        {
            get => $"${string.Format("{0:0.00}", UnitPrice)} ea.";
        }

        public string ShoppingCartPrice
        {
            get => $"${string.Format("{0:0.00}", UnitPrice)}";
        }
    }
}
