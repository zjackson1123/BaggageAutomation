using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using static BaggageAutomation.SQLstring;

namespace BaggageAutomation
{
    internal class SQL_Operations
    {
        /// <summary>
        /// gets sql connection for global variable assignment
        /// </summary>
        /// <returns> a SqlConnection object</returns>
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            return conn;
        }

        //remove bag
        public static string DeliverLuggage(string luggageID, ref List<LuggageItem> currentList)
        {
            string rQuery = "SELECT * FROM dbo.Baggage WHERE LuggageID =" + luggageID + ";";

            return "";
        }

        //add bag
        public static List<LuggageItem> LuggageArrived(List<LuggageItem> currentList)
        {

            return currentList;
        }

        public static List<LuggageItem> GetAllLuggage(SqlConnection conn)
        {
            List<LuggageItem> luggagelist = new List<LuggageItem>();
            string lQuery = "SELECT * FROM dbo.Baggage";
            SqlCommand lCommand = new SqlCommand(lQuery, conn);
            SqlDataReader rdr = lCommand.ExecuteReader();
            while (rdr.Read())
            {
                LuggageItem _luggage = new LuggageItem();
                foreach(var row in rdr.GetColumnSchema())
                {
                    switch (row.ColumnName)
                    {
                        case "LuggageID":
                            _luggage.LuggageID = rdr.GetString((int)row.ColumnOrdinal);
                            break;
                        case "Airline":
                            _luggage.Airline = rdr.GetString((int)row.ColumnOrdinal);
                            break;
                        case "Owner":
                            _luggage.Owner = rdr.GetString((int)row.ColumnOrdinal);
                            break;
                        case "Location":
                            _luggage.Location = rdr.GetString ((int)row.ColumnOrdinal);
                            break;
                    }
                }
                luggagelist.Add(_luggage);
            }

            return luggagelist;
        }
        
    }
}
