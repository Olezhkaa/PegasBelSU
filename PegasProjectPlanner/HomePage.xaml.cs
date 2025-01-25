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
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        PegasProjectPlanerEntities db = new PegasProjectPlanerEntities();
        PegasProjectPlanner.Users _currentUser;
        PegasProjectPlanner.Students _currentStudent;
        PegasProjectPlanner.Teatchers _currentTeatcher;
        public HomePage(PegasProjectPlanner.Users currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
            if(_currentUser.ID_Department == 2)
            {
                _currentStudent = db.Students.FirstOrDefault(p => p.ID_User == _currentUser.ID);
                hiTextBlock.Text = $"Здравствуйте, {_currentStudent.Name} {_currentStudent.Patronymic}! 👋 ";
            }
            else if (_currentUser.ID_Department == 1)
            {
                _currentTeatcher = db.Teatchers.FirstOrDefault(p => p.ID_User == _currentUser.ID);
                hiTextBlock.Text = $"Здравствуйте, {_currentTeatcher.Name} {_currentTeatcher.Patronymic}! 👋 ";
            }
            else
            {
                hiTextBlock.Text = $"Здравствуйте, {_currentUser.Login}! 👋 ";
            }
            
        }

    }
}
