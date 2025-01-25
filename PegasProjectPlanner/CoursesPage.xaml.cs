using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PegasProjectPlanner
{
    /// <summary>
    /// Логика взаимодействия для CoursesPage.xaml
    /// </summary>
    public partial class CoursesPage : Page
    {
        PegasProjectPlanerEntities db = new PegasProjectPlanerEntities();
        Frame frame;
        PegasProjectPlanner.Users _currentUser;
        public CoursesPage(Frame frame, PegasProjectPlanner.Users currentUser = null)
        {
            InitializeComponent();
            LoadCourses();
            this.frame = frame;
            _currentUser = currentUser;
            
        }

        private void LoadCourses()
        {   
            List<Courses> courses = db.Courses.ToList<Courses>();

            CoursesItemsControl.ItemsSource = courses;
        }

        private void cardCourseStackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            var currentCourse = db.Courses.FirstOrDefault(p => p.ID.ToString() == stackPanel.Tag.ToString());
            frame.NavigationService.Navigate(new TasksPage(frame, _currentUser, currentCourse));
        }
    }
}
