using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using PlanWWSI.Models;
using PlanWWSI.Services;
using System.Linq;

namespace PlanWWSI.ViewModels
{
    public class ZajeciaViewModel : BaseViewModel
    {
        public ObservableCollection<LekcjaWidok> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command PobierzZjazdyCommand { get; set; }
        private HTTP _httpClient;
        public INavigation Navigation { get; set; }
        private ZjazdWidokDTO[] _zjazdy;

        public string NumerGrupy { get; set; }

        private DateTime _data = DateTime.Today;
        public DateTime Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged(nameof(Data)); }
        }

        private string _zjazdInfo = null;
        public string ZjazdInfo
        {
            get { return _zjazdInfo; }
            set { _zjazdInfo = value; OnPropertyChanged(nameof(ZjazdInfo)); }
        }

        public ZajeciaViewModel()
        {
            Title = "Zajęcia";
            Items = new ObservableCollection<LekcjaWidok>();
            LoadItemsCommand = new Command(async (object p) => await ExecuteLoadItemsCommand((ContentPage)p));
            PobierzZjazdyCommand = new Command(async (object p) => await ExecutePobierzZjazdyCommand((ContentPage)p));
            _httpClient = new HTTP();
            if (string.IsNullOrEmpty(NumerGrupy))
                NumerGrupy = Application.Current.Properties["grupa"].ToString();
        }

        async Task ExecuteLoadItemsCommand(ContentPage page)
        {
            try
            {
                Items.Clear();
                var lista = await _httpClient.GetAsync<LekcjaWidokDTO[]>($"/api/lekcja/daj-plan/{Data:yyyy-MM-dd}/{NumerGrupy}");
                if (lista == null)
                    return;
                foreach (var item in lista)
                {
                    Items.Add(new LekcjaWidok
                    {
                        IdLekcji = item.IdLekcji,
                        Nazwa = item.Nazwa,
                        Godziny = $"{item.Od} - {item.Do}",
                        Sala = item.Sala,
                        Wykladowca = item.Wykladowca,
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

        async Task ExecutePobierzZjazdyCommand(ContentPage page)
        {
            try
            {
                _zjazdy = await _httpClient.GetAsync<ZjazdWidokDTO[]>($"/api/kalendarium/{NumerGrupy}");
                if (_zjazdy == null)
                    return;
                UstawZjazdInfo();
            }
            catch (Exception)
            {
                await page.DisplayAlert("Wystąpił błąd", "Spróbuj ponownie", "OK");
            }
        }

        private void UstawZjazdInfo()
        {
            var aktualnyZjazd = _zjazdy.FirstOrDefault(x => x.DataOd <= Data && Data <= x.DataDo);
            if (aktualnyZjazd == null)
                return;
            string value = $"Zjazd {aktualnyZjazd.Nr}.";
            if (aktualnyZjazd.CzyOdpracowanie)
                value += " (odpracowanie)";
            ZjazdInfo = value;
        }

        public void UstawPoprzedniDzien()
        {
            var poprzedniDzien = Data.AddDays(-1);
            if (poprzedniDzien.DayOfWeek == DayOfWeek.Thursday)
                poprzedniDzien = poprzedniDzien.AddDays(-1);
            var aktualnyZjazd = _zjazdy.FirstOrDefault(x => x.DataOd <= Data && Data <= x.DataDo);
            if (aktualnyZjazd != null && poprzedniDzien >= aktualnyZjazd.DataOd)
                Data = poprzedniDzien;
            else
            {
                var poprzedniZjazd = _zjazdy.OrderByDescending(x => x.DataOd).FirstOrDefault(x => x.DataDo < Data);
                if (poprzedniZjazd != null)
                    Data = poprzedniZjazd.DataDo;
                UstawZjazdInfo();
            }
        }

        public void UstawKolejnyDzien()
        {
            var kolejnyDzien = Data.AddDays(1);
            if (kolejnyDzien.DayOfWeek == DayOfWeek.Thursday)
                kolejnyDzien = kolejnyDzien.AddDays(1);
            var aktualnyZjazd = _zjazdy.FirstOrDefault(x => x.DataOd <= Data && Data <= x.DataDo);
            if (aktualnyZjazd != null && kolejnyDzien <= aktualnyZjazd.DataDo)
                Data = kolejnyDzien;
            else
            {
                var kolejnyZjazd = _zjazdy.OrderBy(x => x.DataOd).FirstOrDefault(x => x.DataOd > Data);
                if (kolejnyZjazd != null)
                    Data = kolejnyZjazd.DataOd;
                UstawZjazdInfo();
            }
        }
    }
}