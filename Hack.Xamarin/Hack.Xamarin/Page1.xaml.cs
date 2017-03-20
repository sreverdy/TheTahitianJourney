using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hack.Xamarin
{
  

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {

        IGPSLocator gpsLocator;
        Timer timerGPS;
        double priseEnCompteGps = 0.01;

        public Page1()
        {
            InitializeComponent();

            gpsLocator = DependencyService.Get<IGPSLocator>();
            timerGPS = new Timer(CheckGPSLocation, null, 0, 5000);

        }


        void CheckGPSLocation(Object state)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    //var locator = CrossGeolocator.Current;
                    //locator.DesiredAccuracy = 50;

                    //var position = locator.GetPositionAsync(timeoutMilliseconds: 10000);
                    
                    var pointActuel = gpsLocator.GetLocation();

                    if (pointActuel != null)
                    {
                        foreach (var item in StaticData.LocationPoints)
                        {
                            var pA = new Point(item.Latitude, item.Longitude);
                            double distance = pA.Distance(pointActuel);
                            if (distance <= priseEnCompteGps)
                            {
                                timerGPS.Dispose();
                                Navigation.PushAsync(new LocationView(item.Id));
                                break;
                            }
                            else
                            {
                                //timerGPS.Change(2000, 2000);
                                testGPS.Text = distance.ToString();
                            }
                        }
                    }
                    else
                    {
                    //    timerGPS.Change(2000, 2000);
                        testGPS.Text = "null";
                    }
                }
                catch (Exception ex)
                {
                   // timerGPS.Change(2000, 2000);
                    testGPS.Text = ex.Message;
                }
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            CheckGPSLocation(null);
            // await 
        }
    }
}
