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
    /// Логика взаимодействия для studentsListPage.xaml
    /// </summary>
    public partial class StudentsListPage : Page
    {
        PegasProjectPlanerEntities db = new PegasProjectPlanerEntities();
        public StudentsListPage()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            List<Students> students = db.Students.ToList<Students>();
            CoursesItemsControl.ItemsSource = students;
        }
    }
}
