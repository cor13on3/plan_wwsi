using App1.Models;
using App1.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class WykladowcyViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }

        private IDataStore<Wykladowca> _dataStore;
        private HttpClient _http;

        public ObservableCollection<Wykladowca> Items { get; set; }
        public WykladowcaSzczegoly Szczegoly { get; set; }

        public WykladowcyViewModel()
        {
            Title = "Wykładowcy";
            Items = new ObservableCollection<Wykladowca>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            _dataStore = GetDataStore<Wykladowca>();
            _http = new HttpClient();
            TestHttp();
            var items = new Wykladowca[]
            {
                new Wykladowca{ Id = "1", Imie = "Jan", Nazwisko = "Kowalski", NazwaPelna = "Jan Kowalski"},
                new Wykladowca{ Id = "2", Imie = "Rafael", Nazwisko = "Triskov", NazwaPelna = "Rafael Triskov"},
            };
            foreach (var item in items)
                _dataStore.AddItemAsync(item);
        }

        async void TestHttp()
        {
            var response = await _http.GetAsync(new Uri("https://jsonplaceholder.typicode.com/todos/1"));
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
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