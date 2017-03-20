using System;
using System.Collections.Generic;
using System.Text;

namespace Hack.Xamarin
{
    public class LocationPoint
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Intitule { get; set; }
        public string Description { get; set; }
    }
    static class StaticData
    {
        public static List<LocationPoint> LocationPoints = new List<LocationPoint>()
        {
            new LocationPoint() { Id=1, Latitude = -17.540347, Longitude= -149.826752 , Intitule="Le Belvédère", Description="Un endroit à ne pas manquer, pour sa vue des baies de cook de Moorea. La vue est sublime, Et très agréable." },
            new LocationPoint() { Id=2, Latitude = -17.498896, Longitude= -149.762134 , Intitule="La plage de Temae" }
        };
    }
}
