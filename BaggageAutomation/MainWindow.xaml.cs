using IronBarCode;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using static BaggageAutomation.SQL_Operations;
using static BaggageAutomation.LuggageChecked;

namespace BaggageAutomation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public static SqlConnection Conn = GetConnection();
        LuggageItem[] AllLuggage = GetAllLuggage(Conn);
        public MainWindow()
        {
            InitializeComponent();      
        }
        private void Scan_Btn_Click(object sender, RoutedEventArgs e)
        {            
            //interesting idea could be to scan static images with generated QR code overlaid on it, allowing us to delve into image scraping
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderTicket ot = new OrderTicket();
            ot.ShowDialog();
            string Name = OrderTicket.Ticket.Name;
            string Airline = OrderTicket.Ticket.Airline;
            ImageBox.Source = CheckedIn(ref AllLuggage, Airline, Name);
        }
    }
}
