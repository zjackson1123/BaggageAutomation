
using System;
using System.IO;
using IronBarCode;
using System.Drawing;
using System.Drawing.Imaging;
using BaggageAutomation.Luggage;
using System.Windows.Media.Imaging;

namespace BaggageAutomation.QRcode
{
    internal class QRGen
    {
        public static string QRGeneration(LuggageDataItem lug, out BitmapImage QRdisplay)
        {
            string QRstring = Guid.NewGuid().ToString();
            QRstring += "|" + lug.Airline + "|" + lug.Owner + "|" + lug.Location + "|" + lug.Destination;
            GeneratedBarcode QRCode = QRCodeWriter.CreateQrCode(QRstring, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium);
            System.Drawing.Image QRImage = (System.Drawing.Image)QRCode.Image;
            Bitmap QRBit = new Bitmap(QRImage);
            QRdisplay = new BitmapImage();
            using(MemoryStream ms = new MemoryStream())
            {
                QRBit.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                QRdisplay.BeginInit();
                QRdisplay.StreamSource = ms;
                QRdisplay.CacheOption = BitmapCacheOption.OnLoad;
                QRdisplay.EndInit();
            }
            string filepath = Directory.GetCurrentDirectory() + "QRcode" + lug.Location + ".png";            
            QRBit.Save(filepath, ImageFormat.Png);
            return filepath;
        }
    }
}
