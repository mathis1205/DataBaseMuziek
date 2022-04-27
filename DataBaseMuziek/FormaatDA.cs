using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseMuziek
{
    internal class FormaatDA
    {
        public static List<formaat> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor het formaat in te plaatsen
            List<formaat> LijstMetFormaat = new List<formaat>();

            //We maken het statement aan om het formaat uit te lezen
            string sSql = "Select Formaat_ID, Formaat FROM dbo.Formaat";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            DataTable formaatDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow formaatDR in formaatDT.Rows)
            {
                formaat formaat = new formaat();

                //hier vullen we de gegevens in in de aangemaakte klasse
                formaat.FormaatID = int.Parse(formaatDR["Formaat_ID"].ToString());
                formaat.Formaat = formaatDR["Formaat"].ToString();

                //hier voegen we de klasse toe aan de lijst van formaat
                LijstMetFormaat.Add(formaat);
            }
            return LijstMetFormaat;
        }
        public static bool voegFormaatToe(formaat formaat)
        {
            try
            {
                //hier geven we de sql string op
                string sql = "INSERT INTO Formaat (Formaat) VALUES (@Formaat) ";

                //hier maken we de parameters aan om de dingen te kunnen aanvullen
                SqlParameter ParFormaat = new SqlParameter("@Formaat", formaat.Formaat);

                //hier sturen de opdracht naar de database
                Database.ExcecuteSQL(sql, ParFormaat);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool WijzigFormaat(formaat formaat)
        {
            try
            {
                //We maken het statement aan om het formaat up te daten.
                string sql = "UPDATE Formaat SET Formaat=@Formaat WHERE Formaat_ID=@Formaat_ID";
                SqlParameter ParFormaat = new SqlParameter("@Formaat", formaat.Formaat);
                SqlParameter ParFormaatID = new SqlParameter("@Formaat_ID", formaat.FormaatID);
                Database.ExcecuteSQL(sql, ParFormaat, ParFormaatID);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteFormaat(int FormaatID)
        {
            try
            {
                //We maken het statement aan om de Formaat te verwijderen.
                string sql = "DELETE FROM Formaat WHERE Formaat_ID=@Formaat_ID";
                SqlParameter parFormaatID = new SqlParameter("@Formaat_ID", FormaatID);
                Database.ExcecuteSQL(sql, parFormaatID);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
