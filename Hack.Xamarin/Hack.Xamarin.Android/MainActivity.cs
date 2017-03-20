using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using System.Collections.Generic;
using System.Linq;
using Android.Util;
using Xamarin.Forms;
using Android.Content;
using Android.Database;
using Android.Provider;

namespace Hack.Xamarin.Droid
{
	[Activity (Label = "Hack.Xamarin", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ILocationListener
    {
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        public Point CurrentLocation;
        LocationManager _locationManager;

        string _locationProvider;
        public string LocationText;

        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        public  void OnLocationChanged(Location location)
        {
            CurrentLocation = new Point(location.Latitude, location.Longitude);
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
        }

        protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new Hack.Xamarin.Page2 ());
            InitializeLocationManager();

        }

        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Medium
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
            


            Log.Debug(TAG, "Using " + _locationProvider + ".");
        }

        //protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);

        //    //Since we set the request code to 1 for both the camera and photo gallery, that's what we need to check for
        //    if (requestCode == 1)
        //    {
        //        if (resultCode == Result.Ok)
        //        {
        //            if (data.Data != null)
        //            {
        //                //Grab the Uri which is holding the path to the image
        //                Android.Net.Uri uri = data.Data;

        //                //Read the meta data of the image to determine what orientation the image should be in
        //                int orientation = getOrientation(uri);

        //                //Stat a background task so we can do all the bitmap stuff off the UI thread
        //                BitmapWorkerTask task = new BitmapWorkerTask(this.ContentResolver, uri);
        //                task.Execute(orientation);
        //            }
        //        }
        //    }
        //}

        //public int getOrientation(Android.Net.Uri photoUri)
        //{
        //    ICursor cursor = Application.ApplicationContext.ContentResolver.Query(photoUri, new String[] { MediaStore.Images.ImageColumns.Orientation }, null, null, null);

        //    if (cursor.Count != 1)
        //    {
        //        return -1;
        //    }

        //    cursor.MoveToFirst();
        //    return cursor.GetInt(0);
        //}
    }





}

