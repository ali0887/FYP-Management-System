using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for StudentDashboard.xaml
    /// </summary>
    /// 
    public partial class StudentAnnouncements : Page
    {

        string user;
        string teamName;
        List<string> memberName = new List<string>();
        List<announcements> announcement = new List<announcements>();
        List<String> announcementsText = new List<String>();
        int teamId;
        Team t;

        public StudentAnnouncements()
        {
            InitializeComponent();

            user = App._username;

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT team_id, team_name FROM Teams WHERE roll_number_1 = @Username OR roll_number_2 = @Username OR roll_number_3 = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", user);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                teamId = reader.GetInt32(0);
                teamName = reader.GetString(1);
            }

            headerName.Text = teamName;
            reader.Close();

            cmd = new MySqlCommand("SELECT * FROM Teams WHERE team_id = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", teamId);

            reader = cmd.ExecuteReader();
            t = new Team();
            if (reader.Read())
            {
                t.TeamID = reader.GetInt32(0);
                t.Roll1 = reader.GetString(1);
                t.Roll2 = reader.GetString(2);
                t.Roll3 = reader.GetString(3);
                t.Supervisor = reader.GetString(4);
                t.TeamName = reader.GetString(5);
                t.FYPYear = reader.GetInt32(6);
            }

            reader.Close();

            memberName.Clear();

            cmd = new MySqlCommand("SELECT FName, MName, LName FROM User WHERE Username = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", t.Roll1);


            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string name = reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2);
                memberName.Add(name);
            }

            reader.Close();


            cmd = new MySqlCommand("SELECT FName, MName, LName FROM User WHERE Username = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", t.Roll2);

            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                memberName.Add(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
            }

            reader.Close();

            cmd = new MySqlCommand("SELECT FName, MName, LName FROM User WHERE Username = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", t.Roll3);

            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                memberName.Add(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
            }

            reader.Close();


            S1Name.Text = memberName[0];
            S1Roll.Text = t.Roll1;

            S2Name.Text = memberName[1];
            S2Roll.Text = t.Roll2;

            S3Name.Text = memberName[2];
            S3Roll.Text = t.Roll3;


            cmd = new MySqlCommand("SELECT * FROM announcements WHERE team_id = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", teamId);

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                announcements announce = new announcements();
                announce.viewed = reader.GetBoolean(5);
                announce.text = reader.GetString(4);
                announce.teamID = teamId;
                announce.announcmentID = reader.GetInt32(1);
                announce.announcementBy = reader.GetString(2);
                announce.announcementTo = reader.GetString(3);

                announcement.Add(announce);
            }

            reader.Close();

            foreach (announcements announce in announcement)
            {
                if (announce.announcementBy.Equals(announce.announcementTo) && announce.announcementBy.Equals(user))
                {
                    announcementsText.Add("An announcment was added by you: " + announce.text);
                }

                else if (user.Equals(announce.announcementTo))
                {
                    announcementsText.Add("An announcment was added by " + announce.announcementBy + ": " + announce.text);
                }
            }

            List<AnnouncementItem> announcementItems = new List<AnnouncementItem>();

            foreach (string message in announcementsText)
            {
                AnnouncementItem aI = new AnnouncementItem();
                aI.AnnouncementsText = message;
                announcementItems.Add(aI);
            }

            // Set the ItemsSource of the DataGrid to the collection of AnnouncementItem objects
            dataGrid.ItemsSource = announcementItems;

            conn.Close();
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected date
            DateTime selectedDate = (sender as Calendar).SelectedDate ?? DateTime.Today;

            // Display a popup window to enter the announcement
            Deadlinepopup.IsOpen = true;

            // Here you can access the entered announcement from the popup window and save it to the database
        }

        private void AddAnnouncementButton(object sender, RoutedEventArgs e)
        {
            Announcepopup.IsOpen = true;
        }

        private void DeadlinePopUpClose(object sender, RoutedEventArgs e)
        {
            Deadlinepopup.IsOpen = false;
        }

        private void AnnouncementPopUpClose(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT MAX(announcement_id) FROM announcements", conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            int id = reader.GetInt32(0) + 1;
            string announceData = AnnouncementTextBox.Text.ToString();

            reader.Close();

            string query = "INSERT INTO announcements (team_id, announcement_id, announcement_by_id, announcement_to_id, announcement_text, viewewd) " +
                      "VALUES (@team_id, @roll_number_1, @roll_number_2, @roll_number_3, @supervisor_id, 0)";

            cmd = new MySqlCommand(query, conn);

            // Add parameters
            cmd.Parameters.AddWithValue("@team_id", teamId);
            cmd.Parameters.AddWithValue("@roll_number_1", id);
            cmd.Parameters.AddWithValue("@roll_number_2", user);
            cmd.Parameters.AddWithValue("@roll_number_3", user);
            cmd.Parameters.AddWithValue("@supervisor_id", announceData);

            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand(query, conn);

            // Add parameters
            cmd.Parameters.AddWithValue("@team_id", teamId);
            cmd.Parameters.AddWithValue("@roll_number_1", id + 1);
            cmd.Parameters.AddWithValue("@roll_number_2", user);
            cmd.Parameters.AddWithValue("@roll_number_3", t.Roll1);
            cmd.Parameters.AddWithValue("@supervisor_id", announceData);

            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand(query, conn);

            // Add parameters
            cmd.Parameters.AddWithValue("@team_id", teamId);
            cmd.Parameters.AddWithValue("@roll_number_1", id + 2);
            cmd.Parameters.AddWithValue("@roll_number_2", user);
            cmd.Parameters.AddWithValue("@roll_number_3", t.Roll2);
            cmd.Parameters.AddWithValue("@supervisor_id", announceData);

            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand(query, conn);

            // Add parameters
            cmd.Parameters.AddWithValue("@team_id", teamId);
            cmd.Parameters.AddWithValue("@roll_number_1", id + 3);
            cmd.Parameters.AddWithValue("@roll_number_2", user);
            cmd.Parameters.AddWithValue("@roll_number_3", t.Roll3);
            cmd.Parameters.AddWithValue("@supervisor_id", announceData);

            cmd.ExecuteNonQuery();

            conn.Close();

            Announcepopup.IsOpen = false;

        }


        private void Button_Click_Out(object sender, RoutedEventArgs e)
        {
            App._username = "";
            this.NavigationService.Navigate(new Uri("AdminLogin.xaml", UriKind.Relative));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Announcepopup.IsOpen = false;
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
    }


}
