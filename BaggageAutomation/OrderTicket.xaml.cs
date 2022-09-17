#pragma warning disable CS8618
using System.Windows;
using System.Windows.Media.Imaging;
using static BaggageAutomation.Luggage.LuggageChecked;

namespace BaggageAutomation
{
    /// <summary>
    /// Interaction logic for OrderTicket.xaml
    /// </summary>
    public partial class OrderTicket : Window
    {
        public Ticket ticket = new();
        public OrderTicket()
        {
            InitializeComponent();
        }
        public class Ticket
        {
            public string Airline { get; set; }
            public string Name { get; set; }
            public string Destination { get; set; }
        }

        public class ValidInput
        {
            public ValidInput(string name, string airline, string destination)
            {
                Name = name;
                Airline = airline;
                Destination = destination;
                if (name != string.Empty)
                {
                    NameValid = true;
                }
                if (airline != string.Empty)
                {
                    AirlineValid = true;
                }
                if (Destination != string.Empty)
                {
                    DestinationValid = true;
                }
            }
            public bool NameValid = false;
            public string Name;
            public bool AirlineValid = false;
            public string Airline;
            public bool DestinationValid = false;
            public string Destination;
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            ValidInput validInput = new ValidInput(NameText.Text, AirlineComboBox.Text, DestinationTextBox.Text);
            if (!validInput.NameValid) { NameErrLbl.Visibility = Visibility.Visible; }
            if (!validInput.AirlineValid) { AirlineErrLbl.Visibility = Visibility.Visible; }
            if (!validInput.DestinationValid) { DestinationErrLbl.Visibility = Visibility.Visible; }
            if (validInput.DestinationValid && validInput.AirlineValid && validInput.NameValid)
            {
                ticket.Name = validInput.Name;
                ticket.Airline = validInput.Airline;
                ticket.Destination = validInput.Destination;
                string filepath = CheckedIn(ticket, out BitmapImage QRcode);
                StartFlight sf = new(filepath, QRcode)
                {
                    WindowState = WindowState.Maximized
                };
                this.Close();
                sf.ShowDialog();
            }
        }
    }
}
