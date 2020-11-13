using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using PlanWWSI.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace PlanWWSI.ViewModels
{
    public class ZajeciaViewModel : BaseViewModel
    {
        public ObservableCollection<Lekcja> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public string NumerGrupy { get; set; }
        private HttpClient _httpClient;
        public INavigation Navigation { get; set; }

        public ZajeciaViewModel()
        {
            Title = "Zajęcia";
            Items = new ObservableCollection<Lekcja>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            _httpClient = new HttpClient();
            if (String.IsNullOrEmpty(NumerGrupy))
                NumerGrupy = Application.Current.Properties["grupa"].ToString();
        }

        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                Items.Clear();
                var data = DateTime.Today.ToString("yyyy-MM-dd");
                var res = await _httpClient.GetAsync(new Uri($"http://10.0.2.2:60211/api/lekcja/daj-plan/{data}/{NumerGrupy}"));
                if (res.IsSuccessStatusCode)
                {
                    string content = await res.Content.ReadAsStringAsync();
                    var lista = JsonConvert.DeserializeObject<LekcjaWidokDTO[]>(content);
                    foreach (var item in lista)
                    {
                        Items.Add(new Lekcja
                        {
                            IdLekcji = item.IdLekcji,
                            Nazwa = item.Nazwa,
                            Godziny = $"{item.Od} - {item.Do}",
                            Sala = item.Sala,
                            Wykladowca = item.Wykladowca,
                            Forma = item.Forma
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}