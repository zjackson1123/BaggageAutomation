using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows;
using Microsoft.Win32;
using static BaggageAutomation.SQL_Operations;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using Emgu.CV;
using System;
using Emgu.CV.Structure;

namespace BaggageAutomation
{
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

        private void Scan_Btn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;";
            if(dialog.ShowDialog() == true)
            {
                foreach (string filename in dialog.FileNames)
                {
                    BitmapImage bmp = new BitmapImage();
                    BitmapImage obmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(filename, UriKind.Absolute);                   
                    bmp.EndInit();
                    ImageBox.Stretch = Stretch.Fill;
                    ImageBox.Source = bmp;
                    Image<Bgr, byte> inputImage = new Image<Bgr, byte>((int)bmp.Width, (int)bmp.Height);
                    Image<Bgr, byte> outputImage = new Image<Bgr, byte>((int)obmp.Width, (int)obmp.Height); ;
                    Mat Matimage = new Mat();
                    Mat outMat = new Mat();
                    Matimage = inputImage.Mat;
                    outMat = outputImage.Mat;
                    IInputArray inputArray = Matimage;
                    IOutputArray outputArray = outMat;
                    QRCodeDetector qrcodeDetector = new QRCodeDetector();
                    qrcodeDetector.Detect(inputArray, outputArray);
                    string test = qrcodeDetector.Decode(inputArray, outputArray);
                }
            }
            //interesting idea could be to scan static images with generated QR code overlaid on it, allowing us to delve into image scraping
        }
    }
}
