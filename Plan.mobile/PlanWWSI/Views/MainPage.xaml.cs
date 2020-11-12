using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using App1.Models;

namespace App1.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.PlanZajec:
                        MenuPages.Add(id, new NavigationPage(new PlanZajecPage()));
                        break;
                    case (int)MenuItemType.Wykladowcy:
                        MenuPages.Add(id, new NavigationPage(new WykladowcyPage()));
                        break;
                    case (int)MenuItemType.ZmienGrupe:
                        MenuPages.Add(id, new NavigationPage(new LoginPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}