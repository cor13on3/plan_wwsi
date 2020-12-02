using PlanWWSI.Models;
using PlanWWSI.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlanWWSI.ViewModels
{
    public class PlanWykladowcyViewModel : BaseViewModel
    {
        public WykladowcaWidok Wykladowca { get; set; }
        public ObservableCollection<LekcjaWidok> Items { get; }
        public Command LoadItemsCommand { get; }

        private HTTP _httpClient;

        private DateTime _data = DateTime.Today;
        public DateTime Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged(nameof(Data)); }
        }

        public PlanWykladowcyViewModel(WykladowcaWidok wykladowca)
        {
            Wykladowca = wykladowca;
            Title = Wykladowca.Nazwa;
            Items = new ObservableCollection<LekcjaWidok>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            _httpClient = new HTTP();
        }

        async Task ExecuteLoadItemsCommand()
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
                        Forma = item.Forma
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
