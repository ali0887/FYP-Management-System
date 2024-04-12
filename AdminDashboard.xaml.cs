using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Page
    {

        string user;
        List<string> userData = new List<string>();

        public AdminDashboard()
        {
            InitializeComponent();

            user = App._username;

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM user WHERE Username = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", user);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) //iterates through rows given by query
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    userData.Add(reader[i].ToString());
                }
            }
            conn.Close();

            name.Text = userData[1] + " " + userData[2] + " " + userData[3];
            roll.Text = userData[0];
            age.Text = userData[11];
            gen.Text = userData[6];
            mail.Text = userData[7];
            num.Text = userData[4];
        }

        private void Button_Click_Out(object sender, RoutedEventArgs e)
        {
            App._username = "";
            this.NavigationService.Navigate(new Uri("AdminLogin.xaml", UriKind.Relative));
        }

        private void ADashboard(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("AdminDashboard.xaml", UriKind.Relative));
        }

        private void ARegistration(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("AdminRegistration.xaml", UriKind.Relative));
        }

        private void ALibrary(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("AdminLibrary.xaml", UriKind.Relative));
        }

        private void AEvaluations(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("AdminEvaluations.xaml", UriKind.Relative));
        }

        private void AAnnouncements(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("AdminAnnouncements.xaml", UriKind.Relative));
        }
    } 
}
