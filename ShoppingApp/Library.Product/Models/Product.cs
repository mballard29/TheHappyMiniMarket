using MvvmHelpers;
using System;

namespace Library.Models
{
    public class Product : ObservableObject
    {
        public Guid Id { get; set; }
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

        public decimal Price
        {
            get => Decimal.Round(Units * UnitPrice, 2);
        }

        //public Product()
        //{
        //    if (Id != Guid.Empty)
        //        Id = Guid.NewGuid();
        //}

        public Product()
        {

        }

        public Product(Product product)
        {
            Id = new Guid($"{product.Id}");
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

        public string ShoppingCartPrice
        {
            get => $"${string.Format("{0:0.00}", Price)}";
        }

        public string InventoryPrice
        {
            get => $"${string.Format("{0:0.00}", UnitPrice)} ea.";
        }
    }
}