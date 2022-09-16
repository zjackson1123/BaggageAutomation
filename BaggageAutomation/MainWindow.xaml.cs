using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            List<LuggageItem> AllLuggage = GetAllLuggage(Conn);
            foreach(LuggageItem item in AllLuggage)
            {
                MessageBox.Show(item.LuggageID + "\n" + item.Airline + "\n" + item.Owner + "\n" + item.Location);
            }          
        }

        private void Scan_Btn_Click(object sender, RoutedEventArgs e)
        {
            //interesting idea could be to scan static images with generated QR code overlaid on it, allowing us to delve into image scraping

        }
    }
}
