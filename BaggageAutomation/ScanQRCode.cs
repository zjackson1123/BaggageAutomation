using Microsoft.Win32;
using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing;
using System.Windows.Media.Imaging;
using System.Linq;

namespace BaggageAutomation
{
    internal class ScanQRCode
    {
        public static string[] Decode(string filepath)
        {
            //var dialog = new OpenFileDialog();
            //dialog.Multiselect = false;
            //dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;";
            //if (dialog.ShowDialog() == true)
            //{
            System.DrawingCore.Bitmap bitmap = (System.DrawingCore.Bitmap)System.DrawingCore.Bitmap.FromFile(filepath);
            LuminanceSource ls;
            ls = new ZXing.ZKWeb.BitmapLuminanceSource(bitmap);
            var binarizer = new HybridBinarizer(ls);
            var binarybmp = new BinaryBitmap(binarizer);
            var result = new MultiFormatReader().decode(binarybmp);
            if (result != null)
            {
                string[] DecodedQR= result.ToString().Split("|").ToArray();
                return DecodedQR;
            }
            else { return new string[] {"Unable to read QR Code"}; }
            //}

        }
    }
}
