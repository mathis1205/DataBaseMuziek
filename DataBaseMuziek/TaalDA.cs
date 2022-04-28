using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseMuziek
{
    internal class TaalDA
    {
        public static List<taal> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de taal in te plaatsen
            List<taal> LijstMetTaal = new List<taal>();

            //We maken het statement aan om de taal uit te lezen
            string sSql = "Select Taal_ID, Taal FROM dbo.Taal";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            DataTable taalDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow taalDR in taalDT.Rows)
            {
                taal taal = new taal();

                //hier vullen we de gegevens in in de aangemaakte klasse
                taal.TaalID = int.Parse(taalDR["Taal_ID"].ToString());
                taal.Taal = taalDR["Taal"].ToString();

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
                string sql = "INSERT INTO Album (Album) VALUES (@Album) ";

                //hier maken we de parameters aan om de dingen te kunnen aanvullen
                SqlParameter ParAlbum = new SqlParameter("@Album", album.Album);

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
