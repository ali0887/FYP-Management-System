using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for AdminRegistration.xaml
    /// </summary>
    public partial class AdminRegistration : Page
    {
        List<Team> dataList = new List<Team>();

        public AdminRegistration()
        {
            InitializeComponent();

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Teams WHERE approved != 1", conn);

            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
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
            conn.Close();

            dataGrid.ItemsSource = dataList;
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

        private void PopUpClose(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("AdminDashboard.xaml", UriKind.Relative));
            popup.IsOpen = false;
        }
        private void Button_Click_Approve(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Team team = button.DataContext as Team;

            if (team != null)
            {
                string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Teams SET approved = 1 WHERE team_id = @TeamId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TeamId", team.TeamID);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            popup.IsOpen = true;
                        }
                    }
                }
            }
        }
    }

    public class Team
    {
        public int TeamID { get; set; }
        public string Roll1 { get; set; }
        public string Roll2 { get; set; }
        public string Roll3 { get; set; }
        public string Supervisor { get; set; }
        public string TeamName { get; set; }
        public int FYPYear { get; set; }
        public string MissionStat { get; set; }
        public int Approved { get; set; }
    }
}
