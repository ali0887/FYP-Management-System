using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using Syncfusion.Windows.Shared;
using System.ComponentModel;
using System.Windows.Media;
using System.Configuration;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for StudentDashboard.xaml
    /// </summary>
    /// 
    public partial class SupervisorEvaluations : Page
    {

        string user;
        List<string> teamName = new List<string>();
        List<string> memberName = new List<string>();
        List<Announcements> announcement = new List<Announcements>();
        List<Deadlines> deadlines = new List<Deadlines>();
        List<String> announcementsText = new List<String>();
        DatesCollection original = new DatesCollection();
        List<studentUploadItem> studentUploadItems = new List<studentUploadItem>();


        int teamId;

        public SupervisorEvaluations()
        {
            InitializeComponent();

            user = App._username;

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT team_id, team_name FROM Teams WHERE supervisor_id = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", user);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                teamName.Add(reader.GetString(1));
            }

            conn.Close();

            filter.ItemsSource = teamName;

        }

        private void SplitButtonClick(object sender, RoutedEventArgs e)
        {
            string currentTeam = filter.Text.ToString();
            int i = 1;

            foreach (string team in teamName)
            {
                if (team == currentTeam)
                {
                    break;
                }

                i++;
            }

            teamId = i;

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM studentUpload WHERE team_id = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", i);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                studentUploadItem st = new studentUploadItem();
                st.uploadID = reader.GetInt32(0);
                st.teamID = teamId;
                st.fileName = reader.GetString(2);
                st.filePath = reader.GetString(3);
                st.fileDesc = reader.GetString(4);
                st.fileDate = reader.GetDateTime(5).ToString();
                st.isGradable = reader.GetBoolean(6);
                st.weightage = reader.GetFloat(7);
                st.totalMarks = reader.GetFloat(8);
                st.obtainedMarks = reader.GetFloat(9);
                st.comments = reader.GetString(10);

                if (st.isGradable)
                {
                   studentUploadItems.Add(st);
                }
            }


            reader.Close();
            conn.Close();

            studentUploadItems.Reverse();
            dataGrid.ItemsSource = studentUploadItems;
        }

        private void Button_UpdateMarks_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            studentUploadItem st = button.DataContext as studentUploadItem;

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Assuming your table name is "studentUpload" and you have appropriate column names
                string updateQuery = "UPDATE studentUpload SET weightage = @weightage, totalMarks = @totalMarks, obtainedMarks = @obtainedMarks WHERE upload_id = @uploadID";
                
                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@weightage", st.weightage);
                cmd.Parameters.AddWithValue("@totalMarks", st.totalMarks);
                cmd.Parameters.AddWithValue("@obtainedMarks", st.obtainedMarks);
                cmd.Parameters.AddWithValue("@uploadID", st.uploadID);

                cmd.ExecuteNonQuery();
                this.NavigationService.Refresh();
            }
        }


        private void Button_AddComments_Click(object sender, RoutedEventArgs e)
        {
            Commentpopup.IsOpen = true;
        }

        private void CommentPopUpClose(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            studentUploadItem st = button.DataContext as studentUploadItem;

            string comments = CommentTextBox.Text;

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // Assuming your table name is "StudentUploadItems" and you have appropriate column names
                string updateQuery = "UPDATE studentUpload SET comments = @comments WHERE upload_id = @uploadID";

                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@comments", comments);
                cmd.Parameters.AddWithValue("@uploadID", st.uploadID);

                int rowsAffected = cmd.ExecuteNonQuery();
                this.NavigationService.Refresh();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Commentpopup.IsOpen = false;
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

        private void TEvaluations(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("SupervisorEvaluations.xaml", UriKind.Relative));
        }
    }
}
