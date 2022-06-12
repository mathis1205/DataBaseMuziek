using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMuziek
{
    public class genre
    {
        public int Genre_ID { get; set; }
        public string Genre { get; set; }
    }
    public class formaat
    {
        public int Formaat_ID { get; set; }
        public string Formaat { get; set; }
    }
    public class album
    {
        public int album_ID { get; set; }
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
        public int Land_ID { get; set; }
        public string Land { get; set; }
        public string Continent { get; set; }
    }
    public class muziek
    {
        public int Muziek_ID { get; set; }
        public string Liedje { get; set; }
        public string Duur { get; set; }
        public int Beoordeling { get; set; }
        public int Taal_ID { get; set; }
        public int Land_ID { get; set; }
        public int Formaat_ID { get; set; }
        public int Genre_ID { get; set; }
        public int Album_ID { get; set; }
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
        public int Taal_ID { get; set; }
        public string Taal { get; set; }
    }
    public class accounts
    {
        public int Account_ID { get; set; } 
        public string Naam { get; set; }
        public string Wachtwoord { get; set; }
    }
}
