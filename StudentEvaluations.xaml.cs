using Azure.Storage.Blobs;
using MahApps.Metro.Controls;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for StudentLibrary.xaml
    /// </summary>
    public partial class StudentEvaluations : Page
    {

        string user;
        List<studentUploadItem> studentuploaditems = new List<studentUploadItem>();
        int teamID;

        public StudentEvaluations()
        {
            InitializeComponent();

            user = App._username;

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT team_id FROM teams WHERE roll_number_1 = @Username OR roll_number_2 = @Username OR roll_number_3 = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", user);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            teamID = reader.GetInt32(0);

            reader.Close();

            cmd = new MySqlCommand("SELECT * FROM studentUpload WHERE team_id = @teamID AND isGradable = 1 ", conn);
            cmd.Parameters.AddWithValue("@teamID", teamID);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                studentUploadItem sui = new studentUploadItem();
                sui.uploadID = reader.GetInt32(0);
                sui.teamID = teamID;
                sui.fileName = reader.GetString(2);
                sui .filePath = reader.GetString(3);
                sui.fileDesc = reader.GetString(4);
                sui.fileDate = reader.GetDateTime(5).ToString();
                sui.isGradable = reader.GetBoolean(6);
                sui.weightage = reader.GetFloat(7);
                sui.totalMarks = reader.GetFloat(8);
                sui.obtainedMarks = reader.GetFloat(9);
                sui.comments = reader.GetString(10);

                studentuploaditems.Add(sui);
            }

            reader.Close();
            conn.Close();

            studentuploaditems.Reverse();
            dataGrid.ItemsSource = studentuploaditems;

        }

        private void Button_Click_Out(object sender, RoutedEventArgs e)
        {
            App._username = "";
            this.NavigationService.Navigate(new Uri("AdminLogin.xaml", UriKind.Relative));
        }

        private void SDashboard(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StudentDashboard.xaml", UriKind.Relative));
        }

        private void SLibrary(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StudentLibrary.xaml", UriKind.Relative));
        }

        private void STeam(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StudentAnnouncements.xaml", UriKind.Relative));
        }

        private void SUpload(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StudentUploads.xaml", UriKind.Relative));
        }

        private void SEvaluations(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StudentEvaluations.xaml", UriKind.Relative));
        }
    }
}
