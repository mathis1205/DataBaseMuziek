﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DataBaseMuziek
{
    /// <summary>
    ///     Interaction logic for Album.xaml
    /// </summary>
    public partial class Album : Window
    {
        //List aanmaken.
        private readonly List<album> LijstMetAlbums = new List<album>();

        public Album()
        {
            InitializeComponent();
            ListboxInvullen();
        }

        private void ListboxInvullen()
        {
            //Listbox leegmaken.
            lsbAlbums.Items.Clear();

            //List leegmaken.
            LijstMetAlbums.Clear();

            //Listbox invullen.
            foreach (var item in AlbumDA.HaalGegevensOp())
            {
                LijstMetAlbums.Add(item);
                lsbAlbums.Items.Add($"{item.Album}");
            }
        }

        private void WpfUpdaten()
        {
            //Listbox invullen.
            ListboxInvullen();

            //Textbox leegmaken.
            txbAlbum.Text = "";
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {
                //Controleren of er iets is ingevuld.
                if (txbAlbum.Text != "")
                {
                    //Klasse aanmaken.
                    var _album = new album();

                    //Klasse variabelen invullen.
                    _album.Album = txbAlbum.Text;

                    //Gegevens meegeven met de database.
                    AlbumDA.voegAlbumToe(_album);

                    //Scherm updaten.
                    WpfUpdaten();
                }
                else
                {
                    //Melding tonen wanneer niet alles is ingevuld.
                    MessageBox.Show("U heeft geen album ingevuld, gelieve alles in te vullen.", "Geen invoer",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            //Melding tonen wanneer er iets niet klopt.
            catch (Exception error)
            {
                //Melding tonen.
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnWijzigen_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {
                //Vragen aan de gebruiker of ze zeker zijn van hun keuze.
                var check = MessageBox.Show("Bent u zeker dat u deze gegevens wilt wijzigen?", "Bent u zeker?",
                    MessageBoxButton.YesNo);

                //Controleren of de gebruiker zeker is.
                if (check == MessageBoxResult.Yes)
                {
                    //Klasse aanmaken.
                    var _album = new album();

                    //Klasse variabelen invullen.
                    _album.Album = txbAlbum.Text;
                    _album.album_ID = LijstMetAlbums[lsbAlbums.SelectedIndex].album_ID;

                    //Gegevens meegeven met de database.
                    AlbumDA.WijzigAlbum(_album);

                    //Scherm updaten.
                    WpfUpdaten();
                }
            }
            //Melding tonen wanneer er iets niet klopt.
            catch (Exception error)
            {
                //Melding tonen.
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lsbAlbums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Controleren of er iets is geselecteerd.
            if (lsbAlbums.SelectedIndex != -1)
                //Textbox invullen met geselecteerde album.
                txbAlbum.Text = LijstMetAlbums[lsbAlbums.SelectedIndex].Album;
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {
                //Controleren of er iets is geselecteerd in de listbox.
                if (lsbAlbums.SelectedIndex != -1)
                {
                    //Vragen of de gebruiker zeker is van zijn keuze.
                    var check = MessageBox.Show($"Bent u zeker dat u {txbAlbum.Text} wilt verwijderen?",
                        "Bent u zeker?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    //Controleren of de gebruiker zeker is.
                    if (check == MessageBoxResult.Yes)
                    {
                        //Geselecteerde item verwijderen.
                        AlbumDA.DeleteAlbum(LijstMetAlbums[lsbAlbums.SelectedIndex].album_ID);

                        //Scherm updaten.
                        WpfUpdaten();
                    }
                }
            }
            //Melding tonen wanneer er iets niet klopt.
            catch (Exception error)
            {
                //Melding tonen.
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            //Nieuw scherm aanmaken en tonen.
            var muziek = new Muziek();
            muziek.Show();

            //Huidig scherm sluiten.
            Close();
        }
    }
}