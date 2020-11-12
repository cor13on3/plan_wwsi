using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using App1.Models;
using App1.Views;
using System.Collections.Generic;
using App1.Services;

namespace App1.ViewModels
{
    public class ZajeciaViewModel : BaseViewModel
    {
        public ObservableCollection<Zajecia> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public string NumerGrupy { get; set; }
        private IDataStore<Zajecia> _dataStore;

        public ZajeciaViewModel()
        {
            Title = "Zajęcia";
            Items = new ObservableCollection<Zajecia>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            _dataStore = GetDataStore<Zajecia>();

            var mockItems = new List<Zajecia>
            {
                new Zajecia { Id = Guid.NewGuid().ToString(), Nazwa = "Programowanie", Godzina="8:00 - 9:35" },
                new Zajecia { Id = Guid.NewGuid().ToString(), Nazwa = "Grafika", Godzina="9:45 - 11:20" },
                new Zajecia { Id = Guid.NewGuid().ToString(), Nazwa = "Sieci", Godzina="11:30 - 13:05" },
                new Zajecia { Id = Guid.NewGuid().ToString(), Nazwa = "Algorytmy", Godzina="13:45 - 15:20" },
            };

            foreach (var item in mockItems)
            {
                _dataStore.AddItemAsync(item);
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await _dataStore.GetItemsAsync(null, true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}