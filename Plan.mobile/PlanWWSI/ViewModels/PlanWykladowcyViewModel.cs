using PlanWWSI.Models;
using PlanWWSI.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlanWWSI.ViewModels
{
    public class PlanWykladowcyViewModel : BaseViewModel
    {
        public WykladowcaWidok Wykladowca { get; set; }
        public ObservableCollection<LekcjaWidok> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command LoadItemDetailsCommand { get; }

        private HTTP _httpClient;

        private DateTime _data = DateTime.Today;
        public DateTime Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged(nameof(Data)); }
        }

        private WykladowcaModel _szczegoly;
        public WykladowcaModel Szczegoly
        {
            get { return _szczegoly; }
            set { _szczegoly = value; OnPropertyChanged(nameof(Szczegoly)); }
        }


        public PlanWykladowcyViewModel(WykladowcaWidok wykladowca)
        {
            Wykladowca = wykladowca;
            Title = Wykladowca.Nazwa;
            Items = new ObservableCollection<LekcjaWidok>();
            LoadItemsCommand = new Command(async (object p) => await ExecuteLoadItemsCommand((ContentPage)p));
            LoadItemDetailsCommand = new Command(async (object p) => await ExecuteLoadItemDetailsCommand((ContentPage)p));
            _httpClient = new HTTP();
        }

        async Task ExecuteLoadItemsCommand(ContentPage page)
        {
            try
            {
                Items.Clear();
                var lista = await _httpClient.GetAsync<LekcjaWidokDTO[]>($"/api/wykladowca/plan/{Wykladowca.Id}/{Data:yyyy-MM-dd}");
                foreach (var item in lista)
                {
                    Items.Add(new LekcjaWidok
                    {
                        IdLekcji = item.IdLekcji,
                        Nazwa = item.Nazwa,
                        Godziny = $"{item.Od} - {item.Do}",
                        Sala = item.Sala,
                        Forma = item.Forma,
                        JestSala = item.Sala != null
                    });
                }
            }
            catch (Exception)
            {
                await page.DisplayAlert("Wystąpił błąd", "Spróbuj ponownie", "OK");
            }
        }

        async Task ExecuteLoadItemDetailsCommand(ContentPage page)
        {
            try
            {
                var dto = await _httpClient.GetAsync<WykladowcaDTO>($"/api/wykladowca/{Wykladowca.Id}");
                Szczegoly = new WykladowcaModel(dto);
            }
            catch (Exception)
            {
                await page.DisplayAlert("Wystąpił błąd", "Spróbuj ponownie", "OK");
            }
        }
    }
}
