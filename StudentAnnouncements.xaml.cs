using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using Syncfusion.Windows.Shared;
using System.ComponentModel;
using System.Windows.Media;
using System.Configuration;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System.Diagnostics;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for StudentDashboard.xaml
    /// </summary>
    /// 
    public partial class StudentAnnouncements : Page
    {

        string user;
        List<string> memberName = new List<string>();
        List<Announcements> announcement = new List<Announcements>();
        List<Deadlines> deadlines = new List<Deadlines>();
        List<string> announcementsText = new List<string>();
        List<supervisorUploadItem> supUp = new List<supervisorUploadItem>();
        DatesCollection original = new DatesCollection();
        Team t = new Team();

        public StudentAnnouncements()
        {
            InitializeComponent();

            user = App._username;

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Teams WHERE roll_number_1 = @Username OR roll_number_2 = @Username OR roll_number_3 = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", user);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                t.TeamID = reader.GetInt32(0);
                t.Roll1 = reader.GetString(1);
                t.Roll2 = reader.GetString(2);
                t.Roll3 = reader.GetString(3);
                t.Supervisor = reader.GetString(4);
                t.TeamName = reader.GetString(5);
                t.FYPYear = reader.GetInt32(6);
                t.MissionStat = reader.GetString(7);
                t.Approved = reader.GetInt32(8);
            }
            reader.Close();

            calender.TodayCellSelectedBorderBrush = Brushes.Red;
            calender.TodayCellSelectedBackground = Brushes.Red;

            calender.TodayRowIsVisible = true;

            calender.SelectedDayCellBackground = Brushes.Yellow;
            calender.SelectedDayCellBorderBrush = Brushes.Blue;
            calender.SelectionForeground = Brushes.Red;
            calender.SelectedDayCellHoverBackground = Brushes.Green;
            calender.SelectionBorderBrush = Brushes.Red;

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

            mission.Text = t.MissionStat;   

            cmd = new MySqlCommand("SELECT FName, MName, LName FROM User WHERE Username = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", t.Supervisor);

            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                memberName.Add(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
            }

            reader.Close();

            SupName.Text = memberName[3];
            SupRoll.Text = t.Supervisor;


            FYPYear.Text = t.FYPYear.ToString();

            if (t.Approved == 1)
            {
                FYPApproved.Text = "Yes";
            }
            else
            {
                FYPApproved.Text = "No";
            }

            cmd = new MySqlCommand("SELECT * FROM Announcements WHERE team_id = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", t.TeamID);

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Announcements announce = new Announcements();
                announce.viewed = reader.GetBoolean(5);
                announce.text = reader.GetString(4);
                announce.teamID = t.TeamID;
                announce.announcmentID = reader.GetInt32(1);
                announce.announcementBy = reader.GetString(2);
                announce.announcementTo = reader.GetString(3);

                announcement.Add(announce);
            }

            reader.Close();

            foreach (Announcements announce in announcement)
            {
                if (announce.announcementBy.Equals(announce.announcementTo) && announce.announcementBy.Equals(user))
                {
                    announcementsText.Add("An announcment was added by you: " + announce.text);
                }

                else if (user.Equals(announce.announcementTo))
                {
                    string name;

                    if (t.Roll1.Equals(announce.announcementBy))
                    {
                        name = memberName[0];
                    }
                    else if (t.Roll2.Equals(announce.announcementBy))
                    {
                        name = memberName[1];
                    }
                    else
                    {
                        name = memberName[2];
                    }

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
            announcementItems.Reverse();
            dataGrid.ItemsSource = announcementItems;

            cmd = new MySqlCommand("SELECT * FROM deadlines WHERE team_id = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", t.TeamID);

            reader = cmd.ExecuteReader();
            DatesCollection dc = new DatesCollection();

            while (reader.Read())
            {
                Deadlines deadline = new Deadlines();

                deadline.teamID = reader.GetInt32(0);
                deadline.deadlineID = reader.GetInt32(1);

                DateTime dateO = reader.GetDateTime(3);
                Date date = new Date(dateO.Year, dateO.Month, dateO.Day);
                deadline.deadlineDate = date;

                deadline.deadlineMet = reader.GetBoolean(4);
                deadline.deadlineText = reader.GetString(2);

                calender.SetToolTip(deadline.deadlineDate, new ToolTip() { Content = deadline.deadlineText });

                SpecialDate specialDate = new SpecialDate();
                specialDate.Equals(date);
                calender.SpecialDates.Add(specialDate);

                dc.Add(dateO);


                deadlines.Add(deadline);
            }

            calender.SelectedDates = dc;

            foreach (DateTime datee in dc)
            {
                original.Add(datee);
            }

            reader.Close();

            cmd = new MySqlCommand("SELECT * FROM supervisorUpload WHERE team_id = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", t.TeamID);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                supervisorUploadItem upload = new supervisorUploadItem();
                upload.FileName = reader.GetString(2);
                upload.FilePath = reader.GetString(3);
                upload.FileDesc = reader.GetString(4);

                supUp.Add(upload);
            }

            supUp.Reverse();
            uploadGrid.ItemsSource = supUp;

            reader.Close();
            conn.Close();
        }

        private async void Button_DownloadFile_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            supervisorUploadItem report = button.DataContext as supervisorUploadItem;

            if (report != null && !string.IsNullOrEmpty(report.FilePath))
            {
                string destinationFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", Path.GetFileName(report.FilePath));
                string blobStorageContainerString = "DefaultEndpointsProtocol=https;AccountName=seprojectstorage;AccountKey=km44lJwL+Vsxy4QcYbdu7kVq+xELZcVFJByA9vqcvdqkvP/+fgmTb9fOwPpebWrFkK64KOXD2fqa+AStD9Ep6w==;EndpointSuffix=core.windows.net";
                string blobStorageContainerName = "supervisorupload";
                BlobContainerClient container = new BlobContainerClient(blobStorageContainerString, blobStorageContainerName);
                BlobClient blob = container.GetBlobClient(report.FilePath);
                await blob.DownloadToAsync(destinationFilePath);

                string fileNameWithoutUniqueSuffix = RemoveUniqueSuffix(Path.GetFileName(destinationFilePath));
                string directoryPath = Path.GetDirectoryName(destinationFilePath);
                string newDestinationFilePath = Path.Combine(directoryPath, fileNameWithoutUniqueSuffix);

                File.Move(destinationFilePath, newDestinationFilePath);
            }
        }

        private string RemoveUniqueSuffix(string fileName)
        {
            // Get the position of the last underscore character
            int lastUnderscoreIndex = fileName.LastIndexOf('_');

            // If underscore found and it's not the first character
            if (lastUnderscoreIndex > 0)
            {
                // Extract the file name without the unique suffix
                string fileNameWithoutSuffix = fileName.Substring(0, lastUnderscoreIndex);

                // Get the file extension
                string fileExtension = Path.GetExtension(fileName);

                // Return the file name with extension
                return fileNameWithoutSuffix + fileExtension;
            }

            // If underscore not found or found at the first character, return the original file name
            return fileName;
        }

        private void calender_DateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            foreach (var date in original)
            {
                calender.SelectedDates.Add(date);
            }
        }

        private void AddAnnouncementButton(object sender, RoutedEventArgs e)
        {
            Announcepopup.IsOpen = true;
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
            cmd.Parameters.AddWithValue("@team_id", t.TeamID);
            cmd.Parameters.AddWithValue("@roll_number_1", id);
            cmd.Parameters.AddWithValue("@roll_number_2", user);
            cmd.Parameters.AddWithValue("@roll_number_3", t.Supervisor);
            cmd.Parameters.AddWithValue("@supervisor_id", announceData);

            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand(query, conn);

            // Add parameters
            cmd.Parameters.AddWithValue("@team_id", t.TeamID);
            cmd.Parameters.AddWithValue("@roll_number_1", id + 1);
            cmd.Parameters.AddWithValue("@roll_number_2", user);
            cmd.Parameters.AddWithValue("@roll_number_3", t.Roll1);
            cmd.Parameters.AddWithValue("@supervisor_id", announceData);

            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand(query, conn);

            // Add parameters
            cmd.Parameters.AddWithValue("@team_id", t.TeamID);
            cmd.Parameters.AddWithValue("@roll_number_1", id + 2);
            cmd.Parameters.AddWithValue("@roll_number_2", user);
            cmd.Parameters.AddWithValue("@roll_number_3", t.Roll2);
            cmd.Parameters.AddWithValue("@supervisor_id", announceData);

            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand(query, conn);

            // Add parameters
            cmd.Parameters.AddWithValue("@team_id", t.TeamID);
            cmd.Parameters.AddWithValue("@roll_number_1", id + 3);
            cmd.Parameters.AddWithValue("@roll_number_2", user);
            cmd.Parameters.AddWithValue("@roll_number_3", t.Roll3);
            cmd.Parameters.AddWithValue("@supervisor_id", announceData);

            cmd.ExecuteNonQuery();

            conn.Close();

            Announcepopup.IsOpen = false;

            this.NavigationService.Refresh();
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
