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
using System.Windows.Shapes;
using static BaggageAutomation.SQL_Operations;
using static BaggageAutomation.LuggageChecked;
using Microsoft.Data.SqlClient;

namespace BaggageAutomation{
#pragma warning disable CS8601
    /// <summary>
    /// Interaction logic for OrderTicket.xaml
    /// </summary>
    public partial class OrderTicket : Window
    {
        public static class Ticket
        {
            public static string Airline { get; set; }
            public static string Name { get; set; }
        }
        public OrderTicket()
        {
            InitializeComponent();
        }
        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            if (NameText.Text == String.Empty)
            {
                NameErrLbl.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                NameErrLbl.Visibility = Visibility.Hidden;
                Ticket.Airline = AirlineComboBox.Text;
                Ticket.Name = NameText.Text;
                this.Close();
            }
            Ticket.Airline = AirlineComboBox.Text;
            Ticket.Name = NameText.Text;
            this.Close();
            StartFlight sf = new StartFlight();
            sf.WindowState = WindowState.Maximized;
            sf.ShowDialog();
        }
    }
}
