using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace PlanWWSI.Testy
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("com.companyname.PlanWWSI").StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}