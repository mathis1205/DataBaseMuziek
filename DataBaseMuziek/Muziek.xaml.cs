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
        List<genre> LijstMetGernes = new List<genre>();
        List<album> LijstMetAlbums = new List<album>();
        List<muziek> LijstMetMuziek = new List<muziek>();

        private void ComboboxenEnListenInvullen()
        {
            //Lijsten invullen.
            LijstMetFormaten = FormaatDA.HaalGegevensOp();
            LijstMetGernes = GenreDA.HaalGegevensOp();
            LijstMetAlbums = AlbumDA.HaalGegevensOp();
            LijstMetLanden = LandDA.HaalGegevensOp();
            LijstMetTalen = TaalDA.HaalGegevensOp();

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
            foreach (genre _genre in LijstMetGernes)
            {
                cmbGenre.Items.Add(_genre.Genre);
            }            
            foreach (album _album in LijstMetAlbums)
            {
                cmbAlbum.Items.Add(_album.Album);
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
            txbBeoordeling.Text = txbDuur.Text = txbLiedje.Text = cmbAlbum.Text = cmbFormaat.Text = cmbGenre.Text = cmbLand.Text = cmbTaal.Text = "";
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
                        //Klasse aanmaken.
                        muziek _muziek = new muziek();
                        _muziek.Liedje = txbLiedje.Text;
                        _muziek.Duur = txbDuur.Text;
                        _muziek.Beoordeling = txbBeoordeling.Text;
                        _muziek.TaalID = LijstMetTalen[cmbTaal.SelectedIndex].TaalID;
                        _muziek.LandID = LijstMetLanden[cmbLand.SelectedIndex].LandID;
                        _muziek.FormaatID = LijstMetFormaten[cmbFormaat.SelectedIndex].FormaatID;
                        _muziek.GenreID = LijstMetGernes[cmbGenre.SelectedIndex].GenreID;
                        _muziek.AlbumID = LijstMetAlbums[cmbAlbum.SelectedIndex].albumID;

                        WpfUpdaten();
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
                WpfUpdaten();
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
                WpfUpdaten();
            }
            //Error tonen wanneer er iets niet klopt.
            catch(Exception error)
            {
                //Error tonen.
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
