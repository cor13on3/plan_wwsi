using PlanWWSI.Views;
using System;
using System.Collections.Generic;
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
            Application.Current.Properties.Add("grupa", NumerGrupy);
            var vm = new ZajeciaViewModel();
            vm.NumerGrupy = NumerGrupy;
            await Navigation.PushAsync(new PlanZajecPage(vm), true);
        }
    }
}