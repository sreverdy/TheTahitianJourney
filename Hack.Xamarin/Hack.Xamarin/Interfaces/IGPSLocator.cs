using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Hack.Xamarin
{
    public interface IGPSLocator 
    {
        Point GetLocation();
    }
}
