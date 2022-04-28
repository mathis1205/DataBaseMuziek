using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseMuziek
{
    internal class MuziekDA
    {
        public static List<muziek> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de muziek in te plaatsen
            List<muziek> LijstMetMuziek = new List<muziek>();

            //We maken het statement aan om de album uit te lezen
            string sSql = "Select Muziek_ID, Muziek FROM dbo.Muziek";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            DataTable muziekDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow muziekDR in muziekDT.Rows)
            {
                muziek muziek = new muziek();

                //hier vullen we de gegevens in in de aangemaakte klasse
                muziek.MuziekID = int.Parse(muziekDR["Muziek_ID"].ToString());
                muziek.Liedje = muziekDR["Liedje"].ToString();
                muziek.Duur = muziekDR["Duur"].ToString();
                muziek.Beoordeling = muziekDR["Bedoordeling"].ToString();
                muziek.TaalID = int.Parse(muziekDR["Taal_ID"].ToString());
                muziek.LandID = int.Parse(muziekDR["Land_ID"].ToString());
                muziek.FormaatID = int.Parse(muziekDR["Formaat_ID"].ToString());
                muziek.GenreID = int.Parse(muziekDR["Genre_ID"].ToString());
                muziek.AlbumID = int.Parse(muziekDR["Album_ID"].ToString());

                //hier voegen we de klasse toe aan de lijst van de muziek
                LijstMetMuziek.Add(muziek);
            }
            return LijstMetMuziek;
        }
        public static bool voegMuziekToe(muziek muziek)
        {
            try
            {
                //hier geven we de sql string op
                string sql = "INSERT INTO Muziek (Muziek) VALUES (@Muziek) ";

                //hier maken we de parameters aan om de dingen te kunnen aanvullen
                SqlParameter ParMuziek = new SqlParameter("@Muziek", muziek.Liedje);
                SqlParameter ParDuur = new SqlParameter("@Muziek", muziek.Duur);
                SqlParameter ParBeoordeling = new SqlParameter("@Muziek", muziek.Beoordeling);
                SqlParameter ParTaalID = new SqlParameter("@Muziek", muziek.TaalID);
                SqlParameter ParLandID = new SqlParameter("@Muziek", muziek.LandID);
                SqlParameter ParFormaatID = new SqlParameter("@Muziek", muziek.FormaatID);
                SqlParameter ParGenreID = new SqlParameter("@Muziek", muziek.GenreID);
                SqlParameter ParAlbumID = new SqlParameter("@Muziek", muziek.AlbumID);

                //hier sturen de opdracht naar de database
                Database.ExcecuteSQL(sql, ParMuziek, ParDuur, ParBeoordeling, ParTaalID, ParLandID, ParFormaatID, ParGenreID, ParAlbumID);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool WijzigMuziek(muziek muziek)
        {
            try
            {
                //We maken het statement aan om de muziek up te daten.
                string sql = "UPDATE Muziek SET Muziek=@Muziek WHERE Muziek_ID=@Muziek_ID";
                SqlParameter ParMuziek = new SqlParameter("@Muziek", muziek.Liedje);
                SqlParameter ParMuziekID = new SqlParameter("@Muziek", muziek.MuziekID);
                SqlParameter ParDuur = new SqlParameter("@Muziek", muziek.Duur);
                SqlParameter ParBeoordeling = new SqlParameter("@Muziek", muziek.Beoordeling);
                SqlParameter ParTaalID = new SqlParameter("@Muziek", muziek.TaalID);
                SqlParameter ParLandID = new SqlParameter("@Muziek", muziek.LandID);
                SqlParameter ParFormaatID = new SqlParameter("@Muziek", muziek.FormaatID);
                SqlParameter ParGenreID = new SqlParameter("@Muziek", muziek.GenreID);
                SqlParameter ParAlbumID = new SqlParameter("@Muziek", muziek.AlbumID);

                //hier sturen de opdracht naar de database
                Database.ExcecuteSQL(sql, ParMuziekID, ParMuziek, ParDuur, ParBeoordeling, ParTaalID, ParLandID, ParFormaatID, ParGenreID, ParAlbumID);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteMuziek(int MuziekID)
        {
            try
            {
                //We maken het statement aan om muziek te verwijderen.
                string sql = "DELETE FROM Muziek WHERE Muziek_ID=@Muziek_ID";
                SqlParameter parMuziekID = new SqlParameter("@Muziek_ID", MuziekID);
                Database.ExcecuteSQL(sql, parMuziekID);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
