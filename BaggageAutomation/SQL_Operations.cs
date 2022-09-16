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
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            return conn;
        }

        //remove bag
        public static string LuggagePickup(string luggageID, SqlConnection conn, ref List<LuggageItem> currentList)
        {
            string rQuery = "SELECT * FROM dbo.Luggage WHERE LuggageID =" + luggageID + ";";
            SqlCommand rCommand = new SqlCommand(rQuery, conn);
            SqlDataReader rdr = rCommand.ExecuteReader();
            //LuggageItem _luggage =
            string dQuery = "DELETE FROM dbo.Luggage WHERE LuggageID =" + luggageID + ";";

            return "";
        }

        //add bag
        public static LuggageItem[] LuggageArrived(SqlConnection conn, List<LuggageItem> currentList)
        {

            //luggage is being scanned into airport table

            string ID = "fake";
            string airline = "fake";
            string owner = "fake";
            string location = "fake";


            //scanner input

            string iQuery = "INSERT INTO dbo.Luggage (LuggageID, Airline, Owner, Location) \nVALUES (" + ID + ", " + airline + ", " + owner + ", " + location + ");";
            SqlCommand iCommand = new SqlCommand(iQuery, conn);
            iCommand.ExecuteNonQuery();


            return new LuggageItem[0];
            //currentList.Add();
        }

        public static LuggageItem[] GetAllLuggage(SqlConnection conn)
        {
            LuggageItem[] luggageArr = new LuggageItem[64];           
            string lQuery = "SELECT * FROM dbo.Luggage";
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
                            _luggage.Location = rdr.GetInt32((int)row.ColumnOrdinal);
                            break;
                    }
                }
                luggageArr[_luggage.Location] = _luggage;

            }

            return luggageArr;
        }
        
    }
}
