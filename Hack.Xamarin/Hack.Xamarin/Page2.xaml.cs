using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hack.Xamarin
{
    public partial class Page2 : Application
    {
        public Page2()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Hack.Xamarin.Page1());
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
