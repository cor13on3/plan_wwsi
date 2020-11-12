using System.ComponentModel;
using Xamarin.Forms;
using App1.ViewModels;

namespace App1.Views
{
    [DesignTimeVisible(false)]
    public partial class ZajeciaSzczegolyPage : ContentPage
    {
        ZajeciaSzczegolyViewModel viewModel;

        public ZajeciaSzczegolyPage(ZajeciaSzczegolyViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ZajeciaSzczegolyPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}