using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseMuziek
{
    internal class LandDA
    {
        public static List<land> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de landen in te plaatsen
            List<land> LijstMetLanden = new List<land>();
            //We maken het statement aan om de landen uit te lezen
            string sSql = "Select Land_ID, Land FROM dbo.Landen";
            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            DataTable LandDT = Database.GetDT(sSql);
            //Hier lezen we de datatabel uit met een foreacht
            foreach (DataRow LandDR in LandDT.Rows)
            {
                land land = new land();
                //oEvaluatie.iAccountID = Int32.Parse(EvaluatieDR["Account_ID"].ToString());
                //hier vullen we de gegevens in in de aangemaakte klasse
                land.LandID = int.Parse(LandDR["Land_ID"].ToString());
                land.Land = LandDR["Land"].ToString();
                land.Continent = LandDR["Land"].ToString();
                //hier voegen we de klasse toe aan de lijst van de landen
                LijstMetLanden.Add(land);
            }
            return LijstMetLanden;
        }
        public static bool voegLandToe(land landen)
        {
            try
            {
                //hier geven we de sql string op
                string sql = "INSERT INTO Landen (Land) VALUES (@Land) ";
                //hier maken we de parameters aan om de dingen te kunnen aanvullen
                SqlParameter ParLand = new SqlParameter("@Land", landen.Land);
                SqlParameter ParContinent = new SqlParameter("@Land", landen.Continent);
                //hier sturen de opdracht naar de database
                Database.ExcecuteSQL(sql, ParLand);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool WijzigLand(land landen)
        {
            try
            {
                string sql = "UPDATE Land SET Land=@Land WHERE Land_ID=@LandID";
                SqlParameter ParLand = new SqlParameter("@Land", landen.Land);
                SqlParameter ParLandID = new SqlParameter("@LandID", landen.LandID);
                SqlParameter ParContinent = new SqlParameter("@Land", landen.Continent);
                Database.ExcecuteSQL(sql, ParLand, ParLandID);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteLand(int landID)
        {
            try
            {
                string sql = "DELETE FROM Land WHERE Land_ID=@Land_ID";
                SqlParameter parLandID = new SqlParameter("@Land_ID", landID);
                Database.ExcecuteSQL(sql, parLandID);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
