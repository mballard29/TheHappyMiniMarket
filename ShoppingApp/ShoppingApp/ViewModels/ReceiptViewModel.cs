using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Library.Models;

namespace ShoppingApp.ViewModels
{
    class ReceiptViewModel : ViewModelBase
    {
        string localpath;
        public AsyncCommand SaveCommand { get; }

        public ReceiptViewModel()
        {
            SaveCommand = new AsyncCommand(Save);
            localpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
                receipt_text += $"\nSubtotal:                 ${string.Format("{0:0.00}", subtotal)}";
                receipt_text +=   $"Tax:            7.0%      ${string.Format("{0:0.00}", Decimal.Round(Tax, 2))}\n";
                receipt_text += $"Total:                      ${string.Format("{0:0.00}", subtotal + Tax)}\n\n";
                receipt_text += "Shop with us again!\n";
                return receipt_text;
            }
        }

        async Task Save()
        {
            File.WriteAllText(Path.Combine(localpath, "ReceiptFile.txt"), ReceiptString);
            string json = JsonConvert.SerializeObject(Inventory, Formatting.Indented);
            File.WriteAllText(Path.Combine(localpath, "saveFile.txt"), json);
            await Task.Delay(3000);
            // Application.Current.Quit();
        }
    }
}
