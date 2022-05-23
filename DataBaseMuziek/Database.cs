﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataBaseMuziek
{
    internal class Database
    {
        // -- DE FORM VOOR HET DE DATABASE AAN TE SPREKEN
        // -- VERSIE 1.0 --
        // -- DATUM 27/04/2022 --
        // -- Mathis Huygelier --
        // -- Guldensporencollege campus Engineering --

        // -- Enumerations --

        // -- Fields --

        // -- Properties --


        private static String ConnectionString
        {
            get
            {
                //het aanspreken van de database via de setting.setting interface.
                string connectionString = "Server=F1764;Database=DatabaseMuziek;Trusted_Connection=Yes";
                return connectionString;
            }
        }

        // -- Constructors --

        private Database()
        {
        }

        // -- methods --
        // connectie maken met de database en ook de connectie sluiten.
        private static SqlConnection GetConnection()
        {
            //het verbinden met de database
            SqlConnection oCon = new SqlConnection(ConnectionString);
            //het openen van de connectie
            oCon.Open();
            return oCon;
        }
        //de connectie sluiten en vrijgeven
        private static void ReleaseConnection(SqlConnection oCon)
        {
            if (oCon != null)
            {
                oCon.Close();
                oCon.Dispose();
            }
        }
        //Command maken op basis van bestaande connection, SQL en parameters.
        private static SqlCommand BuildCommand(SqlConnection oCon, string sSql, params SqlParameter[] dbParams)
        {
            SqlCommand oCommand = oCon.CreateCommand();
            oCommand.CommandType = System.Data.CommandType.Text;
            oCommand.CommandText = sSql;
            foreach (SqlParameter oPar in dbParams)
            {
                oCommand.Parameters.Add(oPar);
            }
            return oCommand;
        }
        // een opdracht maken op basis van de SQL en de parameters
        private static SqlCommand BuildCommand(String sSql, params SqlParameter[] dbParams)
        {
            SqlConnection oCon = GetConnection();
            return BuildCommand(oCon, sSql, dbParams);
        }
        //een data tabel ophalen uit de database
        public static DataTable GetDT(String sSql, params SqlParameter[] dbParams)
        {
            SqlCommand oCommand = null;
            try
            {
                oCommand = BuildCommand(sSql, dbParams);
                SqlDataAdapter oDA = new SqlDataAdapter();
                oDA.SelectCommand = oCommand;
                DataTable oDT = new DataTable();
                oDA.Fill(oDT);
                return oDT;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (oCommand != null)
                {
                    ReleaseConnection(oCommand.Connection);
                }
            }
        }
        // een data lezer ophalen
        public static SqlDataReader GetDR(string sSql, params SqlParameter[] dbParams)
        {
            SqlCommand oCommand = null;
            SqlDataReader oDR = null;
            try
            {
                oCommand = BuildCommand(sSql, dbParams);
                oDR = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return oDR;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oCommand != null)
                {
                    ReleaseConnection(oCommand.Connection);
                }
            }
        }
        // een resultaat uit de database ophalen
        public static Object executeScalar(string sSql, params SqlParameter[] dbParams)
        {
            SqlCommand oCommand = null;
            try
            {
                oCommand = BuildCommand(sSql, dbParams);
                Object oObject = oCommand.ExecuteScalar();
                return oObject;
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (oCommand != null)
                {
                    ReleaseConnection(oCommand.Connection);
                }
            }
        }
        //het uitvoeren van update, delete en insert via onderstaande gegevens.
        public static void ExcecuteSQL(String sSQL, params SqlParameter[] dbParams)
        {
            SqlCommand oCommand = null;
            try
            {
                oCommand = BuildCommand(sSQL, dbParams);
                oCommand.ExecuteNonQuery();
            }
            catch           
            {
                throw;
            }
            finally
            {
                if (oCommand != null)
                {
                    ReleaseConnection(oCommand.Connection);
                }
            }
        }
    }
}
