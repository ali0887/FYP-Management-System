using MySql.Data.MySqlClient;
using System.Net;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for Admin_Login.xaml
    /// </summary>
    public partial class AdminLogin : Page
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            string usernamee = username.Text;
            SecureString passwordd = password.SecurePassword;

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT Password FROM Login WHERE Username = @Username", conn); // Select only the password hash from the database
            cmd.Parameters.AddWithValue("@Username", usernamee);
            conn.Open();

            bool isUser = false;

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) // Check if user exists
            {
                // Retrieve the hashed password from the database
                string passwordHashFromDB = reader.GetString(0); // Assuming the password hash is in the first column (index 0)

                // Convert the SecureString entered by the user to a plain text string (not recommended, but needed for comparison)
                string passwordEntered = new NetworkCredential(string.Empty, passwordd).Password;

                // Compare the hashed password from the database with the password entered by the user
                if (passwordEntered == passwordHashFromDB) // Hash function is a placeholder for your actual hashing algorithm
                {
                    isUser = true;
                }
            }
            conn.Close();

            if (isUser)
            {
                App._username = usernamee;

                conn = new MySqlConnection(connectionString);
                cmd = new MySqlCommand("SELECT * FROM User WHERE Username = @Username", conn);
                cmd.Parameters.AddWithValue("@Username", usernamee);
                conn.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read()) // Check if data is available
                {
                    if (reader[5].ToString() == "A")
                    {
                        this.NavigationService.Navigate(new Uri("AdminDashboard.xaml", UriKind.Relative));
                    }
                    else if (reader[5].ToString() == "S")
                    {
                        this.NavigationService.Navigate(new Uri("StudentDashboard.xaml", UriKind.Relative));
                    }
                    else
                    {
                        this.NavigationService.Navigate(new Uri("SupervisorDashboard.xaml", UriKind.Relative));
                    }
                }
                conn.Close(); // Close the connection after reading data
            }
        }
    }
}
