using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PegasProjectPlanner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PegasProjectPlanner.Users _currentUser;
        public MainWindow(PegasProjectPlanner.Users currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
            MainFrame.NavigationService.Navigate(new HomePage(_currentUser));
 
            
        }
        


        private void onClickNavigationWithButton(object sender, RoutedEventArgs e)
        {
            if(sender.Equals(homeButton)) { MainFrame.NavigationService.Navigate(new HomePage(_currentUser)); }
            if (sender.Equals(personalAccountButton)) { MainFrame.NavigationService.Navigate(new PersonalAccountPage(MainFrame, _currentUser)); }
            if (sender.Equals(coursesButton)) { MainFrame.NavigationService.Navigate(new CoursesPage(MainFrame, _currentUser)); }
            if (sender.Equals(studentsListButton)) { MainFrame.NavigationService.Navigate(new StudentsListPage()); }
            if (sender.Equals(teatchersListButton)) { MainFrame.NavigationService.Navigate(new TeatchersListPage()); }
            if (sender.Equals(reportsButton)) { MainFrame.NavigationService.Navigate(new CourseReportsPage(_currentUser)); }


        }

        private void rightPanelButton_Click(object sender, RoutedEventArgs e)
        {
            if (rightStackPanel.Visibility == System.Windows.Visibility.Collapsed)
            {
                rightStackPanel.Visibility = System.Windows.Visibility.Visible;
                (sender as Button).Content = "X";
                (sender as Button).Background = System.Windows.Media.Brushes.White;
                (sender as Button).Foreground = System.Windows.Media.Brushes.Black;
                (sender as Button).Margin = new Thickness(0, 10, 5, 0);
            }
            else
            {
                rightStackPanel.Visibility = System.Windows.Visibility.Collapsed;
                (sender as Button).Content = "<";
                (sender as Button).Background = System.Windows.Media.Brushes.Green;
                (sender as Button).Foreground = System.Windows.Media.Brushes.White;
                (sender as Button).Margin = new Thickness(0, 10, 0, 0);

            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if(MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack) backButton.Visibility = Visibility.Visible;
            else backButton.Visibility = Visibility.Hidden;
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
