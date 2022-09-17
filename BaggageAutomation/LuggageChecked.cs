using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using IronBarCode;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Xaml.Schema;
using System.Windows;

namespace BaggageAutomation
{
    public class LuggageChecked
    {
        public static BitmapImage CheckedIn(ref LuggageDataItem[] currentArr, string airline, string owner)
        {
            LuggageDataItem lug = new LuggageDataItem();
            lug.Airline = airline;
            lug.Owner = owner;
            LocFind(ref currentArr, out int? index);
            if (index.HasValue)
            {
                lug.Location = (int)index;
            }
            return QRGeneration(lug);
        }
        public static void LocFind(ref LuggageDataItem[] currentArr, out int? index)
        {
            index = null;
            for (int i = 0; i < currentArr.Length; i++)
            {
                if (currentArr[i] != null)
                {
                    currentArr[i] = new LuggageDataItem();
                    index = i;
                    break;
                }                
            }
            if(index == null)
            {
                MessageBox.Show("No Locations Remain in Destination Airport");
            }
        }
        public static BitmapImage QRGeneration(LuggageDataItem lug)
        {

            string QRstring = Guid.NewGuid().ToString();
            QRstring += " | " + lug.Airline + " | " + lug.Owner + " | " + lug.Location;
            GeneratedBarcode QRCode = QRCodeWriter.CreateQrCode(QRstring, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium);
            System.Drawing.Image QR = (System.Drawing.Image)QRCode.Image;
            Bitmap QRBit = new Bitmap(QR);
            BitmapImage bmp = new BitmapImage();
            using (MemoryStream ms = new MemoryStream())
            {
                QRBit.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                bmp.BeginInit();
                bmp.StreamSource = ms;
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.EndInit();

            }
            return bmp;
        }

    }
}
