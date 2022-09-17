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
        public static LuggageItem[] AllLuggage = GetAllLuggage(Conn);
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
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
                    System.DrawingCore.Bitmap bitmap = (System.DrawingCore.Bitmap)System.DrawingCore.Bitmap.FromFile(filename);
                    LuminanceSource ls;
                    ls = new ZXing.ZKWeb.BitmapLuminanceSource(bitmap);
                    var binarizer = new HybridBinarizer(ls);
                    var binarybmp = new BinaryBitmap(binarizer);
                    var result = new MultiFormatReader().decode(binarybmp);
                    if (result != null) { MessageBox.Show(result.ToString()); }
                    else { MessageBox.Show("No Result"); }

                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderTicket ot = new OrderTicket();
            ot.WindowState = WindowState.Maximized;
            ot.ShowDialog();
            string Name = OrderTicket.Ticket.Name;
            string Airline = OrderTicket.Ticket.Airline;
            //ImageBox.Source = CheckedIn(ref AllLuggage, Airline, Name);

        }
    }
}
