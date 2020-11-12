using System.ComponentModel;
using Xamarin.Forms;
using PlanWWSI.ViewModels;

namespace PlanWWSI.Views
{
    [DesignTimeVisible(false)]
    public partial class WykladowcaSzczegolyPage : ContentPage
    {
        private WykladowcyViewModel _viewModel;

        public WykladowcaSzczegolyPage(WykladowcyViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = _viewModel = vm;
        }
    }
}