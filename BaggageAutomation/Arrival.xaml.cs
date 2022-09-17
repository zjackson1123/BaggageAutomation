using System.Windows;
using static BaggageAutomation.SQL_Operations;
using static BaggageAutomation.ScanQRCode;

namespace BaggageAutomation
{
    /// <summary>
    /// Interaction logic for Arrival.xaml
    /// </summary>
    public partial class Arrival : Window
    {
        LuggageDataItem storedLuggage;
        public Arrival(string filepath)
        {           
            storedLuggage = LuggageAtDest(sqlConnection, Decode(filepath));
            string welcometext = "Welcome to " + storedLuggage.Destination + "!";
            WelcomeLbl.Content = welcometext;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(storedLuggage != null) 
            {
                string owner = LuggagePickup(sqlConnection, storedLuggage);
                OwnerLbl.Content = owner;
            }
        }
    }
}
