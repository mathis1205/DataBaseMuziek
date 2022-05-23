using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseMuziek
{
    internal class Muziek_ZangerDA
    {
        public static List<Muziek_Zanger> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de muziek en zangers in te plaatsen
            List<Muziek_Zanger> LijstMetMuziek_Zanger = new List<Muziek_Zanger>();

            //We maken het statement aan om de table uit te lezen
            string sSql = "Select Genre_ID, Muziek_ID, Zanger_ID FROM dbo.Muziek_Zanger";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            DataTable Muziek_ZangerDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow Muziek_ZangerDR in Muziek_ZangerDT.Rows)
            {
                Muziek_Zanger muziek_zanger = new Muziek_Zanger();

                //hier vullen we de gegevens in in de aangemaakte klasse
                muziek_zanger.Genre_ID = int.Parse(Muziek_ZangerDR["Genre_ID"].ToString());
                muziek_zanger.Muziek_ID = int.Parse(Muziek_ZangerDR["Muziek_ID"].ToString());
                muziek_zanger.Zanger_ID = int.Parse(Muziek_ZangerDR["Zanger_ID"].ToString());

                //hier voegen we de klasse toe aan de lijst van de album
                LijstMetMuziek_Zanger.Add(muziek_zanger);
            }
            return LijstMetMuziek_Zanger;
        }
        public static bool voegMuziek_ZangerToe(Muziek_Zanger _Muziek_Zanger)
        {
            try
            {
                //hier geven we de sql string op
                string sql = "INSERT INTO Muziek_Zanger (Genre_ID, Muziek_ID, Zanger_ID) VALUES (@Genre_ID, @Muziek_ID, @Zanger_ID)";

                //hier maken we de parameters aan om de dingen te kunnen aanvullen
                SqlParameter ParGenre_ID= new SqlParameter("@Genre_ID", _Muziek_Zanger.Genre_ID);
                SqlParameter ParMuziek_ID = new SqlParameter("@Muziek_ID", _Muziek_Zanger.Muziek_ID);
                SqlParameter ParZanger_ID = new SqlParameter("@Zanger_ID", _Muziek_Zanger.Zanger_ID);

                //hier sturen de opdracht naar de database
                Database.ExcecuteSQL(sql, ParGenre_ID, ParMuziek_ID, ParZanger_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool WijzigMuziek_Zanger(Muziek_Zanger _Muziek_Zanger)
        {
            try
            {
                //We maken het statement aan om de album up te daten.
                string sql = "UPDATE Muziek_Zanger SET Genre_ID=@Genre_ID, Muziek_ID=@Muziek_ID, Zanger_ID=@Zanger_ID WHERE Genre_ID=@Genre_ID OR Muziek_ID=@Muziek_ID OR Zanger_ID=@Zanger_ID";
                SqlParameter ParGenre_ID = new SqlParameter("@Genre_ID", _Muziek_Zanger.Genre_ID);
                SqlParameter ParMuziek_ID = new SqlParameter("@Muziek_ID", _Muziek_Zanger.Muziek_ID);
                SqlParameter ParZanger_ID = new SqlParameter("@Zanger_ID", _Muziek_Zanger.Zanger_ID);
                Database.ExcecuteSQL(sql, ParGenre_ID, ParMuziek_ID, ParZanger_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteMuziek_Zanger(int Genre_ID, int Muziek_ID, int Zanger_ID)
        {
            try
            {
                //We maken het statement aan om de album te verwijderen.
                string sql = "DELETE FROM Muziek_Zanger WHERE Genre_ID=@Genre_ID OR Muziek_ID=@Muziek_ID OR Zanger_ID=@Zanger_ID";
                SqlParameter ParGenre_ID = new SqlParameter("@Genre_ID", _Muziek_Zanger.Genre_ID);
                SqlParameter ParMuziek_ID = new SqlParameter("@Muziek_ID", _Muziek_Zanger.Muziek_ID);
                SqlParameter ParZanger_ID = new SqlParameter("@Zanger_ID", _Muziek_Zanger.Zanger_ID);
                Database.ExcecuteSQL(sql, ParGenre_ID, ParMuziek_ID, ParZanger_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
