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
    public partial class StudentUploads : Page
    {

        string user;
        List<studentUploadItem> studentuploaditems = new List<studentUploadItem>();
        int teamID;
        string selectedFile;

        public StudentUploads()
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

            cmd = new MySqlCommand("SELECT * FROM studentUpload WHERE team_id = @teamID", conn);
            cmd.Parameters.AddWithValue("@teamID", teamID);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                studentUploadItem sui = new studentUploadItem();
                sui.uploadID = reader.GetInt32(0);
                sui.fileName = reader.GetString(2);
                sui.filePath = reader.GetString(3);
                sui.fileDesc = reader.GetString(4);
                sui.fileDate = reader.GetDateTime(5).ToString();

                if (reader.GetBoolean(6))
                {
                    sui.gradable = "Yes";
                }

                else
                {
                    sui.gradable = "No";
                }

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

        private async void Button_DeleteFile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            studentUploadItem report = button.DataContext as studentUploadItem;

            if (report != null && !string.IsNullOrEmpty(report.filePath))
            {
                string blobStorageContainerString = "DefaultEndpointsProtocol=https;AccountName=seprojectstorage;AccountKey=km44lJwL+Vsxy4QcYbdu7kVq+xELZcVFJByA9vqcvdqkvP/+fgmTb9fOwPpebWrFkK64KOXD2fqa+AStD9Ep6w==;EndpointSuffix=core.windows.net";
                string blobStorageContainerName = "studentupload";
                BlobContainerClient container = new BlobContainerClient(blobStorageContainerString, blobStorageContainerName);
                BlobClient blob = container.GetBlobClient(report.filePath);
                await blob.DeleteIfExistsAsync();

                string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand("DELETE FROM studentUpload WHERE upload_id = @UploadId", conn);
                cmd.Parameters.AddWithValue("@UploadId", report.uploadID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                this.NavigationService.Refresh();
            }
        }

        private async void Button_DownloadFile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            studentUploadItem report = button.DataContext as studentUploadItem;

            if (report != null && !string.IsNullOrEmpty(report.filePath))
            {
                string destinationFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", Path.GetFileName(report.filePath));
                string blobStorageContainerString = "DefaultEndpointsProtocol=https;AccountName=seprojectstorage;AccountKey=km44lJwL+Vsxy4QcYbdu7kVq+xELZcVFJByA9vqcvdqkvP/+fgmTb9fOwPpebWrFkK64KOXD2fqa+AStD9Ep6w==;EndpointSuffix=core.windows.net";
                string blobStorageContainerName = "studentupload";
                BlobContainerClient container = new BlobContainerClient(blobStorageContainerString, blobStorageContainerName);
                BlobClient blob = container.GetBlobClient(report.filePath);
                await blob.DownloadToAsync(destinationFilePath);

                string fileNameWithoutUniqueSuffix = RemoveUniqueSuffix(Path.GetFileName(destinationFilePath));
                string directoryPath = Path.GetDirectoryName(destinationFilePath);
                string newDestinationFilePath = Path.Combine(directoryPath, fileNameWithoutUniqueSuffix);

                File.Move(destinationFilePath, newDestinationFilePath);
            }
        }

        private string GetUniqueFileName(string filePath)
        {
            // Get the filename without extension
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            // Get the file extension
            string fileExtension = Path.GetExtension(filePath);

            // Add a unique suffix to the filename
            string uniqueFileName = $"{fileNameWithoutExtension}_{DateTime.Now.Ticks}{fileExtension}";

            return uniqueFileName;
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

        private void AddUploadButton(object sender, RoutedEventArgs e)
        {
            Uploadpopup.IsOpen = true; 
            Uploadpopup.Visibility = Visibility.Visible;
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Multiselect = false;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                selectedFile = dlg.FileName;
                SelectedFileText.Text = "Selected File: " + selectedFile;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Uploadpopup.IsOpen = false;
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
           
        }

        private async void UploadPopUpClose(object sender, RoutedEventArgs e)
        {
            UploadingMessage.Visibility = Visibility.Visible;

            string blobStorageContainerString = "DefaultEndpointsProtocol=https;AccountName=seprojectstorage;AccountKey=km44lJwL+Vsxy4QcYbdu7kVq+xELZcVFJByA9vqcvdqkvP/+fgmTb9fOwPpebWrFkK64KOXD2fqa+AStD9Ep6w==;EndpointSuffix=core.windows.net";
            string blobStorageContainerName = "studentupload";
            BlobContainerClient container = new BlobContainerClient(blobStorageContainerString, blobStorageContainerName);

            string currentDirectory = Directory.GetCurrentDirectory();
            string newDirectory = Path.Combine(currentDirectory, teamID.ToString());

            if (!Directory.Exists(newDirectory))
            {
                Directory.CreateDirectory(newDirectory);
            }

            string destinationFilePath = Path.Combine(newDirectory, Path.GetFileName(selectedFile));
            string uniqueFileName = GetUniqueFileName(destinationFilePath);
            string destinationFilePathWithUniqueName = Path.Combine(newDirectory, uniqueFileName);
            File.Copy(selectedFile, destinationFilePathWithUniqueName);

            BlobClient blob = container.GetBlobClient(Path.Combine(teamID.ToString(), uniqueFileName));
            FileStream stream = File.OpenRead(destinationFilePathWithUniqueName);
            await blob.UploadAsync(stream);
            stream.Close();

            Directory.Delete(newDirectory, true);

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT MAX(upload_id) FROM studentUpload", conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            int id = reader.GetInt32(0) + 1;
            string uploadDescription = UploadTextBox.Text.ToString();
            string fileName = Path.GetFileName(destinationFilePath);
            string filePath = Path.Combine(teamID.ToString(), uniqueFileName);


            reader.Close();

            string query = "INSERT INTO studentUpload (upload_id, team_id, fileName, filePath, fileDescription, fileDateTime, isGradable, weightage, totalMarks, obtainedMarks, comments) " +
                           "VALUES (@uploadid, @teamid, @filename, @filepath, @filedesc, @filedate, @isGrade, 0, 0, 0, '')";

            cmd = new MySqlCommand(query, conn);

            // Add parameters
            cmd.Parameters.AddWithValue("@uploadid", id);
            cmd.Parameters.AddWithValue("@teamid", teamID);
            cmd.Parameters.AddWithValue("@filename", fileName);
            cmd.Parameters.AddWithValue("@filepath", filePath);
            cmd.Parameters.AddWithValue("@filedesc", uploadDescription);
            cmd.Parameters.AddWithValue("@filedate", DateTime.Now);
            cmd.Parameters.AddWithValue("@isGrade", toggle.IsOn);

            cmd.ExecuteNonQuery();
            conn.Close(); ;

            Uploadpopup.IsOpen = false;
            UploadingMessage.Visibility = Visibility.Collapsed;
            this.NavigationService.Refresh();
        }
    }

    public class studentUploadItem
    {
        public int uploadID {  get; set; }
        public int teamID { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileDesc { get; set; }
        public string fileDate { get; set; }
        public string gradable {  get; set; }
        public float weightage { get; set; }
        public float totalMarks { get; set; }
        public float obtainedMarks { get; set; }
        public string comments { get; set; }
        public bool isGradable { get; set; }
    }
}
