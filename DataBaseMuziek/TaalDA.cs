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
                LijstMetTaal.Add(taal);
            }
            return LijstMetTaal;
        }
    }
}
