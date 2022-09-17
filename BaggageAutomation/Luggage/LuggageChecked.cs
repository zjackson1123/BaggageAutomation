using System.Windows;
using static BaggageAutomation.SQL.SQL_Operations;
using static BaggageAutomation.OrderTicket;
using static BaggageAutomation.QRcode.QRGen;
using System.Windows.Media.Imaging;

namespace BaggageAutomation.Luggage
{
    public class LuggageChecked
    {
        public static string CheckedIn(Ticket ticket, out BitmapImage QRcode)
        {
            LuggageDataItem lug = new LuggageDataItem();
            lug.Airline = ticket.Airline;
            lug.Owner = ticket.Name;
            lug.Destination = ticket.Destination;
            LocFind(out int? index);
            if (index.HasValue)
            {
                lug.Location = (int)index;
            }
            string decodedQR = QRGeneration(lug, out BitmapImage QRdisplay);
            QRcode = QRdisplay;
            return decodedQR;
        }

        public static void LocFind(out int? index)
        {
            index = null;
            for (int i = 0; i < LugColl.Length; i++)
            {
                if (LugColl[i] == null)
                {
                    LugColl[i] = new LuggageDataItem();
                    index = i;
                    break;
                }
            }
            if (index == null)
            {
                MessageBox.Show("No Locations Remain in Destination Airport");
            }
        }
    }
}
