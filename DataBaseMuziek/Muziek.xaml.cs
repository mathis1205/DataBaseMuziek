using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DataBaseMuziek
{
    /// <summary>
    ///     Interaction logic for Muziek.xaml
    /// </summary>
    public partial class Muziek : Window
    {
        private List<album> LijstMetAlbums = new List<album>();
        private List<formaat> LijstMetFormaten = new List<formaat>();
        private List<genre> LijstMetGenres = new List<genre>();
        private List<land> LijstMetLanden = new List<land>();
        private readonly List<muziek> LijstMetMuziek = new List<muziek>();

        //Listen aanmaken.
        private List<taal> LijstMetTalen = new List<taal>();
        private List<zanger> LijstMetZangers = new List<zanger>();

        public Muziek()
        {
            InitializeComponent();
            ComboboxenEnListenInvullen();
            ListboxInvullen();
        }

        private void ComboboxenEnListenInvullen()
        {
            //Lijsten invullen.
            LijstMetFormaten = FormaatDA.HaalGegevensOp();
            LijstMetGenres = GenreDA.HaalGegevensOp();
            LijstMetAlbums = AlbumDA.HaalGegevensOp();
            LijstMetLanden = LandDA.HaalGegevensOp();
            LijstMetTalen = TaalDA.HaalGegevensOp();
            LijstMetZangers = ZangerDA.HaalGegevensOp();

            //Comboboxen invullen.
            foreach (var _taal in LijstMetTalen) cmbTaal.Items.Add(_taal.Taal);
            foreach (var _land in LijstMetLanden) cmbLand.Items.Add(_land.Land);
            foreach (var _formaat in LijstMetFormaten) cmbFormaat.Items.Add(_formaat.Formaat);
            foreach (var _genre in LijstMetGenres) cmbGenre.Items.Add(_genre.Genre);
            foreach (var _album in LijstMetAlbums) cmbAlbum.Items.Add(_album.Album);
            foreach (var _zanger in LijstMetZangers) cmbZanger.Items.Add(_zanger.Voornaam);
        }

        private void ListboxInvullen()
        {
            //Listbox leegmaken.
            lsbMuziek.Items.Clear();

            //Listbox invullen.
            foreach (var item in MuziekDA.HaalGegevensOp())
            {
                LijstMetMuziek.Add(item);
                lsbMuziek.Items.Add($"{item.Liedje}");
            }
        }

        private void WpfUpdaten()
        {
            //WPF updaten.
            ListboxInvullen();
            cmbZanger.Text = txbBeoordeling.Text = txbDuur.Text = txbLiedje.Text =
                cmbAlbum.Text = cmbFormaat.Text = cmbGenre.Text = cmbLand.Text = cmbTaal.Text = "";
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {
                //Controle op invoer.
                if (txbLiedje.Text != "" && txbDuur.Text != "" && txbBeoordeling.Text != "")
                {
                    //Controle op selectie.
                    if (cmbAlbum.SelectedIndex != -1 && cmbFormaat.SelectedIndex != -1 &&
                        cmbGenre.SelectedIndex != -1 && cmbLand.SelectedIndex != -1 && cmbTaal.SelectedIndex != -1)
                    {
                        //Controleren of er een cijfer wordt in gegeven.
                        int nmr;
                        var testNr = int.TryParse(txbBeoordeling.Text, out nmr);
                        if (testNr)
                        {
                            //Controleren of het getal tussen 0 en 10 is.                            
                            var a = int.Parse(txbBeoordeling.Text);
                            if (a <= 10 && a >= 0)
                            {
                                //Klasses aanmaken.
                                var _muziek = new muziek();
                                var muziek_Zanger = new Muziek_Zanger();

                                //Klasse variabelen invullen.
                                _muziek.Liedje = txbLiedje.Text;
                                _muziek.Duur = txbDuur.Text;
                                _muziek.Beoordeling = int.Parse(txbBeoordeling.Text);
                                _muziek.Taal_ID = LijstMetTalen[cmbTaal.SelectedIndex].Taal_ID;
                                _muziek.Land_ID = LijstMetLanden[cmbLand.SelectedIndex].Land_ID;
                                _muziek.Formaat_ID = LijstMetFormaten[cmbFormaat.SelectedIndex].Formaat_ID;
                                muziek_Zanger.Genre_ID =
                                    _muziek.Genre_ID = LijstMetGenres[cmbGenre.SelectedIndex].Genre_ID;
                                _muziek.Album_ID = LijstMetAlbums[cmbAlbum.SelectedIndex].album_ID;
                                muziek_Zanger.Zanger_ID = LijstMetZangers[cmbZanger.SelectedIndex].Zanger_ID;

                                //Zoeken naar laatste ID voor het juiste ID mee te geven.
                                muziek_Zanger.Muziek_ID = LijstMetMuziek[LijstMetMuziek.Count - 1].Muziek_ID + 1;

                                //Gegevens meegeven met de database.
                                MuziekDA.voegMuziekToe(_muziek);
                                Muziek_ZangerDA.voegMuziek_ZangerToe(muziek_Zanger);

                                //Scherm updaten.
                                LijstMetMuziek.Clear();
                                WpfUpdaten();
                            }
                            else
                            {
                                //Melding als het cijfer niet tussen 0 en 10 is.
                                MessageBox.Show(
                                    "Het cijfer bij beoordeling moet tussen 0 en 10 zijn, gelieve een correct cijfer in te geven.",
                                    "Foutieve invoer", MessageBoxButton.OK, MessageBoxImage.Warning);

                                //Textbox terug leegmaken.
                                txbBeoordeling.Text = "";
                            }
                        }
                        else
                        {
                            //Melding als er geen cijfer is ingevuld.
                            MessageBox.Show(
                                "U heeft geen cijfer ingevuld bij beoordeling, gelieve een cijfer in te vullen.",
                                "Foutieve invoer", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        //Melding tonen wanneer niet alles geselecteerd is.
                        MessageBox.Show("U heeft niet alles geselecteerd, gelieve alles te selecteren.",
                            "Geen selectie", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    //Melding tonen wanneer niet alles is ingevuld.
                    MessageBox.Show("U heeft niet alles ingevuld, gelieve alles in te vullen.", "Geen invoer",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            //Error tonen wanneer er iets niet klopt.
            catch (Exception error)
            {
                //Error tonen.
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {
                //Controleren of er iets is geselecteerd in de listbox.
                if (lsbMuziek.SelectedIndex != -1)
                {
                    //Vragen of de gebruiker zeker is van zijn keuze.
                    var check = MessageBox.Show("Bent u zeker dat u deze gegevens wilt verwijderen?", "Bent u zeker?",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);

                    //Controleren of de gebruiker zeker is.
                    if (check == MessageBoxResult.Yes)
                    {
                        //Geselecteerde item verwijderen.
                        MuziekDA.DeleteMuziek(LijstMetMuziek[lsbMuziek.SelectedIndex].Muziek_ID);
                        Muziek_ZangerDA.DeleteMuziek_Zanger(LijstMetMuziek[lsbMuziek.SelectedIndex].Muziek_ID,
                            LijstMetMuziek[lsbMuziek.SelectedIndex].Genre_ID);

                        //Scherm updaten.
                        LijstMetMuziek.Clear();
                        WpfUpdaten();
                    }
                }
                else
                {
                    //Melding tonen dat er niet is geselecteerd..
                    MessageBox.Show("U heeft niet geselecteerd in de lijst, gelieve iets te selecteren.",
                        "Geen selectie", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            //Error tonen wanneer er iets niet klopt.
            catch (Exception error)
            {
                //Error tonen.
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnWijzigen_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {
                //Controleren of het getal tussen 0 en 10 is.
                var a = int.Parse(txbBeoordeling.Text);
                if (a <= 10 && a >= 0)
                {
                    //Vragen aan de gebruiker of ze zeker zijn van hun keuze.
                    var check = MessageBox.Show("Bent u zeker dat u deze gegevens wilt wijzigen?", "Bent u zeker?",
                        MessageBoxButton.YesNo);

                    //Controleren of de gebruiker zeker is.
                    if (check == MessageBoxResult.Yes)
                    {
                        //Klasses aanmaken.
                        var _muziek = new muziek();
                        var muziek_Zanger = new Muziek_Zanger();

                        //Klasse variabelen invullen.
                        _muziek.Muziek_ID = LijstMetMuziek[lsbMuziek.SelectedIndex].Muziek_ID;
                        _muziek.Liedje = txbLiedje.Text;
                        _muziek.Duur = txbDuur.Text;
                        _muziek.Beoordeling = int.Parse(txbBeoordeling.Text);
                        _muziek.Taal_ID = LijstMetTalen[cmbTaal.SelectedIndex].Taal_ID;
                        _muziek.Land_ID = LijstMetLanden[cmbLand.SelectedIndex].Land_ID;
                        _muziek.Formaat_ID = LijstMetFormaten[cmbFormaat.SelectedIndex].Formaat_ID;
                        muziek_Zanger.Genre_ID = _muziek.Genre_ID = LijstMetGenres[cmbGenre.SelectedIndex].Genre_ID;
                        _muziek.Album_ID = LijstMetAlbums[cmbAlbum.SelectedIndex].album_ID;
                        muziek_Zanger.Zanger_ID = LijstMetZangers[cmbZanger.SelectedIndex].Zanger_ID;

                        //Geselecteerde gegevens aanpassen.
                        MuziekDA.WijzigMuziek(_muziek);
                        Muziek_ZangerDA.WijzigMuziek_Zanger(muziek_Zanger);

                        //Scherm updaten.
                        LijstMetMuziek.Clear();
                        WpfUpdaten();
                    }
                }
                else
                {
                    //Melding als het cijfer niet tussen 0 en 10 is.
                    MessageBox.Show(
                        "Het cijfer bij beoordeling moet tussen 0 en 10 zijn, gelieve een correct cijfer in te geven.",
                        "Foutieve invoer", MessageBoxButton.OK, MessageBoxImage.Warning);

                    //Textbox terug leegmaken.
                    txbBeoordeling.Text = "";
                }
            }
            //Error tonen wanneer er iets niet klopt.
            catch (Exception error)
            {
                //Error tonen.
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lsbMuziek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Controleren of er iets is geselecteerd.
            if (lsbMuziek.SelectedIndex != -1)
            {
                //Text-comboboxen invullen.
                txbLiedje.Text = LijstMetMuziek[lsbMuziek.SelectedIndex].Liedje;
                txbDuur.Text = LijstMetMuziek[lsbMuziek.SelectedIndex].Duur;
                txbBeoordeling.Text = $"{LijstMetMuziek[lsbMuziek.SelectedIndex].Beoordeling}";
                foreach (var _taal in LijstMetTalen)
                    if (_taal.Taal_ID == LijstMetMuziek[lsbMuziek.SelectedIndex].Taal_ID)
                        cmbTaal.Text = _taal.Taal;
                foreach (var _land in LijstMetLanden)
                    if (_land.Land_ID == LijstMetMuziek[lsbMuziek.SelectedIndex].Land_ID)
                        cmbLand.Text = _land.Land;
                foreach (var _formaat in LijstMetFormaten)
                    if (_formaat.Formaat_ID == LijstMetMuziek[lsbMuziek.SelectedIndex].Formaat_ID)
                        cmbFormaat.Text = _formaat.Formaat;
                foreach (var _genre in LijstMetGenres)
                    if (_genre.Genre_ID == LijstMetMuziek[lsbMuziek.SelectedIndex].Genre_ID)
                        cmbGenre.Text = _genre.Genre;
                foreach (var _album in LijstMetAlbums)
                    if (_album.album_ID == LijstMetMuziek[lsbMuziek.SelectedIndex].Album_ID)
                        cmbAlbum.Text = _album.Album;
                foreach (var _zanger in LijstMetZangers)
                    if (_zanger.Zanger_ID == LijstMetZangers[lsbMuziek.SelectedIndex].Zanger_ID)
                        cmbZanger.Text = _zanger.Voornaam;
            }
        }

        private void btnAlbum_Click(object sender, RoutedEventArgs e)
        {
            //Scherm aanmaken en tonen.
            var album = new Album();
            album.Show();

            //Huidig scherm sluiten.
            Close();
        }

        private void btnZanger_Click(object sender, RoutedEventArgs e)
        {
            //Scherm aanmaken en tonen.
            var zanger = new Zanger();
            zanger.Show();

            //Huidig scherm sluiten.
            Close();
        }

        private void btnUitloggen_Click(object sender, RoutedEventArgs e)
        {
            //Vragen of ze zeker zijn dat ze willen uitloggen.
            var mess = MessageBox.Show("U bent zeker dat u wilt uitloggen?", "Uitloggen", MessageBoxButton.YesNo);
            //Wanneer er op JA wordt geklikt wordt de gebruiker uitgelogd.
            if (mess == MessageBoxResult.Yes)
            {
                //Terug naar login scherm.
                var login = new Login();
                login.Show();
                Close();
            }
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            //Accountscherm tonen.
            var account = new Account();
            account.Show();
            Close();
        }
    }
}