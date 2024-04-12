using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for StudentLibrary.xaml
    /// </summary>
    public partial class StudentLibrary : Page
    {

        string user;
        List<FYPReports> dataList = new List<FYPReports>();
        List<int> keywords = new List<int>();
        List<string> filterBy = new List<string>();
        int currentIndex = 0;

        public StudentLibrary()
        {
            InitializeComponent();

            user = App._username;

            filterBy.Add("Title");
            filterBy.Add("Description");
            filterBy.Add("Tools");

            filter.ItemsSource = filterBy;


        }

        private void Button_Click_Out(object sender, RoutedEventArgs e)
        {
            App._username = "";
            this.NavigationService.Navigate(new Uri("AdminLogin.xaml", UriKind.Relative));
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM FYPRepository", conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            dataList.Clear();
            keywords.Clear();
            string see = search.ToString();
            string[] words = see.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            while (reader.Read())
            {
                FYPReports fYPReports = new FYPReports();
                fYPReports.Title = reader[0].ToString();
                fYPReports.Description = reader[1].ToString();
                fYPReports.Link = reader[2].ToString();
                fYPReports.Tools = reader[3].ToString();

                dataList.Add(fYPReports);

                int count = 0;

                if (currentIndex == 0) // Filter by title
                {
                    foreach (string word in words)
                    {
                        string[] searchWords = fYPReports.Title.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string searchWord in searchWords)
                        {
                            if (searchWord.Equals(word, StringComparison.OrdinalIgnoreCase))
                            {
                                count++;
                            }
                        }
                    }  
                }

                else if (currentIndex == 1)
                {
                    foreach (string word in words)
                    {
                        string[] searchWords = fYPReports.Description.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string searchWord in searchWords)
                        {
                            if (searchWord.Equals(word, StringComparison.OrdinalIgnoreCase))
                            {
                                count++;
                            }
                        }
                    }
                }

                else
                {
                    foreach (string word in words)
                    {
                        string[] searchWords = fYPReports.Tools.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string searchWord in searchWords)
                        {
                            if (searchWord.Equals(word, StringComparison.OrdinalIgnoreCase))
                            {
                                count++;
                            }
                        }
                    }
                }

                keywords.Add(count);
            }

            conn.Close();

            var combinedList = dataList.Zip(keywords, (fyp, keyword) => new { FYP = fyp, Keyword = keyword })
                           // Sorting based on the values in the second list
                           .OrderByDescending(item => item.Keyword)
                           // Filtering out entries with a value of 0
                           .Where(item => item.Keyword != 0)
                           // Converting back to List<FYPReports>
                           .Select(item => item.FYP)
                           .ToList();

            // Reassigning the sorted and filtered list back to dataList
            dataList = combinedList;
            dataGrid.ItemsSource = dataList;

        }

        private void Button_Click_Search_All(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM FYPRepository", conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            dataList.Clear();

            while (reader.Read())
            {
                FYPReports fYPReports = new FYPReports();
                fYPReports.Title = reader[0].ToString();
                fYPReports.Description = reader[1].ToString();
                fYPReports.Link = reader[2].ToString();
                fYPReports.Tools = reader[3].ToString();

                dataList.Add(fYPReports);
            }

            conn.Close();
            dataGrid.ItemsSource = dataList;
        }

        private void SplitButtonClick(object sender, RoutedEventArgs e)
        {
            string selectedValue = filter.Text.ToString();

            if (selectedValue == "Title")
            {
                currentIndex = 0;
            }
            else if (selectedValue == "Description")
            {
                currentIndex = 1;
            }
            else if (selectedValue == "Tools")
            {
                currentIndex = 3;
            }

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

        private void Button_OpenReport_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked button
            Button button = sender as Button;

            // Get the corresponding FYPReports object from the DataContext of the button
            FYPReports report = button.DataContext as FYPReports;

            // Check if the report is not null and the Link property is not empty
            if (report != null && !string.IsNullOrEmpty(report.Link))
            {
                try
                {
                    // Open the default web browser and navigate to the specified URL
                    Process.Start("C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe", report.Link);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening report: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No report link available.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
