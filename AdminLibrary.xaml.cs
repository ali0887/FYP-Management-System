using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using ExcelDataReader;
using System.Data;
using System.Windows.Data;
using System.Diagnostics;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for AdminLibrary.xaml
    /// </summary>
    public partial class AdminLibrary : Page
    {

        string user;
        List<FYPReports> dataList = new List<FYPReports>();

        public AdminLibrary()
        {
            InitializeComponent();

            user = App._username;

        }

        private void Button_Click_Out(object sender, RoutedEventArgs e)
        {
            App._username = "";
            this.NavigationService.Navigate(new Uri("AdminLogin.xaml", UriKind.Relative));
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                search.Text = openFileDialog.FileName;
            }

            try
            {
                using (var stream = File.Open(search.Text, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var dataSet = reader.AsDataSet();

                        // Clear previous data
                        dataList.Clear();
                        int i = 0;

                        foreach (DataTable table in dataSet.Tables)
                        {
                            if (i == 0)
                            {
                                i += 1;

                            }

                            else
                            {

                                foreach (DataRow row in table.Rows)
                                {
                                    if (row["Column2"].ToString() == "FYP Title")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        FYPReports report = new FYPReports();
                                        report.Title = row["Column0"].ToString();
                                        report.Description = row["Column1"].ToString();
                                        report.Link = row["Column2"].ToString();
                                        report.Tools = row["Column3"].ToString();
                                        dataList.Add(report);
                                    }
                                }
                            }
                        }

                        dataGrid.ItemsSource = dataList;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading Excel file: " + ex.Message);
            }
        }


        private void Button_Click_Upload(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("DELETE FROM FYPRepository", conn);
            conn.Open();
            cmd.ExecuteNonQuery();

            int rowsAffected = 0;

            foreach (FYPReports report in dataList)
            {
                string query = $"INSERT INTO FYPRepository (Title, Description, Link, Tools) VALUES (@Title, @Description, @Link, @Tools)";

                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", report.Title);
                cmd.Parameters.AddWithValue("@Description", report.Description);
                cmd.Parameters.AddWithValue("@Link", report.Link);
                cmd.Parameters.AddWithValue("@Tools", report.Tools);

                rowsAffected = cmd.ExecuteNonQuery();
            }

            if (rowsAffected > 0)
            {
                popup.IsOpen = true;
            }
            else
            {
                popup.IsOpen = false;
            }

            conn.Close();
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

        private void PopUpClose(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            this.NavigationService.Navigate(new Uri("AdminDashboard.xaml", UriKind.Relative));
        }
    }

    public class FYPReports
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Tools { get; set; }
    }
}
