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
    /// Логика взаимодействия для TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        PegasProjectPlanerEntities db = new PegasProjectPlanerEntities();
        Frame frame;
        PegasProjectPlanner.Courses _currentCourse;
        PegasProjectPlanner.Users _currentUser;
        public TasksPage(Frame frame = null, PegasProjectPlanner.Users currentUser = null, PegasProjectPlanner.Courses currentCourse = null)
        {
            InitializeComponent();
            this.frame = frame;
            _currentCourse = currentCourse;
            _currentUser = currentUser;
            LoadTask();
            if (_currentCourse != null) courseTitle.Text = _currentCourse.Title.ToString();
            if (db.Students.FirstOrDefault(p => p.ID_User == _currentUser.ID) != null) addTaskButton.Visibility = Visibility.Collapsed;
        }

        private void LoadTask()
        {
            
            List<Tasks> tasks = new List<Tasks>();
            foreach(var i in db.Tasks.ToList<Tasks>())
            {
                if (_currentCourse != null && i.ID_Courses == _currentCourse.ID) tasks.Add(i);
            }
           
            CoursesItemsControl.ItemsSource = tasks;
        }

        private void tasksDockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DockPanel dockPanel = (DockPanel)sender;
            var currentTask = db.Tasks.FirstOrDefault(p => p.ID.ToString() == dockPanel.Tag.ToString());
            frame.NavigationService.Navigate(new ConcreteTaskPage(frame, _currentUser, currentTask));
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new AddChangeTask(frame, _currentUser, _currentCourse));
        }
    }
}
