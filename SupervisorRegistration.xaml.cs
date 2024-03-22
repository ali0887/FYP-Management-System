using MySql.Data.MySqlClient;
using System.Formats.Tar;
using System.Windows;
using System.Windows.Controls;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for SupervisorRegistration.xaml
    /// </summary>
    /// 

    public partial class SupervisorRegistration : Page
    {
        int nextTeamNum, year;

        public SupervisorRegistration()
        {
            InitializeComponent();

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT MAX(team_id) FROM Teams", conn);
           
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) //iterates through rows given by query
            {
                nextTeamNum = reader.GetInt32(0) + 1;
            }
            reader.Close();

            cmd = new MySqlCommand("SELECT YEAR(CURDATE())", conn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                year = reader.GetInt32(0); 
            }
            reader.Close();
            conn.Close();



        }

        private void Button_Click_Add(object sender, System.Windows.RoutedEventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            string query = "INSERT INTO Teams (team_id, roll_number_1, roll_number_2, roll_number_3, supervisor_id, team_name, fyp_year, mission_statement, approved) " +
                      "VALUES (@team_id, @roll_number_1, @roll_number_2, @roll_number_3, @supervisor_id, @team_name, @fyp_year, @mission_statement, 0)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@team_id", nextTeamNum);
                    cmd.Parameters.AddWithValue("@roll_number_1", roll1.Text);
                    cmd.Parameters.AddWithValue("@roll_number_2", roll2.Text);
                    cmd.Parameters.AddWithValue("@roll_number_3", roll3.Text);
                    cmd.Parameters.AddWithValue("@supervisor_id", App._username);
                    cmd.Parameters.AddWithValue("@team_name", name.Text);
                    cmd.Parameters.AddWithValue("@fyp_year", year);
                    cmd.Parameters.AddWithValue("@mission_statement", mission.Text);

                    // Open connection and execute query
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    // Check if rows were inserted successfully
                    if (rowsAffected > 0)
                    {
                       popup.IsOpen = true;
                    }
                    else
                    {
                        popup.IsOpen = false;
                    }
                }
            }
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

        private void PopUpClose(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            this.NavigationService.Navigate(new Uri("SupervisorDashboard.xaml", UriKind.Relative));
        }
    }
}
