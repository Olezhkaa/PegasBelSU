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
    /// Логика взаимодействия для PersonalAccountPage.xaml
    /// </summary>
    public partial class PersonalAccountPage : Page
    {
        PegasProjectPlanerEntities db = new PegasProjectPlanerEntities();
        PegasProjectPlanner.Users _currentUser;
        Teatchers teatchers;
        Students students;
        Frame frame;
        public PersonalAccountPage(Frame frame, Users currentUser)
        {
            InitializeComponent();
            this.frame = frame;
            _currentUser = currentUser;
            students = db.Students.FirstOrDefault(p => p.ID_User == _currentUser.ID);
            teatchers = db.Teatchers.FirstOrDefault(p => p.ID_User == _currentUser.ID);
            LoadDataUser();
            LoadCourses();
        }

        public void LoadDataUser()
        {
            if(students == null)
            {
                fullNameTextBlock.Text = $"{teatchers.Surname} {teatchers.Name} {teatchers.Patronymic}";
                dateOfBirthTextBlock.Text = teatchers.DateOfBirth;
            }
            else
            {
                fullNameTextBlock.Text = $"{students.Surname} {students.Name} {students.Patronymic}";
                dateOfBirthTextBlock.Text = students.DateOfBirth;
            }
            emailTextBlock.Text = $"{_currentUser.Login}@bsu.edu.ru";
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
