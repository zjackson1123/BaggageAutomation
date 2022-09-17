#pragma warning disable CS8618
#pragma warning disable CS8629
using BaggageAutomation.Luggage;
using Microsoft.Data.SqlClient;
using System;
using static BaggageAutomation.SQL.SQLstring;

namespace BaggageAutomation.SQL
{
    internal class SQL_Operations
    {
        public static SqlConnection sqlConnection = new SqlConnection(connString);
        public static LuggageDataItem[] LugColl;

        public static string LuggagePickup(SqlConnection conn, LuggageDataItem storedLuggage)
        {
            string rQuery = "SELECT * FROM dbo.Luggage WHERE LuggageID ='" + storedLuggage.LuggageID + "';";
            SqlCommand rCommand = new SqlCommand(rQuery, conn);
            SqlDataReader rdr = rCommand.ExecuteReader();
            rdr.Read();
            string LuggageOwner = rdr.GetString(2);
            rdr.Close();
            string dQuery = "DELETE FROM dbo.Luggage WHERE LuggageID ='" + storedLuggage.LuggageID + "';";
            SqlCommand dCommand = new SqlCommand(dQuery, conn);
            dCommand.ExecuteNonQuery();
            return LuggageOwner;
        }

        public static LuggageDataItem LuggageAtDest(SqlConnection conn, string[] dQR)
        {
            //QR code scanned as luggage arrives at destination airport
            LuggageDataItem _lug = new LuggageDataItem(dQR[0], dQR[1], dQR[2], Convert.ToInt32(dQR[3]), dQR[4]);
            string iQuery = "INSERT INTO dbo.Luggage (LuggageID, Airline, Owner, Location) " +
                "\nVALUES ('" + _lug.LuggageID + "', '" + _lug.Airline + "', '" + _lug.Owner + "', '" + _lug.Location + "');";
            SqlCommand iCommand = new SqlCommand(iQuery, conn);
            iCommand.ExecuteNonQuery();
            LugColl[_lug.Location] = _lug;
            return _lug;
        }

        public static void PopulateLugColl()
        {
            LugColl = GetAllLuggage(sqlConnection);
        }

        private static LuggageDataItem[] GetAllLuggage(SqlConnection conn)
        {
            LuggageDataItem[] luggageArr = new LuggageDataItem[64];
            string lQuery = "SELECT * FROM dbo.Luggage";
            SqlCommand lCommand = new SqlCommand(lQuery, conn);
            SqlDataReader rdr = lCommand.ExecuteReader();
            while (rdr.Read())
            {
                LuggageDataItem _luggage = new LuggageDataItem();
                foreach (var row in rdr.GetColumnSchema())
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
