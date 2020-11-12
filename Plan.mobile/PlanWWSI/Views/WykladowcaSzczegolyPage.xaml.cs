using System.ComponentModel;
using Xamarin.Forms;
using App1.ViewModels;

namespace App1.Views
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