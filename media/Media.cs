using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Motherboard_Diagnostic
{
    class Media
    {
        public static Image GetPicture(string filename)
        {
            Image image = new();
            image.Source= new BitmapImage(new Uri($"/media/{filename}", UriKind.Relative));
            return image;
        }
    }
}
