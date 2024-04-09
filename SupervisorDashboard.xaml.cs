using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for StudentDashboard.xaml
    /// </summary>
    /// 
    public partial class SupervisorDashboard : Page
    {

        string user;
        List<string> userData = new List<string>();
        List<Team> dataList = new List<Team>();

        public SupervisorDashboard()
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

            name.Text = userData[1] + " " + userData[2] + " " + userData[3];
            roll.Text = userData[0];
            age.Text = userData[11];
            gen.Text = userData[6];
            mail.Text = userData[7];
            num.Text = userData[4];
            dep.Text = userData[10];

            reader.Close();

            cmd = new MySqlCommand("SELECT * FROM Teams WHERE supervisor_id = @Sup", conn);
            cmd.Parameters.AddWithValue("@Sup",user);

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Team team = new Team();
                team.TeamID = reader.GetInt32(0);
                team.Roll1 = reader[1].ToString();
                team.Roll2 = reader[2].ToString();
                team.Roll3 = reader[3].ToString();
                team.Supervisor = reader[4].ToString();
                team.TeamName = reader[5].ToString();
                team.FYPYear = reader.GetInt32(6);
                team.MissionStat = reader[7].ToString();
                team.Approved = reader.GetInt32(8);

                dataList.Add(team);
            }
            reader.Close();
            conn.Close();

            dataGrid.ItemsSource = dataList;

        }

        private void Button_Click_Out(object sender, RoutedEventArgs e)
        {
            App._username = "";
            this.NavigationService.Navigate(new Uri("AdminLogin.xaml", UriKind.Relative));
        }

        private void TDashboard(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("SupervisorDashboard.xaml", UriKind.Relative));
        }

        private void TRegistration(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("SupervisorRegistration.xaml", UriKind.Relative));
        }

        private void TLibrary(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("SupervisorLibrary.xaml", UriKind.Relative));
        }

        private void TTeams(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("SupervisorTeams.xaml", UriKind.Relative));
        }
    }

    
}
