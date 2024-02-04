using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace DataBaseMuziek
{
    /// <summary>
    ///     Interaction logic for StartScherm.xaml
    /// </summary>
    public partial class StartScherm : Window
    {
        //Variabele aanmaken.
        private int i;

        //Timer aanmaken.
        private readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Normal);

        public StartScherm()
        {
            InitializeComponent();

            //Timer interval instellen op 1 seconde.
            timer.Interval = TimeSpan.FromSeconds(1);

            //Timer starten.
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            //i + 1 doen.
            i++;

            //Controleren of i groter/gelijk is aan 3.
            if (i >= 3)
            {
                //label tonen.
                lblKlik.Visibility = Visibility.Visible;

                //Controleren of i even/oneven is.
                if (i % 2 == 0)
                    //label onzichtbaarmaken.
                    lblKlik.Visibility = Visibility.Hidden;
                else
                    //label tonen.
                    lblKlik.Visibility = Visibility.Visible;
            }
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Zorgen dat je pas naar het volgende scherm gaat wanneer de label zichtbaar is.
            if (i >= 3)
            {
                //Nieuw scherm aanmaken en tonen.
                var login = new Login();
                login.Show();

                //Huidig scherm sluiten.
                Close();
            }
        }
    }
}