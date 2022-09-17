#pragma warning disable CS8601
#pragma warning disable CS8602
using System.Windows;
using static BaggageAutomation.SQL.SQL_Operations;
using static BaggageAutomation.QRcode.ScanQRCode;
using static BaggageAutomation.QRcode.OverlayImage;
using BaggageAutomation.Luggage;
using System.Windows.Media.Imaging;

namespace BaggageAutomation
{
    /// <summary>
    /// Interaction logic for Arrival.xaml
    /// </summary>
    public partial class Arrival : Window
    {
        LuggageDataItem storedLuggage;
        public Arrival(string filepath, BitmapImage QRimg)
        {
            string[] decodedInfo = Decode(filepath);
            storedLuggage = decodedInfo[0] == "Unable to read QR Code" ? null : LuggageAtDest(sqlConnection, decodedInfo);
            string welcometext = "Welcome to " + storedLuggage.Destination + "!";         
            InitializeComponent();
            WelcomeLbl.Content = welcometext;
            attachedQRimg.Stretch = System.Windows.Media.Stretch.Fill;
            attachedQRimg.Source = GetOverlaidImage(QRimg);
            attachedQRlbl.Content = "Here is your baggage being scanned in!";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(storedLuggage != null) 
            {
                string owner = LuggagePickup(sqlConnection, storedLuggage);
                OwnerLbl.Content = "Your Luggage is on its way " + owner + " c:";
            }
        }
    }
}
