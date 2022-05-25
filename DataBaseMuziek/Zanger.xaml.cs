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
    /// Interaction logic for Zanger.xaml
    /// </summary>
    public partial class Zanger : Window
    {
        public Zanger()
        {
            InitializeComponent();
            ListboxInvullen();

            //Combobox invullen
            foreach (var item in LandDA.HaalGegevensOp())
            {
                cmbLand.Items.Add(item.Land);
            }
        }

        //Listen aanmaken.
        List<zanger> LijstMetZangers = new List<zanger>();
        List<land> LijstMetLanden = new List<land>();

        //Listbox invullen.
        private void ListboxInvullen()
        {
            //Listbox leegmaken.
            lsbZangers.Items.Clear();

            //List invullen.
            LijstMetLanden = LandDA.HaalGegevensOp();
            
            foreach (var item in ZangerDA.HaalGegevensOp())
            {
                //List invullen.
                LijstMetZangers.Add(item);

                //Listbox invullen.
                lsbZangers.Items.Add($"{item.Voornaam}  {item.Naam}  {item.ArtiestenNaam}");
            }
        }

        private void WpfUpdaten()
        {
            //WPF updaten.
            ListboxInvullen();
            txbArtiestennaam.Text = txbNaam.Text = txbVoornaam.Text = "";
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {
                //Controle op invoer.
                if (txbVoornaam.Text != "" && txbNaam.Text != "" && txbArtiestennaam.Text != "")
                {
                    //Klasse aanmaken.
                    zanger _zanger = new zanger();

                    //Klasse variabelen invullen.
                    _zanger.Naam = txbNaam.Text;
                    _zanger.Voornaam = txbVoornaam.Text;
                    _zanger.ArtiestenNaam = txbArtiestennaam.Text;
                    _zanger.Land_ID = LijstMetLanden[cmbLand.SelectedIndex].Land_ID;

                    //Gegevens meegeven met de database.
                    ZangerDA.voegZangerToe(_zanger);

                    //Scherm updaten.
                    WpfUpdaten();
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
        private void btnWijzigen_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {
                //Vragen aan de gebruiker of ze zeker zijn van hun keuze.
                var check = MessageBox.Show("Bent u zeker dat u deze gegevens wilt wijzigen?", "Bent u zeker?", MessageBoxButton.YesNo);

                //Controleren of de gebruiker zeker is.
                if (check == MessageBoxResult.Yes)
                {
                    //Klasse aanmaken.
                    zanger _zanger = new zanger();

                    //Klasse variabelen invullen.
                    _zanger.Naam = txbNaam.Text;
                    _zanger.Voornaam = txbVoornaam.Text;
                    _zanger.ArtiestenNaam = txbArtiestennaam.Text;
                    _zanger.Land_ID = LijstMetLanden[cmbLand.SelectedIndex].Land_ID;

                    //Geselecteerde gegevens aanpassen.
                    ZangerDA.Wijzigzanger(_zanger);

                    //Scherm updaten.
                    WpfUpdaten();
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
                if (lsbZangers.SelectedIndex != -1)
                {
                    //Vragen of de gebruiker zeker is van zijn keuze.
                    var check = MessageBox.Show("Bent u zeker dat u deze gegevens wilt verwijderen?", "Bent u zeker?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    //Controleren of de gebruiker zeker is.
                    if (check == MessageBoxResult.Yes)
                    {
                        //Geselecteerde item verwijderen.
                        ZangerDA.Deletezanger(LijstMetZangers[lsbZangers.SelectedIndex].Zanger_ID);

                        //Scherm updaten.
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

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            //Scherm aanmaken en tonen.
            Overzicht overzicht = new Overzicht();
            overzicht.Show();

            //Huidig scherm sluiten.
            this.Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Controleren of er iets is geselecteerd.
            if (lsbZangers.SelectedIndex != -1)
            {
                //Textboxen invullen.
                txbNaam.Text = LijstMetZangers[lsbZangers.SelectedIndex].Naam;
                txbVoornaam.Text = LijstMetZangers[lsbZangers.SelectedIndex].Voornaam;
                txbArtiestennaam.Text = LijstMetZangers[lsbZangers.SelectedIndex].ArtiestenNaam;
                foreach (land _land in LijstMetLanden)
                {
                    if (_land.Land_ID == LijstMetZangers[lsbZangers.SelectedIndex].Land_ID)
                    {
                        cmbLand.Text = _land.Land;
                    }
                }
            }
        }
    }
}
