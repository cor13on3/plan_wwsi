using System.ComponentModel;
using Xamarin.Forms;
using PlanWWSI.Models;
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

        public PlanZajecPage(ZajeciaViewModel vm)
        {
            InitializeComponent();

            BindingContext = viewModel = vm;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Zajecia;
            if (item == null)
                return;

            await Navigation.PushAsync(new ZajeciaSzczegolyPage(new ZajeciaSzczegolyViewModel(item)));

            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}