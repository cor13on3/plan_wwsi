using PlanWWSI.Models;
using PlanWWSI.Services;
using PlanWWSI.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlanWWSI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private HTTP _http;

        public LoginViewModel()
        {
            Title = "Wybór grupy";
            ZapiszCommand = new Command(async (object p) => await ExecuteZapiszCommand((ContentPage)p));
        }

        public ICommand ZapiszCommand { get; }
        public string NumerGrupy { get; set; }
        public INavigation Navigation { get; set; }

        async Task ExecuteZapiszCommand(ContentPage page)
        {
            try
            {
                _http = new HTTP();
                var grupa = await _http.GetAsync<GrupaDTO>($"/api/grupa/{NumerGrupy}");
                if (grupa == null)
                {
                    await page.DisplayAlert("Nie ma takiej grupy :(", "Spróbuj ponownie", "OK");
                    return;
                }
                if (!Application.Current.Properties.ContainsKey("grupa"))
                    Application.Current.Properties.Add("grupa", NumerGrupy);
                else
                    Application.Current.Properties["grupa"] = NumerGrupy;
                await Application.Current.SavePropertiesAsync();
            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Wystąpił błąd", "Spróbuj ponownie", "OK");
            }
            Application.Current.MainPage = new MainPage();
        }
    }
}