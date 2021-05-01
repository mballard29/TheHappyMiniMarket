using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Library.Models;
using System.Net.Http;
using ShoppingApp.Services;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace ShoppingApp.ViewModels
{
    class ReceiptViewModel : ViewModelBase
    {
        public string ReceiptString { get; set; }
        public Command ExitCommand { get; }

        public ReceiptViewModel()
        {
            ReceiptInit();

            ExitCommand = new Command(Exit);
        }

        public async void ReceiptInit()
        {
            using (HttpClient client = new())
            {
                var response = await client.GetStringAsync("http://192.168.56.1/ShoppingAppAPI/cart/receipt").ConfigureAwait(false);
                ReceiptString = response;
                OnPropertyChanged(nameof(ReceiptString));
            }
        }

        public void Exit()
        {
            string filename = "receipt-";
            filename += DateTime.Today.ToString("d");
            filename += ".txt";
            filename = filename.Replace("/", "-");
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), filename), ReceiptString);
            Application.Current.Quit();
        }
    }
}
