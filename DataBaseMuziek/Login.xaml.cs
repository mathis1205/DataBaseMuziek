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
                        //Controleren of 
                        if(accounts.Naam.Equals(txbNaam.Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            if(accounts.Wachtwoord == pwbWachtwoord.Password)
                            {
                                Muziek muziek = new Muziek();
                                muziek.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Het wachtwoord is niet juist, probeer opnieuw.", "Foutief wachtwoord", MessageBoxButton.OK, MessageBoxImage.Warning);
                                pwbWachtwoord.Password = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Gebruikersnaam niet gevonden, probeer opnieuw.", "Gebruikers naam niet gevonden", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txbNaam.Text = "";
                        }
                    }
                }
                else
                {
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
