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
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        public Account()
        {
            InitializeComponent();
            ListboxInvullen();
        }

        //Listen aanmaken.
        List<accounts> LijstMetAccounts = new List<accounts>();

        private void ListboxInvullen()
        {
            //Listbox leegmaken.
            lsbAccounts.Items.Clear();

            foreach (var item in AccountsDA.HaalGegevensOp())
            {
                //List invullen.
                LijstMetAccounts.Add(item);

                //Listbox invullen.
                lsbAccounts.Items.Add(item.Naam);
            }

            //List invullen.
            LijstMetAccounts = AccountsDA.HaalGegevensOp();
        }

        private void WpfUpdaten()
        {
            //WPF updaten.
            ListboxInvullen();
            txbNaam.Text = pwbWachtwoord.Password = "";
        }

        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            //Foutenopvang.
            try
            {
                //Controle op invoer.
                if (txbNaam.Text != "" && pwbWachtwoord.Password != "")
                {
                    //Klasse aanmaken.
                    accounts account = new accounts();

                    //Klasse variabelen invullen.
                    account.Naam = txbNaam.Text;
                    account.Wachtwoord = pwbWachtwoord.Password;

                    //Gegevens meegeven met de database.
                    AccountsDA.voegAccountToe(account);

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
            catch(Exception error)
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
                if (lsbAccounts.SelectedIndex != -1)
                {
                    //Vragen of de gebruiker zeker is van zijn keuze.
                    var check = MessageBox.Show("Bent u zeker dat u deze gegevens wilt verwijderen?", "Bent u zeker?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    //Controleren of de gebruiker zeker is.
                    if (check == MessageBoxResult.Yes)
                    {
                        //Geselecteerde item verwijderen.
                        AccountsDA.DeleteAccount(LijstMetAccounts[lsbAccounts.SelectedIndex].Account_ID);

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
            //Muziekscherm tonen.
            Muziek muziek = new Muziek();
            muziek.Show();
            this.Close();
        }
    }
}
