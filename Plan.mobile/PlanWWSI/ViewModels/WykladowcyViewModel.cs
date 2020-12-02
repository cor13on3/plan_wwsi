using PlanWWSI.Models;
using PlanWWSI.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlanWWSI.ViewModels
{


    public class WykladowcyViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }
        public ObservableCollection<WykladowcaWidok> Items { get; set; }
        private HTTP _http;

        public WykladowcyViewModel()
        {
            Title = "Wykładowcy";
            Items = new ObservableCollection<WykladowcaWidok>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            _http = new HTTP();
        }

        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                Items.Clear();
                var lista = await _http.GetAsync<WykladowcaWidok[]>($"/api/wykladowca");
                foreach (var item in lista)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
    }
}