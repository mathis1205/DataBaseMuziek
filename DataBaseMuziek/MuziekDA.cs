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

                //hier voegen we de klasse toe aan de lijst van de album
                LijstMetAlbum.Add(album);
            }
            return LijstMetAlbum;
        }
        public static bool voegAlbumToe(album album)
        {
            try
            {
                //hier geven we de sql string op
                string sql = "INSERT INTO Landen (Land) VALUES (@land) ";

                //hier maken we de parameters aan om de dingen te kunnen aanvullen
                SqlParameter ParAlbum = new SqlParameter("@land", album.Album);

                //hier sturen de opdracht naar de database
                Database.ExcecuteSQL(sql, ParAlbum);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool WijzigAlbum(album album)
        {
            try
            {
                //We maken het statement aan om de album up te daten.
                string sql = "UPDATE Album SET Album=@Album WHERE Album_ID=@Album_ID";
                SqlParameter ParAlbum = new SqlParameter("@Album", album.Album);
                SqlParameter ParAlbumID = new SqlParameter("@Album_ID", album.albumID);
                Database.ExcecuteSQL(sql, ParAlbum, ParAlbumID);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteAlbum(int AlbumID)
        {
            try
            {
                //We maken het statement aan om de album te verwijderen.
                string sql = "DELETE FROM Album WHERE Album_ID=@Album_ID";
                SqlParameter parAlbumID = new SqlParameter("@Album_ID", AlbumID);
                Database.ExcecuteSQL(sql, parAlbumID);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
