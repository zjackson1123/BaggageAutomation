using Microsoft.Data.SqlClient;
using System.Windows;
using Microsoft.Win32;
using static BaggageAutomation.SQL.SQL_Operations;
using ZXing;
using ZXing.Common;
using static BaggageAutomation.Luggage.LuggageChecked;

namespace BaggageAutomation
{
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            sqlConnection.Open();
            PopulateLugColl();
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        private void Scan_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderTicket ot = new OrderTicket();
            ot.WindowState = WindowState.Maximized;
            ot.Show();
            this.Close();
        }
    }
}
