using System.Windows;
using static BaggageAutomation.SQL.SQL_Operations;
using static BaggageAutomation.Luggage.LuggageChecked;
using System.Windows.Media.Imaging;

namespace BaggageAutomation
{
    public partial class StartFlight : Window
    {
        public string Filepath;
        public BitmapImage QRImg;
        public StartFlight(string filepath, BitmapImage QRimg)
        {
            InitializeComponent();
            QRcodeimg.Stretch = System.Windows.Media.Stretch.Fill;
            QRcodeimg.Source = QRimg;
            QRImg = QRimg;
            QRcodeLbl.Content = "Here we have the QR code generated from your ticket information!";
            Filepath = filepath;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Arrival arr = new(Filepath, QRImg);
            arr.WindowState = WindowState.Maximized;
            this.Close();
            arr.ShowDialog();
           
           
        }
    }
}
