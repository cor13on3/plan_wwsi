using PlanWWSI.Models;
using PlanWWSI.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlanWWSI.ViewModels
{
    public class WykladowcyViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }
        public ObservableCollection<WykladowcaWidok> Items { get; set; }
        private HTTP _http;

        private string _fraza;
        public string Fraza
        {
            get { return _fraza; }
            set { _fraza = value; OnPropertyChanged(nameof(Fraza)); }
        }


        public WykladowcyViewModel()
        {
            Title = "Wykładowcy";
            Items = new ObservableCollection<WykladowcaWidok>();
            LoadItemsCommand = new Command(async (object p) => await ExecuteLoadItemsCommand((ContentPage)p));
            _http = new HTTP();
        }

        async Task ExecuteLoadItemsCommand(ContentPage page)
        {
            try
            {
                Items.Clear();
                var lista = await _http.GetAsync<WykladowcaWidok[]>($"/api/wykladowca?fraza={Fraza}");
                if (lista == null)
                    return;
                foreach (var item in lista)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Wystąpił błąd", "Spróbuj ponownie", "OK");
            }

        }
    }
}