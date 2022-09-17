using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BaggageAutomation.QRcode
{
    internal class OverlayImage
    {
        public static BitmapImage GetOverlaidImage(BitmapImage QRcode)
        {
            Bitmap QRBit;
            using (MemoryStream outstream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(QRcode));
                enc.Save(outstream);
                QRBit = new Bitmap(outstream);
            }
            Bitmap luggage = (Bitmap)Bitmap.FromFile(Directory.GetCurrentDirectory() + @"\Luggageimg.png");
            Bitmap overlaid = new Bitmap(luggage.Width, luggage.Height);
            BitmapImage showOverlaid = new BitmapImage();
            using (Graphics gr = Graphics.FromImage(overlaid))
            {
                gr.DrawImage(luggage, new Point(0, 0));
                QRBit.SetResolution(60, 60);
                gr.DrawImage(QRBit, new Rectangle(300, 300, 60, 60));
            }
            using (MemoryStream ms = new MemoryStream())
            {
                overlaid.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                showOverlaid.BeginInit();
                showOverlaid.StreamSource = ms;
                showOverlaid.CacheOption = BitmapCacheOption.OnLoad;
                showOverlaid.EndInit();
            }
            return showOverlaid;
        }
    }
}
