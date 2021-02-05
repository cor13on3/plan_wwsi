using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PlanWWSI.Services;
using PlanWWSI.Views;

namespace PlanWWSI
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            if (Properties.ContainsKey("grupa"))
                MainPage = new MainPage();
            else
                MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
