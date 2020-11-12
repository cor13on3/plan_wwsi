using System.ComponentModel;
using Xamarin.Forms;
using PlanWWSI.ViewModels;

namespace PlanWWSI.Views
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