﻿using System;
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

        //add bag
        public static LuggageItem[] LuggageArrived(SqlConnection conn, LuggageItem[] currentArr)
        {

            string LuggageID = "fake";
            string airline = "fake";
            string owner = "fake";
            int location = 17;
            //scanner input

            string iQuery = "INSERT INTO dbo.Luggage (LuggageID, Airline, Owner, Location) \nVALUES ('" + LuggageID + "', '" + airline + "', '" + owner + "', '" + location + "');";
            SqlCommand iCommand = new SqlCommand(iQuery, conn);
            iCommand.ExecuteNonQuery();
            LuggageItem dadada = new LuggageItem(LuggageID, airline, owner, location);
            for(int i = 0; i < currentArr.Length; i++)
            {
                if(currentArr[i] != null)
                {
                    currentArr[i] = dadada;
                    break;
                }
            }

            return currentArr;
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
            rdr.Close();
            return luggageArr;
        }
        
    }
}
