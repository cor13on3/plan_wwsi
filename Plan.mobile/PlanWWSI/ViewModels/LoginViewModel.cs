using App1.Views;
using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace App1.ViewModels
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
            var vm = new ZajeciaViewModel();
            vm.NumerGrupy = NumerGrupy;
            await Navigation.PushAsync(new PlanZajecPage(vm), true);
        }
    }
}