using Microsoft.Data.SqlClient;
using System.Windows;
using Microsoft.Win32;
using static BaggageAutomation.SQL_Operations;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Emgu.CV;
using System;
using Emgu.CV.Structure;
using System.Linq;
using System.Collections.Generic;
using ZXing;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Emgu.CV.Cuda;
using System.Windows.Controls;
using System.Text;
using ZXing.QrCode;
using ZXing.Common;

namespace BaggageAutomation
{
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SqlConnection Conn = GetConnection();
        public MainWindow()
        {
            InitializeComponent();
            LuggageItem[] AllLuggage = GetAllLuggage(Conn);
            
        }

        private static byte[] GetByteArray(System.Drawing.Image img)
        {
            byte[] toReturn = new byte[0];
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                toReturn = ms.ToArray();
            }
            return toReturn;
        }

        private void Scan_Btn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;";
            if (dialog.ShowDialog() == true)
            {
                foreach (string filename in dialog.FileNames)
                {
                    System.Drawing.Bitmap img = (Bitmap)System.Drawing.Bitmap.FromFile(filename);                 
                    BitmapImage _bmp = new BitmapImage();
                    _bmp.BeginInit();
                    _bmp.UriSource = new Uri(filename, UriKind.Absolute);
                    _bmp.EndInit();
                    var Wbmp = new WriteableBitmap(_bmp);                                   
                    QRCodeReader reader = new QRCodeReader();                    
                    ImageConverter conv = new ImageConverter();                                   
                    LuminanceSource ls = new RGBLuminanceSource(GetByteArray(img), img.Width, img.Height);
                    var binarizer = new HybridBinarizer(ls);
                    var binarybmp = new BinaryBitmap(binarizer);
                    string result = reader.decode(binarybmp).Text;
                    
                    
                    MessageBox.Show(result);
                }
            }

        }
    }
}
