using MvvmHelpers.Commands;
using Newtonsoft.Json;
using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace ShoppingApp.ViewModels
{
    class ReceiptViewModel : ViewModelBase
    {
        string localpath;
        public AsyncCommand SaveCommand { get; }

        public ReceiptViewModel()
        {
            SaveCommand = new AsyncCommand(Save);
            localpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public decimal Tax = Decimal.Round(subtotal * 0.07m, 2);

        public string ReceiptString
        {
            get
            {
                string receipt_text = $"Happy MiniMarket Receipt\n\n";
                foreach (Product x in Cart)
                {
                    receipt_text += $"{x.Name}, {x.Units} units * ${x.UnitPrice} = ${x.Price}\n";
                }
                receipt_text += $"\nSubtotal:                 ${subtotal}\n";
                receipt_text +=   $"Tax:            7.0%      ${Tax}\n";
                receipt_text += $"Total:                      ${Tax + subtotal}\n\n";
                receipt_text += "Shop with us again!\n";
                return receipt_text;
            }
        }

        async Task Save()
        {
            File.WriteAllText($"{localpath}/ReceiptFile.txt", ReceiptString);
            File.WriteAllText($"{localpath}/saveFile.txt", JsonConvert.SerializeObject(nameof(Inventory)));
            await Task.Delay(3000);
            Application.Current.Quit();
        }
    }
}
