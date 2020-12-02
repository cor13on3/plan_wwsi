using PlanWWSI.Models;
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
    public partial class PlanWykladowcyPage : ContentPage
    {
        private PlanWykladowcyViewModel viewModel;

        public PlanWykladowcyPage(WykladowcaWidok wykladowca)
        {
            InitializeComponent();
            BindingContext = viewModel = new PlanWykladowcyViewModel(wykladowca);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadItemsCommand.Execute(null);
        }

        private void OnDateChange(object sender, DateChangedEventArgs args)
        {
            viewModel.Data = args.NewDate;
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}