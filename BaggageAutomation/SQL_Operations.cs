using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using static BaggageAutomation.SQLstring;

namespace BaggageAutomation
{
#pragma warning disable CS8629
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

        public static int[] LuggagePickup(LuggageItem[] UserLuggage, SqlConnection conn, ref LuggageItem[] currentArr)
        {
            int[] LocationInt = new int[UserLuggage.Length];
            for(int i = 0; i < UserLuggage.Length; i++)
            {
                string rQuery = "SELECT * FROM dbo.Luggage WHERE LuggageID =" + UserLuggage[i].LuggageID + ";";
                SqlCommand rCommand = new SqlCommand(rQuery, conn);
                SqlDataReader rdr = rCommand.ExecuteReader();
                rdr.Read();
                LocationInt[i] = rdr.GetInt32(rdr.GetOrdinal("Location"));
                string dQuery = "DELETE FROM dbo.Luggage WHERE LuggageID =" + UserLuggage[i].LuggageID + ";";
                SqlCommand dCommand = new SqlCommand(dQuery, conn);
                dCommand.ExecuteNonQuery();
                rdr.Close();
            }
           
            return LocationInt;
        }

        public static LuggageItem[] LuggageAtDest(SqlConnection conn, LuggageItem[] currentArr)
        {
            //QR code scanned as luggage arrives at destination airport
            LuggageItem _lug = new LuggageItem("id", "airline", "owner", 10);
            string iQuery = "INSERT INTO dbo.Luggage (LuggageID, Airline, Owner, Location) " +
                "\nVALUES ('" + _lug.LuggageID + "', '" + _lug.Airline + "', '" + _lug.Owner + "', '" + _lug.Location + "');";
            SqlCommand iCommand = new SqlCommand(iQuery, conn);
            iCommand.ExecuteNonQuery();
            currentArr[_lug.Location] = _lug;  
            return currentArr;            
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
            rdr.Close();
            return luggageArr;
        }
        
    }
}
