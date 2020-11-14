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
            viewModel.LoadItemsCommand.Execute(null);
            viewModel.PobierzZjazdyCommand.Execute(null);
        }

        private void OnDateChange(object sender, DateChangedEventArgs args)
        {
            viewModel.Data = args.NewDate;
            viewModel.LoadItemsCommand.Execute(null);
        }

        private void OnWsteczClicked(object sender, System.EventArgs e)
        {
            viewModel.UstawPoprzedniDzien();
        }

        private void OnDalejClicked(object sender, System.EventArgs e)
        {
            viewModel.UstawKolejnyDzien();
        }
    }
}