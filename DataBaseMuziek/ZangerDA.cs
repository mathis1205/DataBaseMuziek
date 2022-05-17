using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMuziek
{
    internal class ZangerDA
    {
        public static List<zanger> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de zanger in te plaatsen
            List<zanger> LijstMetZanger = new List<zanger>();

            //We maken het statement aan om de zanger uit te lezen
            string sSql = "Select Zanger_ID, Naam, Voornaam, ArtiestenNaam, Land_ID FROM dbo.Zanger";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            DataTable zangerDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow zangerDR in zangerDT.Rows)
            {
                zanger zanger = new zanger();

                //hier vullen we de gegevens in in de aangemaakte klasse
                zanger.Zanger_ID = int.Parse(zangerDR["Zanger_ID"].ToString());
                zanger.Naam = zangerDR["Naam"].ToString();
                zanger.Voornaam = zangerDR["Voornaam"].ToString();
                zanger.ArtiestenNaam = zangerDR["ArtiestenNaam"].ToString();
                zanger.Zanger_ID = int.Parse(zangerDR["Land_ID"].ToString());

                //hier voegen we de klasse toe aan de lijst van de zanger
                LijstMetZanger.Add(zanger);
            }
            return LijstMetZanger;
        }
        public static bool voegZangerToe(zanger zanger)
        {
            try
            {
                //hier geven we de sql string op
                string sql = "INSERT INTO Zanger (Naam, Voornaam, ArtiestenNaam, Land_ID) VALUES (@Naam, @Voornaam, @ArtiestenNaam, @Land_ID)";

                //hier maken we de parameters aan om de gegevens te kunnen invullen.
                SqlParameter ParNaam = new SqlParameter("@Naam", zanger.Naam);
                SqlParameter ParVoornaam = new SqlParameter("@Voornaam", zanger.Voornaam);
                SqlParameter ParArtiestenNaam = new SqlParameter("@ArtiestenNaam", zanger.ArtiestenNaam);
                SqlParameter ParLand_ID = new SqlParameter("@Land_ID", zanger.Land_ID);

                //hier sturen de opdracht naar de database
                Database.ExcecuteSQL(sql, ParNaam, ParVoornaam, ParArtiestenNaam, ParLand_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Wijzigzanger(zanger zanger)
        {
            try
            {
                //We maken het statement aan om de zanger up te daten.
                string sql = "UPDATE Zanger SET Naam=@Naam, Voornaam=@Voornaam, ArtiestenNaam=@ArtiestenNaam, Land_ID=@Land_ID WHERE Zanger_ID=@Zanger_ID";
                SqlParameter ParNaam = new SqlParameter("@Naam", zanger.Naam);
                SqlParameter ParVoornaam = new SqlParameter("@Voornaam", zanger.Voornaam);
                SqlParameter ParArtiestenNaam = new SqlParameter("@ArtiestenNaam", zanger.ArtiestenNaam);
                SqlParameter ParLand_ID = new SqlParameter("@Land_ID", zanger.Land_ID);
                SqlParameter ParZanger_ID = new SqlParameter("@Zanger_ID", zanger.Zanger_ID);
                Database.ExcecuteSQL(sql, ParNaam, ParVoornaam, ParArtiestenNaam, ParLand_ID, ParZanger_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Deletezanger(int Zanger_ID)
        {
            try
            {
                //We maken het statement aan om de zanger te verwijderen.
                string sql = "DELETE FROM Zanger WHERE Zanger_ID=@Zanger_ID";
                SqlParameter parZanger_ID = new SqlParameter("@Zanger_ID", Zanger_ID);
                Database.ExcecuteSQL(sql, parZanger_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
