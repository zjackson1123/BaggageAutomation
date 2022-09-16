using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using static BaggageAutomation.SQL_Operations;

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
            //interesting idea could be to scan static images with generated QR code overlaid on it, allowing us to delve into image scraping
        }
    }
}
