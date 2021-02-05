using PlanWWSI.Models;
using PlanWWSI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlanWWSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WykladowcyPage : ContentPage
    {
        private WykladowcyViewModel viewModel;

        public WykladowcyPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new WykladowcyViewModel();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as WykladowcaWidok;
            if (item == null)
                return;
            Navigation.PushAsync(new PlanWykladowcyPage(item));
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(this);
        }
    }
}