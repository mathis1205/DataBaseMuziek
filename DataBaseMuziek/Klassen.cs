using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMuziek
{
    public class Klassen
    {
    }
    public class genre
    {
        public int GenreID { get; set; }
        public string Genre { get; set; }
    }
    public class formaat
    {
        public int FormaatID { get; set; }
        public string Formaat { get; set; }
    }
    public class album
    {
        public int albumID { get; set; }
        public string Album { get; set; }
    }
    public class Muziek_Zanger
    {
        public int Genre_ID { get; set; }
        public int Muziek_ID { get; set; }
        public int Zanger_ID { get; set; }
    }
    public class land
    {
        public int LandID { get; set; }
        public string Land { get; set; }
        public string Continent { get; set; }
    }
    public class muziek
    {
        public int MuziekID { get; set; }
        public string Liedje { get; set; }
        public string Duur { get; set; }
        public string Beoordeling { get; set; }
        public int TaalID { get; set; }
        public int LandID { get; set; }
        public int FormaatID { get; set; }
        public int GenreID { get; set; }
        public int AlbumID { get; set; }
    }
    public class zanger
    {
        public int Zanger_ID { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string ArtiestenNaam { get; set; }
        public int Land_ID { get; set; }
    }
    public class taal
    {
        public int TaalID { get; set; }
        public string Taal { get; set; }
    }
}
