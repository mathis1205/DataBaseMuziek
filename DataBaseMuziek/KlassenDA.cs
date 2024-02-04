using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseMuziek
{
    public class ZangerDA
    {
        public static List<zanger> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de zanger in te plaatsen
            var LijstMetZanger = new List<zanger>();

            //We maken het statement aan om de zanger uit te lezen
            var sSql = "Select Zanger_ID, Naam, Voornaam, ArtiestenNaam, Land_ID FROM dbo.Zanger";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            var zangerDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow zangerDR in zangerDT.Rows)
            {
                var zanger = new zanger();

                //hier vullen we de gegevens in in de aangemaakte klasse
                zanger.Zanger_ID = int.Parse(zangerDR["Zanger_ID"].ToString());
                zanger.Naam = zangerDR["Naam"].ToString();
                zanger.Voornaam = zangerDR["Voornaam"].ToString();
                zanger.ArtiestenNaam = zangerDR["ArtiestenNaam"].ToString();
                zanger.Land_ID = int.Parse(zangerDR["Land_ID"].ToString());

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
                var sql =
                    "INSERT INTO Zanger (Naam, Voornaam, ArtiestenNaam, Land_ID) VALUES (@Naam, @Voornaam, @ArtiestenNaam, @Land_ID)";

                //hier maken we de parameters aan om de gegevens te kunnen invullen.
                var ParNaam = new SqlParameter("@Naam", zanger.Naam);
                var ParVoornaam = new SqlParameter("@Voornaam", zanger.Voornaam);
                var ParArtiestenNaam = new SqlParameter("@ArtiestenNaam", zanger.ArtiestenNaam);
                var ParLand_ID = new SqlParameter("@Land_ID", zanger.Land_ID);

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
                var sql =
                    "UPDATE Zanger SET Naam=@Naam, Voornaam=@Voornaam, ArtiestenNaam=@ArtiestenNaam, Land_ID=@Land_ID WHERE Zanger_ID=@Zanger_ID";
                var ParNaam = new SqlParameter("@Naam", zanger.Naam);
                var ParVoornaam = new SqlParameter("@Voornaam", zanger.Voornaam);
                var ParArtiestenNaam = new SqlParameter("@ArtiestenNaam", zanger.ArtiestenNaam);
                var ParLand_ID = new SqlParameter("@Land_ID", zanger.Land_ID);
                var ParZanger_ID = new SqlParameter("@Zanger_ID", zanger.Zanger_ID);
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
                var sql = "DELETE FROM Zanger WHERE Zanger_ID=@Zanger_ID";
                var parZanger_ID = new SqlParameter("@Zanger_ID", Zanger_ID);
                Database.ExcecuteSQL(sql, parZanger_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class TaalDA
    {
        public static List<taal> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de taal in te plaatsen
            var LijstMetTaal = new List<taal>();

            //We maken het statement aan om de taal uit te lezen
            var sSql = "Select Taal_ID, Taal FROM dbo.Taal";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            var taalDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow taalDR in taalDT.Rows)
            {
                var taal = new taal();

                //hier vullen we de gegevens in in de aangemaakte klasse
                taal.Taal_ID = int.Parse(taalDR["Taal_ID"].ToString());
                taal.Taal = taalDR["Taal"].ToString();

                //hier voegen we de klasse toe aan de lijst van de album
                LijstMetTaal.Add(taal);
            }

            return LijstMetTaal;
        }
    }

    public class MuziekDA
    {
        public static List<muziek> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de muziek in te plaatsen
            var LijstMetMuziek = new List<muziek>();

            //We maken het statement aan om de album uit te lezen
            var sSql =
                "Select Muziek_ID, Liedje, Duur, Beoordeling, Taal_ID, Land_ID, Formaat_ID, Genre_ID, Album_ID FROM dbo.Muziek";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            var muziekDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow muziekDR in muziekDT.Rows)
            {
                var muziek = new muziek();

                //hier vullen we de gegevens in in de aangemaakte klasse
                muziek.Muziek_ID = int.Parse(muziekDR["Muziek_ID"].ToString());
                muziek.Liedje = muziekDR["Liedje"].ToString();
                muziek.Duur = muziekDR["Duur"].ToString();
                muziek.Beoordeling = int.Parse(muziekDR["Beoordeling"].ToString());
                muziek.Taal_ID = int.Parse(muziekDR["Taal_ID"].ToString());
                muziek.Land_ID = int.Parse(muziekDR["Land_ID"].ToString());
                muziek.Formaat_ID = int.Parse(muziekDR["Formaat_ID"].ToString());
                muziek.Genre_ID = int.Parse(muziekDR["Genre_ID"].ToString());
                muziek.Album_ID = int.Parse(muziekDR["Album_ID"].ToString());

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
                var sql =
                    "INSERT INTO Muziek (Liedje, Duur, Beoordeling, Taal_ID, Land_ID, Formaat_ID, Genre_ID, Album_ID) VALUES (@Liedje, @Duur, @Beoordeling, @Taal_ID, @Land_ID, @Formaat_ID, @Genre_ID, @Album_ID)";

                //hier maken we de parameters aan om de dingen te kunnen aanvullen.
                var ParLiedje = new SqlParameter("@Liedje", muziek.Liedje);
                var ParDuur = new SqlParameter("@Duur", muziek.Duur);
                var ParBeoordeling = new SqlParameter("@Beoordeling", muziek.Beoordeling);
                var ParTaal_ID = new SqlParameter("@Taal_ID", muziek.Taal_ID);
                var ParLand_ID = new SqlParameter("@Land_ID", muziek.Land_ID);
                var ParFormaat_ID = new SqlParameter("@Formaat_ID", muziek.Formaat_ID);
                var ParGenre_ID = new SqlParameter("@Genre_ID", muziek.Genre_ID);
                var ParAlbum_ID = new SqlParameter("@Album_ID", muziek.Album_ID);

                //hier sturen de opdracht naar de database
                Database.ExcecuteSQL(sql, ParLiedje, ParDuur, ParBeoordeling, ParTaal_ID, ParLand_ID, ParFormaat_ID,
                    ParGenre_ID, ParAlbum_ID);
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
                var sql =
                    "UPDATE Muziek SET Liedje=@Liedje, Duur=@Duur, Beoordeling=@Beoordeling, Taal_ID=@Taal_ID, Land_ID=@Land_ID, Formaat_ID=@Formaat_ID, Genre_ID=@Genre_ID, Album_ID=@Album_ID  WHERE Muziek_ID=@Muziek_ID";
                //hier maken we de parameters aan om de dingen te kunnen aanvullen
                var ParMuziek_ID = new SqlParameter("@Muziek_ID", muziek.Muziek_ID);
                var ParLiedje = new SqlParameter("@Liedje", muziek.Liedje);
                var ParDuur = new SqlParameter("@Duur", muziek.Duur);
                var ParBeoordeling = new SqlParameter("@Beoordeling", muziek.Beoordeling);
                var ParTaal_ID = new SqlParameter("@Taal_ID", muziek.Taal_ID);
                var ParLand_ID = new SqlParameter("@Land_ID", muziek.Land_ID);
                var ParFormaat_ID = new SqlParameter("@Formaat_ID", muziek.Formaat_ID);
                var ParGenre_ID = new SqlParameter("@Genre_ID", muziek.Genre_ID);
                var ParAlbum_ID = new SqlParameter("@Album_ID", muziek.Album_ID);

                //hier sturen de opdracht naar de database
                Database.ExcecuteSQL(sql, ParMuziek_ID, ParLiedje, ParDuur, ParBeoordeling, ParTaal_ID, ParLand_ID,
                    ParFormaat_ID, ParGenre_ID, ParAlbum_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteMuziek(int Muziek_ID)
        {
            try
            {
                //We maken het statement aan om muziek te verwijderen.
                var sql = "DELETE FROM Muziek WHERE Muziek_ID=@Muziek_ID";
                var parMuziek_ID = new SqlParameter("@Muziek_ID", Muziek_ID);
                Database.ExcecuteSQL(sql, parMuziek_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class Muziek_ZangerDA
    {
        public static List<Muziek_Zanger> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de muziek en zangers in te plaatsen
            var LijstMetMuziek_Zanger = new List<Muziek_Zanger>();

            //We maken het statement aan om de table uit te lezen
            var sSql = "Select Genre_ID, Muziek_ID, Zanger_ID FROM dbo.Muziek_Zanger";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            var Muziek_ZangerDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow Muziek_ZangerDR in Muziek_ZangerDT.Rows)
            {
                var muziek_zanger = new Muziek_Zanger();

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
                var sql =
                    "INSERT INTO Muziek_Zanger (Genre_ID, Muziek_ID, Zanger_ID) VALUES (@Genre_ID, @Muziek_ID, @Zanger_ID)";

                //hier maken we de parameters aan om de dingen te kunnen aanvullen
                var ParGenre_ID = new SqlParameter("@Genre_ID", _Muziek_Zanger.Genre_ID);
                var ParMuziek_ID = new SqlParameter("@Muziek_ID", _Muziek_Zanger.Muziek_ID);
                var ParZanger_ID = new SqlParameter("@Zanger_ID", _Muziek_Zanger.Zanger_ID);

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
                var sql =
                    "UPDATE Muziek_Zanger SET Genre_ID=@Genre_ID, Muziek_ID=@Muziek_ID, Zanger_ID=@Zanger_ID WHERE Genre_ID=@Genre_ID OR Muziek_ID=@Muziek_ID OR Zanger_ID=@Zanger_ID";
                var ParGenre_ID = new SqlParameter("@Genre_ID", _Muziek_Zanger.Genre_ID);
                var ParMuziek_ID = new SqlParameter("@Muziek_ID", _Muziek_Zanger.Muziek_ID);
                var ParZanger_ID = new SqlParameter("@Zanger_ID", _Muziek_Zanger.Zanger_ID);
                Database.ExcecuteSQL(sql, ParGenre_ID, ParMuziek_ID, ParZanger_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteMuziek_Zanger(int Genre_ID, int Muziek_ID)
        {
            try
            {
                //We maken het statement aan om de album te verwijderen.
                var sql = "DELETE FROM Muziek_Zanger WHERE Genre_ID=@Genre_ID AND Muziek_ID=@Muziek_ID";
                var ParGenre_ID = new SqlParameter("@Genre_ID", Genre_ID);
                var ParMuziek_ID = new SqlParameter("@Muziek_ID", Muziek_ID);
                Database.ExcecuteSQL(sql, ParGenre_ID, ParMuziek_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class LandDA
    {
        public static List<land> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de landen in te plaatsen
            var LijstMetLanden = new List<land>();

            //We maken het statement aan om de landen uit te lezen
            var sSql = "Select Land_ID, Land, Continent FROM dbo.Land";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            var LandDT = Database.GetDT(sSql);
            //Hier lezen we de datatabel uit met een foreacht
            foreach (DataRow LandDR in LandDT.Rows)
            {
                var land = new land();

                //hier vullen we de gegevens in in de aangemaakte klasse
                land.Land_ID = int.Parse(LandDR["Land_ID"].ToString());
                land.Land = LandDR["Land"].ToString();
                land.Continent = LandDR["Continent"].ToString();

                //hier voegen we de klasse toe aan de lijst van de landen
                LijstMetLanden.Add(land);
            }

            return LijstMetLanden;
        }
    }

    public class GenreDA
    {
        public static List<genre> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de genre in te plaatsen
            var LijstMetGenre = new List<genre>();

            //We maken het statement aan om de genre uit te lezen
            var sSql = "Select Genre_ID, Genre FROM dbo.Genre";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            var genreDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow genreDR in genreDT.Rows)
            {
                var genre = new genre();

                //hier vullen we de gegevens in in de aangemaakte klasse
                genre.Genre_ID = int.Parse(genreDR["Genre_ID"].ToString());
                genre.Genre = genreDR["Genre"].ToString();

                //hier voegen we de klasse toe aan de lijst van de genre
                LijstMetGenre.Add(genre);
            }

            return LijstMetGenre;
        }
    }

    public class FormaatDA
    {
        public static List<formaat> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor het formaat in te plaatsen
            var LijstMetFormaat = new List<formaat>();

            //We maken het statement aan om het formaat uit te lezen
            var sSql = "Select Formaat_ID, Formaat FROM dbo.Formaat";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            var formaatDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow formaatDR in formaatDT.Rows)
            {
                var formaat = new formaat();

                //hier vullen we de gegevens in in de aangemaakte klasse
                formaat.Formaat_ID = int.Parse(formaatDR["Formaat_ID"].ToString());
                formaat.Formaat = formaatDR["Formaat"].ToString();

                //hier voegen we de klasse toe aan de lijst van formaat
                LijstMetFormaat.Add(formaat);
            }

            return LijstMetFormaat;
        }
    }

    public class AlbumDA
    {
        public static List<album> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de album in te plaatsen
            var LijstMetAlbum = new List<album>();

            //We maken het statement aan om de album uit te lezen
            var sSql = "Select Album_ID, Album FROM dbo.Album";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            var albumDT = Database.GetDT(sSql);

            //Hier lezen we de datatabel uit met een foreach
            foreach (DataRow albumDR in albumDT.Rows)
            {
                var album = new album();

                //hier vullen we de gegevens in in de aangemaakte klasse
                album.album_ID = int.Parse(albumDR["Album_ID"].ToString());
                album.Album = albumDR["Album"].ToString();

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
                var sql = "INSERT INTO Album (Album) VALUES (@Album) ";

                //hier maken we de parameters aan om de dingen te kunnen aanvullen
                var ParAlbum = new SqlParameter("@Album", album.Album);

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
                var sql = "UPDATE Album SET Album=@Album WHERE Album_ID=@Album_ID";
                var ParAlbum = new SqlParameter("@Album", album.Album);
                var ParAlbum_ID = new SqlParameter("@Album_ID", album.album_ID);
                Database.ExcecuteSQL(sql, ParAlbum, ParAlbum_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteAlbum(int Album_ID)
        {
            try
            {
                //We maken het statement aan om de album te verwijderen.
                var sql = "DELETE FROM Album WHERE Album_ID=@Album_ID";
                var parAlbum_ID = new SqlParameter("@Album_ID", Album_ID);
                Database.ExcecuteSQL(sql, parAlbum_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class AccountsDA
    {
        public static List<accounts> HaalGegevensOp()
        {
            //het uitlezen van de database
            //we maken een lijst aan voor de accounts in te plaatsen
            var LijstMetAccounts = new List<accounts>();

            //We maken het statement aan om de landen uit te lezen
            var sSql = "Select Account_ID, Naam, Wachtwoord FROM dbo.Accounts";

            //hier gaan we de verschillende dingen ophalen uit de database
            //we plaatsen dit in een datatabel
            var AccountDT = Database.GetDT(sSql);
            //Hier lezen we de datatabel uit met een foreacht
            foreach (DataRow AccountDR in AccountDT.Rows)
            {
                var account = new accounts();

                //hier vullen we de gegevens in in de aangemaakte klasse
                account.Account_ID = int.Parse(AccountDR["Account_ID"].ToString());
                account.Naam = AccountDR["Naam"].ToString();
                account.Wachtwoord = AccountDR["Wachtwoord"].ToString();

                //hier voegen we de klasse toe aan de lijst van de accounts
                LijstMetAccounts.Add(account);
            }

            return LijstMetAccounts;
        }

        public static bool voegAccountToe(accounts account)
        {
            try
            {
                //hier geven we de sql string op
                var sql = "INSERT INTO Accounts (Naam, Wachtwoord) VALUES (@Naam, @Wachtwoord)";

                //hier maken we de parameters aan om de gegevens te kunnen invullen.
                var ParNaam = new SqlParameter("@Naam", account.Naam);
                var ParWachtwoord = new SqlParameter("@Wachtwoord", account.Wachtwoord);

                //hier sturen de opdracht naar de database
                Database.ExcecuteSQL(sql, ParNaam, ParWachtwoord);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteAccount(int Account_ID)
        {
            try
            {
                //We maken het statement aan om het account te verwijderen.
                var sql = "DELETE FROM Accounts WHERE Account_ID=@Account_ID";
                var parAccount_ID = new SqlParameter("@Account_ID", Account_ID);
                Database.ExcecuteSQL(sql, parAccount_ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}