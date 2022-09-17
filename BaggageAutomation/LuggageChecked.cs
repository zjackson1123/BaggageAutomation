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
using static BaggageAutomation.SQL_Operations;
using static BaggageAutomation.OrderTicket;

namespace BaggageAutomation
{
    public class LuggageChecked
    {
        public static string CheckedIn(Ticket ticket)
        {
            LuggageDataItem lug = new LuggageDataItem();
            lug.Airline = ticket.Airline;
            lug.Owner = ticket.Name;
            lug.Destination = ticket.Destination;  
            LocFind(out int? index);
            if (index.HasValue)
            {
                lug.Location = (int)index;
            }
            return QRGeneration(lug);
        }
        public static void LocFind(out int? index)
        {
            index = null;
            for (int i = 0; i < LugColl.Length; i++)
            {
                if (LugColl[i] != null)
                {
                    LugColl[i] = new LuggageDataItem();
                    index = i;
                    break;
                }                
            }
            if(index == null)
            {
                MessageBox.Show("No Locations Remain in Destination Airport");
            }
        }
        public static string QRGeneration(LuggageDataItem lug)
        {

            string QRstring = Guid.NewGuid().ToString();
            QRstring += " | " + lug.Airline + " | " + lug.Owner + " | " + lug.Location;
            GeneratedBarcode QRCode = QRCodeWriter.CreateQrCode(QRstring, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium);
            System.Drawing.Image QRImage = (System.Drawing.Image)QRCode.Image;
            Bitmap QRBit = new Bitmap(QRImage);
            BitmapImage bmp = new BitmapImage();
            string filepath = Directory.GetCurrentDirectory() + "QRcode" + lug.Location;
            using (MemoryStream ms = new MemoryStream())
            {
                
                QRBit.Save(filepath, ImageFormat.Png);
                //QRBit.Save(ms, ImageFormat.Png);
                //ms.Position = 0;
                //bmp.BeginInit();
                //bmp.StreamSource = ms;
                //bmp.CacheOption = BitmapCacheOption.OnLoad;
                //bmp.EndInit();
            }
            
            
            return filepath;
        }

    }
}
