using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Hack.Xamarin.Droid.Locator;

[assembly: Xamarin.Forms.Dependency(typeof(GpsLocator))]
namespace Hack.Xamarin.Droid.Locator
{
    public class GpsLocator : IGPSLocator
    {
        public Point GetLocation()
        {
            MainActivity activity = Forms.Context as MainActivity;
            return activity.CurrentLocation;
        }
    }
}