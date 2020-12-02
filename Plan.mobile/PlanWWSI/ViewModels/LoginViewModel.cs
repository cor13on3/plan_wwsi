using PlanWWSI.Views;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlanWWSI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Wybór grupy";
            ZapiszCommand = new Command(ExecuteZapiszCommand);
        }

        public ICommand ZapiszCommand { get; }
        public string NumerGrupy { get; set; }
        public INavigation Navigation { get; set; }

        private async void ExecuteZapiszCommand()
        {
            if (!Application.Current.Properties.ContainsKey("grupa"))
                Application.Current.Properties.Add("grupa", NumerGrupy);
            else
                Application.Current.Properties["grupa"] = NumerGrupy;
            await Application.Current.SavePropertiesAsync();
            Application.Current.MainPage = new MainPage();
        }
    }
}