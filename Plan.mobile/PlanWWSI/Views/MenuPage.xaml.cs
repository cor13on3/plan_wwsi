using PlanWWSI.Models;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace PlanWWSI.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<AppMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<AppMenuItem>
            {
                new AppMenuItem {Id = MenuItemType.PlanZajec, Title = "Plan zajęć" },
                new AppMenuItem {Id = MenuItemType.Wykladowcy, Title = "Wykładowcy" },
                new AppMenuItem {Id = MenuItemType.ZmienGrupe, Title = "Zmień grupę" },
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[2];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((AppMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}