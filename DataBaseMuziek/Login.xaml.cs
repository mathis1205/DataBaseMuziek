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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        //List aanmaken.
        List<accounts> LijstMetAccounts = new List<accounts>();

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {
                //List invullen.
                LijstMetAccounts = AccountsDA.HaalGegevensOp();

                //Controleren of alles is ingevuld.
                if(txbNaam.Text != "" && pwbWachtwoord.Password != "")
                {
                    foreach(accounts accounts in LijstMetAccounts)
                    {
                        //Controleren of het gebruiksnaam bestaat en zorgen dat het niet hoofdlettergevoelig is.
                        if(accounts.Naam.Equals(txbNaam.Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            //Controleren of het wachtwoord correct is.
                            if(accounts.Wachtwoord == pwbWachtwoord.Password)
                            {
                                //Tonen dat je succesvol bent ingelogd.
                                var mess = MessageBox.Show("U bent succesvol ingelogd.", "Succesvol ingelogd", MessageBoxButton.OK);

                                //Muziek scherm tonen wanneer er op OK wordt geklikt.
                                if(mess == MessageBoxResult.OK)
                                {
                                    //Muziek scherm tonen.
                                    Muziek muziek = new Muziek();
                                    muziek.Show();
                                    this.Close();
                                }                                
                            }
                            else
                            {
                                //Tonen dat het wachtwoord niet juist is en de passwoordbox leegmaken.
                                MessageBox.Show("Het wachtwoord is niet juist, probeer opnieuw.", "Foutief wachtwoord", MessageBoxButton.OK, MessageBoxImage.Warning);
                                pwbWachtwoord.Password = "";
                            }
                        }
                        else
                        {
                            //Tonen dat het gebruiksnaam niet gevonden is en de textbox leegmaken.
                            MessageBox.Show("Gebruikersnaam niet gevonden, probeer opnieuw.", "Gebruikers naam niet gevonden", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txbNaam.Text = "";
                        }
                    }
                }
                else
                {
                    //Tonen dat niet alles is ingevuld.
                    MessageBox.Show("U heeft niet alles ingevuld, gelieve alles in te vullen.", "Geen invoer", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            //Error tonen wanneer er iets niet klopt.
            catch(Exception error)
            {
                //Error tonen.
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
