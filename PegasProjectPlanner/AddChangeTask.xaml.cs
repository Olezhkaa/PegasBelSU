using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для AddChangeTask.xaml
    /// </summary>
    public partial class AddChangeTask : Page
    {
        PegasProjectPlanerEntities db = new PegasProjectPlanerEntities();
        PegasProjectPlanner.Tasks _currentTask;
        PegasProjectPlanner.Users _currentUser;
        PegasProjectPlanner.Courses _currentCourse;
        Frame frame;
        public AddChangeTask(Frame frame, PegasProjectPlanner.Users currentUser = null, PegasProjectPlanner.Courses currentCourse = null, PegasProjectPlanner.Tasks currentTask = null)
        {
            InitializeComponent();
            _currentTask = currentTask;
            _currentUser = currentUser;
            _currentCourse = currentCourse;
            this.frame = frame;
            if (_currentTask != null)
            {
                loadDataTask();
            }
            else
            {
                dateRecordTextBox.Text = DateTime.Now.Date.ToShortDateString();
                dateTheEndTextBox.Text = DateTime.Now.Date.ToShortDateString();
            }

        }

        public void loadDataTask()
        {
            titleTextBox.Text = _currentTask.Title;
            descriptionTextBox.Text = _currentTask.Description;
            dateRecordTextBox.Text = _currentTask.DateRecording;
            dateTheEndTextBox.Text = _currentTask.DateTheEnd;
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if(titleTextBox.Text.Trim() == "" || descriptionTextBox.Text.Trim() == "" || dateRecordTextBox.Text.Trim() == "" || dateTheEndTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Заполните все необходимые поля");
            }
            else
            {
                if (_currentTask == null)
                {
                    PegasProjectPlanner.Tasks task = new PegasProjectPlanner.Tasks();
                    task.Title = titleTextBox.Text;
                    task.Description = descriptionTextBox.Text;
                    task.ID_Courses = _currentCourse.ID;
                    task.ID_User = _currentUser.ID;
                    task.DateRecording = dateRecordTextBox.Text;
                    task.DateTheEnd = dateTheEndTextBox.Text;
                    db.Tasks.Add(task);
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Данные успешно добавлены");
                        frame.NavigationService.Navigate(new TasksPage(frame, _currentUser, _currentCourse));
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
                else
                {
                    Tasks task = _currentTask;
                    task.Title = titleTextBox.Text;
                    task.Description = descriptionTextBox.Text;
                    task.DateTheEnd = dateTheEndTextBox.Text;
                    db.Tasks.AddOrUpdate(task);
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Данные успешно изменены");
                        frame.NavigationService.Navigate(new TasksPage(frame, _currentUser, _currentCourse));
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
            }
            
        }
    }
}
