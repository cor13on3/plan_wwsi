using PlanWWSI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlanWWSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("grupa"))
            {
                var zajecia = new ZajeciaViewModel();
                zajecia.NumerGrupy = Application.Current.Properties["grupa"].ToString();
                Navigation.PushAsync(new PlanZajecPage(zajecia), true);
            }
            else
            {
                var vm = new LoginViewModel();
                vm.Navigation = this.Navigation;
                this.BindingContext = vm;
            }
        }
    }
}