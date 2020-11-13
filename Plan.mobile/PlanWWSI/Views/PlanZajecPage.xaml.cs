using System.ComponentModel;
using Xamarin.Forms;
using PlanWWSI.ViewModels;

namespace PlanWWSI.Views
{
    [DesignTimeVisible(false)]
    public partial class PlanZajecPage : ContentPage
    {
        ZajeciaViewModel viewModel;

        public PlanZajecPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ZajeciaViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}