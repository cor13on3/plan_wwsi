using PlanWWSI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlanWWSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel vm;

        public LoginPage()
        {
            InitializeComponent();
            vm = new LoginViewModel();
            this.BindingContext = vm;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            vm.ZapiszCommand.Execute(this);
        }
    }
}