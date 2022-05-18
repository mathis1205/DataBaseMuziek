using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseMuziek
{
    internal class GenreDA
    {
        public static List<genre> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de genre in te plaatsen
            List<genre> LijstMetGenre = new List<genre>();

            //We maken het statement aan om de genre uit te lezen
            string sSql = "Select Genre_ID, Genre FROM dbo.Genre";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            DataTable genreDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow genreDR in genreDT.Rows)
            {
                genre genre = new genre();

                //hier vullen we de gegevens in in de aangemaakte klasse
                genre.GenreID = int.Parse(genreDR["Genre_ID"].ToString());
                genre.Genre = genreDR["Genre"].ToString();

                //hier voegen we de klasse toe aan de lijst van de genre
                LijstMetGenre.Add(genre);
            }
            return LijstMetGenre;
        }        
    }
}
