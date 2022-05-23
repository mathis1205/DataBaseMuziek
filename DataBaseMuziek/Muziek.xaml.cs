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

            LijstMetMuziek = MuziekDA.HaalGegevensOp();
            foreach (muziek _muziek in LijstMetMuziek)
            {
                lsbMuziek.Items.Add($"{_muziek.Liedje}  {_muziek.Duur}  {_muziek.Beoordeling}  {_muziek.TaalID}  {_muziek.Liedje}  {_muziek.FormaatID}  {_muziek.GenreID}  {_muziek.AlbumID}");
            }            
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {

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
                
            }
            //Error tonen wanneer er iets niet klopt.
            catch(Exception error)
            {
                //Error tonen.
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

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
