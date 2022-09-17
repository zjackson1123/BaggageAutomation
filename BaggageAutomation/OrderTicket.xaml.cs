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
using System.Security.RightsManagement;
using System.ComponentModel;
using System.Windows.Automation;

namespace BaggageAutomation
{
#pragma warning disable CS8601
    /// <summary>
    /// Interaction logic for OrderTicket.xaml
    /// </summary>
    public partial class OrderTicket : Window
    {
        public Ticket ticket;
        public OrderTicket()
        {
            InitializeComponent();        
        }
        public class Ticket
        {
            //public Guid TicketID = Guid.NewGuid();
            public string Airline { get; set; }
            public string Name { get; set; }
            public string Destination { get; set; }
        }     

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            string[] ticketInfo = new string[] { NameText.Text, AirlineComboBox.Text, DestinationTextBox.Text };
            bool validInput = true;
            for (int i = 0; i < ticketInfo.Length; i++)
            {
                if (ticketInfo[i] == string.Empty)
                {
                    validInput = false;
                }
            }
            if (validInput)
            {
                ticket.Name = ticketInfo[0];
                ticket.Airline = ticketInfo[1];
                ticket.Destination = ticketInfo[2];
                string filepath = CheckedIn(ticket);           
            }
            this.Close();          
        }
    }
}
