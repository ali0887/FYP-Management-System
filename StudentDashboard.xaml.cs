using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for StudentDashboard.xaml
    /// </summary>
    public partial class StudentDashboard : Page
    {

        string user;
        List<string> userData = new List<string>();

        public StudentDashboard()
        {
            InitializeComponent();

            user = App._username;

            string connectionString = "Server=127.0.0.1;Port=3306;Database=SE;Uid=root;Pwd=12345678;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM user WHERE Username = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", user);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) //iterates through rows given by query
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    userData.Add(reader[i].ToString());
                }
            }
            conn.Close();

            name.Text = userData[1] + " " + userData[2] + " " + userData[3];
            roll.Text = userData[0];
            age.Text = userData[11];
            gen.Text = userData[6];
            mail.Text = userData[7];
            num.Text = userData[4];
            cgpa.Text = userData[9];
            degree.Text = userData[10];

            conn = new MySqlConnection(connectionString);
            cmd = new MySqlCommand("SELECT * FROM Teams WHERE roll_number_1 = @Username or roll_number_2 = @Username or roll_number_3 = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", user);
            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.Read()) //iterates through rows given by query
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    userData.Add(reader[i].ToString());
                }
            }
            conn.Close();

            pname.Text = userData[11 + 5];

        }

        private void Button_Click_Out(object sender, RoutedEventArgs e)
        {

        }
    }
}
