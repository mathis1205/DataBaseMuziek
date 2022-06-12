using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBaseMuziek
{
    /// <summary>
    /// Interaction logic for Muziek.xaml
    /// </summary>
    public partial class Muziek : Window
    {
        public Muziek()
        {
            InitializeComponent();
            ComboboxenEnListenInvullen();
            ListboxInvullen();
        }

        //Listen aanmaken.
        List<taal> LijstMetTalen = new List<taal>();
        List<land> LijstMetLanden = new List<land>();
        List<formaat> LijstMetFormaten = new List<formaat>();
        List<genre> LijstMetGenres = new List<genre>();
        List<album> LijstMetAlbums = new List<album>();
        List<muziek> LijstMetMuziek = new List<muziek>();
        List<zanger> LijstMetZangers = new List<zanger>();

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
            foreach(taal _taal in LijstMetTalen)
            {
                cmbTaal.Items.Add(_taal.Taal);
            }
            foreach (land _land in LijstMetLanden)            
            {                
                cmbLand.Items.Add(_land.Land);
            }         
            foreach (formaat _formaat in LijstMetFormaten)
            {               
                cmbFormaat.Items.Add(_formaat.Formaat);
            }
            foreach (genre _genre in LijstMetGenres)
            {
                cmbGenre.Items.Add(_genre.Genre);
            }            
            foreach (album _album in LijstMetAlbums)
            {
                cmbAlbum.Items.Add(_album.Album);
            }
            foreach (zanger _zanger in LijstMetZangers)
            {
                cmbZanger.Items.Add(_zanger.Voornaam);
            }            
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
            cmbZanger.Text = txbBeoordeling.Text = txbDuur.Text = txbLiedje.Text = cmbAlbum.Text = cmbFormaat.Text = cmbGenre.Text = cmbLand.Text = cmbTaal.Text = "";
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {
                //Controle op invoer.
                if(txbLiedje.Text != "" && txbDuur.Text != "" && txbBeoordeling.Text != "")
                {
                    //Controle op selectie.
                    if(cmbAlbum.SelectedIndex != -1 && cmbFormaat.SelectedIndex != -1 && cmbGenre.SelectedIndex != -1 && cmbLand.SelectedIndex != -1 && cmbTaal.SelectedIndex != -1)
                    {
                        //Controleren of er een cijfer wordt in gegeven.
                        int nmr;
                        bool testNr = Int32.TryParse(txbBeoordeling.Text, out nmr);
                        if (testNr == true)
                        {
                            //Controleren of het getal tussen 0 en 10 is.                            
                            int a = int.Parse(txbBeoordeling.Text);
                            if (a <= 10 && a >= 0)
                            {
                                //Klasses aanmaken.
                                muziek _muziek = new muziek();
                                Muziek_Zanger muziek_Zanger = new Muziek_Zanger();

                                //Klasse variabelen invullen.
                                _muziek.Liedje = txbLiedje.Text;
                                _muziek.Duur = txbDuur.Text;
                                _muziek.Beoordeling = int.Parse(txbBeoordeling.Text);
                                _muziek.Taal_ID = LijstMetTalen[cmbTaal.SelectedIndex].Taal_ID;
                                _muziek.Land_ID = LijstMetLanden[cmbLand.SelectedIndex].Land_ID;
                                _muziek.Formaat_ID = LijstMetFormaten[cmbFormaat.SelectedIndex].Formaat_ID;
                                muziek_Zanger.Genre_ID = _muziek.Genre_ID = LijstMetGenres[cmbGenre.SelectedIndex].Genre_ID;
                                _muziek.Album_ID = LijstMetAlbums[cmbAlbum.SelectedIndex].album_ID;
                                muziek_Zanger.Zanger_ID = LijstMetZangers[cmbZanger.SelectedIndex].Zanger_ID;

                                //Zoeken naar laatste ID voor het juiste ID mee te geven.
                                muziek_Zanger.Muziek_ID = LijstMetMuziek[LijstMetMuziek.Count -1].Muziek_ID + 1; 
                                
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
                                MessageBox.Show("Het cijfer bij beoordeling moet tussen 0 en 10 zijn, gelieve een correct cijfer in te geven.", "Foutieve invoer", MessageBoxButton.OK, MessageBoxImage.Warning);

                                //Textbox terug leegmaken.
                                txbBeoordeling.Text = "";
                            }
                        }
                        else
                        {
                            //Melding als er geen cijfer is ingevuld.
                            MessageBox.Show("U heeft geen cijfer ingevuld bij beoordeling, gelieve een cijfer in te vullen.", "Foutieve invoer", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        //Melding tonen wanneer niet alles geselecteerd is.
                        MessageBox.Show("U heeft niet alles geselecteerd, gelieve alles te selecteren.", "Geen selectie", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    //Melding tonen wanneer niet alles is ingevuld.
                    MessageBox.Show("U heeft niet alles ingevuld, gelieve alles in te vullen.", "Geen invoer", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    var check = MessageBox.Show("Bent u zeker dat u deze gegevens wilt verwijderen?", "Bent u zeker?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    //Controleren of de gebruiker zeker is.
                    if (check == MessageBoxResult.Yes)
                    {
                        //Geselecteerde item verwijderen.
                        MuziekDA.DeleteMuziek(LijstMetMuziek[lsbMuziek.SelectedIndex].Muziek_ID);
                        Muziek_ZangerDA.DeleteMuziek_Zanger(LijstMetMuziek[lsbMuziek.SelectedIndex].Muziek_ID, LijstMetMuziek[lsbMuziek.SelectedIndex].Genre_ID);

                        //Scherm updaten.
                        LijstMetMuziek.Clear();
                        WpfUpdaten();
                    }
                }
                else
                {
                    //Melding tonen dat er niet is geselecteerd..
                    MessageBox.Show("U heeft niet geselecteerd in de lijst, gelieve iets te selecteren.", "Geen selectie", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                int a = int.Parse(txbBeoordeling.Text);
                if (a <= 10 && a >= 0)
                {
                    //Vragen aan de gebruiker of ze zeker zijn van hun keuze.
                    var check = MessageBox.Show("Bent u zeker dat u deze gegevens wilt wijzigen?", "Bent u zeker?", MessageBoxButton.YesNo);

                    //Controleren of de gebruiker zeker is.
                    if (check == MessageBoxResult.Yes)
                    {
                        //Klasses aanmaken.
                        muziek _muziek = new muziek();
                        Muziek_Zanger muziek_Zanger = new Muziek_Zanger();

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
                    MessageBox.Show("Het cijfer bij beoordeling moet tussen 0 en 10 zijn, gelieve een correct cijfer in te geven.", "Foutieve invoer", MessageBoxButton.OK, MessageBoxImage.Warning);

                    //Textbox terug leegmaken.
                    txbBeoordeling.Text = "";
                }
            }
            //Error tonen wanneer er iets niet klopt.
            catch(Exception error)
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
                foreach (taal _taal in LijstMetTalen)
                {
                    if (_taal.Taal_ID == LijstMetMuziek[lsbMuziek.SelectedIndex].Taal_ID)
                    {
                        cmbTaal.Text = _taal.Taal;
                    }
                }
                foreach (land _land in LijstMetLanden)
                {
                    if (_land.Land_ID == LijstMetMuziek[lsbMuziek.SelectedIndex].Land_ID)
                    {
                        cmbLand.Text = _land.Land;
                    }
                }
                foreach (formaat _formaat in LijstMetFormaten)
                {
                    if (_formaat.Formaat_ID == LijstMetMuziek[lsbMuziek.SelectedIndex].Formaat_ID)
                    {
                        cmbFormaat.Text = _formaat.Formaat;
                    }
                }
                foreach (genre _genre in LijstMetGenres)
                {
                    if (_genre.Genre_ID == LijstMetMuziek[lsbMuziek.SelectedIndex].Genre_ID)
                    {
                        cmbGenre.Text = _genre.Genre;
                    }
                }
                foreach (album _album in LijstMetAlbums)
                {
                    if (_album.album_ID == LijstMetMuziek[lsbMuziek.SelectedIndex].Album_ID)
                    {
                        cmbAlbum.Text = _album.Album;
                    }
                }
                foreach (zanger _zanger in LijstMetZangers)
                {
                    if (_zanger.Zanger_ID == LijstMetZangers[lsbMuziek.SelectedIndex].Zanger_ID)
                    {
                        cmbZanger.Text = _zanger.Voornaam;
                    }
                }
            }
        }

        private void btnAlbum_Click(object sender, RoutedEventArgs e)
        {
            //Scherm aanmaken en tonen.
            Album album = new Album();
            album.Show();

            //Huidig scherm sluiten.
            this.Close();
        }

        private void btnZanger_Click(object sender, RoutedEventArgs e)
        {
            //Scherm aanmaken en tonen.
            Zanger zanger = new Zanger();
            zanger.Show();

            //Huidig scherm sluiten.
            this.Close();
        }
    }
}
