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
    /// Interaction logic for Overzicht.xaml
    /// </summary>
    public partial class Overzicht : Window
    {
        public Overzicht()
        {
            InitializeComponent();
        }

        private void btnAlbum_Copy_Click(object sender, RoutedEventArgs e)
        {
            //Nieuw scherm aanmaken en tonen.
            Album album = new Album();
            album.Show();

            //Huidig scherm sluiten.
            this.Close();
        }

        private void btnMuziek_Click(object sender, RoutedEventArgs e)
        {
            //Nieuw scherm aanmaken en tonen.
            Muziek muziek = new Muziek();
            muziek.Show();

            //Huidig scherm sluiten.
            this.Close();
        }

        private void btnZanger_Click(object sender, RoutedEventArgs e)
        {
            //Nieuw scherm aanmaken en tonen.
            Zanger zanger = new Zanger();
            zanger.Show();

            //Huidig scherm sluiten.
            this.Close();
        }
    }
}
