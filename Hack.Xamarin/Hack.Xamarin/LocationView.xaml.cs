using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hack.Xamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationView : ContentPage
	{
		public LocationView (int id)
		{
			InitializeComponent ();
            var locationPoint = StaticData.LocationPoints.FirstOrDefault(l => l.Id == id);
            if (locationPoint != null)
            {
                Title = locationPoint.Intitule;
                Description.Text = locationPoint.Description;
            }
            MessagingCenter.Subscribe<byte[]>(this, "ImageSelected", (args) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    //Set the source of the image view with the byte array
                    MaPhoto.Source = ImageSource.FromStream(() => new MemoryStream((byte[])args));
                });
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

           // await DisplayAlert("File Location", file.Path, "OK");

            MaPhoto.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
            OKButton.IsVisible = true;
            

        }

        private async void OK_Clicked(object sender, EventArgs e)
        {

            await Navigation.PopAsync();
        }

      
    }
}
