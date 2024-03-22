using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new Uri("AdminLogin.xaml", UriKind.Relative));
        }

        private void LaunchGitHubSite(object sender, RoutedEventArgs e)
        {
            // Launch the GitHub site...
        }

        private void DeployCupCakes(object sender, RoutedEventArgs e)
        {
            // deploy some CupCakes...
        }

        public void NavigateToAdminDashboard()
        {
            mainFrame.Navigate(new Uri("AdminDashboard.xaml", UriKind.Relative));
        }
    }
}