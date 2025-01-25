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
    /// Логика взаимодействия для CourseReports.xaml
    /// </summary>
    public partial class CourseReportsPage : Page
    {
        PegasProjectPlanerEntities db = new PegasProjectPlanerEntities();
        PegasProjectPlanner.Users _currentUser;

        List<CoursesReports> coursesReportsList = new List<CoursesReports>();
        public CourseReportsPage(PegasProjectPlanner.Users currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;

            if (db.Students.FirstOrDefault(p => p.ID_User == _currentUser.ID) != null)
                loadDataReports(_currentUser);
            else
                loadDataReports(null);

        }
        
        public void loadDataReports(PegasProjectPlanner.Users user = null)
        {
            if(user != null)
            {
                loadCoursesReports(user);
            }
            else 
            {
                List<Users> users = new List<Users>();
                foreach (var i in db.Users.ToList<Users>())
                    if (i.ID_Department == 2) users.Add(i);
                foreach (var k in users) loadCoursesReports(k);
            }
            CoursesItemsControl.ItemsSource = coursesReportsList;
            
        }

        public void loadCoursesReports(Users user)
        {
            List<Courses> courses = db.Courses.ToList<Courses>();
            foreach (var i in courses)
            {
                List<Tasks> tasks = new List<Tasks>();
                List<AnswerTasks> answerTasks = new List<AnswerTasks>();
                foreach (var k in db.Tasks.ToList<Tasks>())
                {
                    if (k.ID_Courses == i.ID)
                    {
                        tasks.Add(k);
                        foreach (var l in db.AnswerTasks.ToList<AnswerTasks>())
                        {
                            if (l.ID_Task == k.ID && l.ID_User == user.ID)
                            {
                                answerTasks.Add(l);
                            }
                        }
                    }
                }
                Students student = db.Students.FirstOrDefault(p => p.ID_User == user.ID);
                CoursesReports coursesReports = new CoursesReports(i.Title, $"{student.Surname} {student.Name} {student.Patronymic}", tasks.Count, answerTasks.Count);
                coursesReportsList.Add(coursesReports);
            }
        }


    }
}
