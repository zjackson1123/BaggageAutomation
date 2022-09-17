using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageAutomation
{
#pragma warning disable CS8618
    public class LuggageDataItem
    {
        public LuggageDataItem() { }
        public LuggageDataItem(string luggageID, string airline, string owner, int location)
        {
            LuggageID = luggageID;
            Airline = airline;
            Owner = owner;
            Location = location;
        }

        public string LuggageID { get; set; }
        public string Airline { get; set; }
        public string Owner { get; set; }
        public int Location { get; set; }

    }
}
