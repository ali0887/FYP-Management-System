using MySql.Data.MySqlClient;
using Syncfusion.Windows.Shared;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for StudentDashboard.xaml
    /// </summary>
    public partial class StudentDashboard : Page
    {

        string user;
        List<string> userData = new List<string>();
        Team t = new Team();
        List<Deadlines> deadlines = new List<Deadlines> ();

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
            
            reader.Close();

            name.Text = userData[1] + " " + userData[2] + " " + userData[3];
            roll.Text = userData[0];
            age.Text = userData[11];
            gen.Text = userData[6];
            mail.Text = userData[7];
            num.Text = userData[4];
            cgpa.Text = userData[9];
            degree.Text = userData[10];


            cmd = new MySqlCommand("SELECT * FROM Teams WHERE roll_number_1 = @Username or roll_number_2 = @Username or roll_number_3 = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", user);

            reader = cmd.ExecuteReader();
            if (reader.Read()) //iterates through rows given by query
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

            cmd = new MySqlCommand("SELECT * FROM deadlines WHERE team_id = @teamID", conn);
            cmd.Parameters.AddWithValue("@teamID", t.TeamID);
            reader = cmd.ExecuteReader();

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

                deadlines.Add(deadline);
            }

            reader.Close();

            cmd = new MySqlCommand("SELECT FName, MName, LName FROM User WHERE username = @user1 or username = @user2 or username = @user3", conn);
            cmd.Parameters.AddWithValue("@user1", t.Roll1);
            cmd.Parameters.AddWithValue("@user2", t.Roll2);
            cmd.Parameters.AddWithValue("@user3", t.Roll3);
            reader = cmd.ExecuteReader();


            userData.Clear();
            while (reader.Read()) {
                string n = reader.GetString(0);
                n += " ";
                n += reader.GetString(1);
                n += " ";
                n += reader.GetString(2);

                userData.Add(n);
            }

            conn.Close();

            pname.Text = t.TeamName;

            if (t.Roll1 == user)
            {
                mem1.Text = userData[1];
                mem2.Text = userData[2];
            }

            else if (t.Roll2 == user)
            {
                mem1.Text = userData[0];
                mem2.Text = userData[2];
            }
            else
            {
                mem1.Text = t.Roll1;
                mem1.Text = userData[0];
                mem2.Text = userData[1];
            }

            if (deadlines.Count == 0)
            {
                TextBlock noDeadlines = new TextBlock();
                noDeadlines.Text = "No Upcoming Deadlines";
                noDeadlines.FontSize = 15;
                noDeadlines.FontFamily = new FontFamily("Roboto Light");
                noDeadlines.Margin = new Thickness(0, 0, 0, 10);
                mainPanelDeadlines.Children.Add(noDeadlines);
            }

            else
            {
                Date oneWeekFromToday = new Date(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 7);
                int count = deadlines.Count(d => d.deadlineDate <= oneWeekFromToday);

                if (count == 0)
                {
                    TextBlock noDeadlines = new TextBlock();
                    noDeadlines.Text = "No Upcoming Deadlines";
                    noDeadlines.FontSize = 15;
                    noDeadlines.FontFamily = new FontFamily("Roboto Light");
                    noDeadlines.Margin = new Thickness(0, 0, 0, 10);
                    mainPanelDeadlines.Children.Add(noDeadlines);
                }
                
                if (count > 3)
                {
                    mainBorderDeadlines.Height = 250;
                    mainPanelDeadlines.Height = 250;
                }

                foreach (var deadline in deadlines)
                {
                    Date currentDate = new Date(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                    if (deadline.deadlineDate <= oneWeekFromToday && deadline.deadlineDate >= currentDate)
                    {
                        TextBlock addDeadline = new TextBlock();
                        DateOnly dateOnly = new DateOnly(deadline.deadlineDate.Year, deadline.deadlineDate.Month, deadline.deadlineDate.Day);
                        addDeadline.Text = deadline.deadlineText + " due on " + dateOnly.DayOfWeek + ", " + dateOnly.ToString("MMMM") + " " + dateOnly.Day + ", " + dateOnly.Year;
                        addDeadline.FontSize = 15;
                        addDeadline.FontFamily = new FontFamily("Roboto Light");
                        addDeadline.Margin = new Thickness(0, 0, 0, 10);
                        addDeadline.TextWrapping = TextWrapping.Wrap;
                        mainPanelDeadlines.Children.Add(addDeadline);
                    }
                }
            }
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
