using Microsoft.Win32;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PegasProjectPlanner
{
    /// <summary>
    /// Логика взаимодействия для ConcreteTaskPage.xaml
    /// </summary>
    public partial class ConcreteTaskPage : Page
    {
        PegasProjectPlanerEntities db = new PegasProjectPlanerEntities();
        Frame frame;
        PegasProjectPlanner.Tasks _currentTask;
        PegasProjectPlanner.Users _currentUser;
        PegasProjectPlanner.AnswerTasks _answerTasks;

        public ConcreteTaskPage(Frame frame = null, PegasProjectPlanner.Users currentUser = null, PegasProjectPlanner.Tasks currentTasks = null)
        {
            InitializeComponent();
            this.frame = frame;
            _currentTask = currentTasks;
            _currentUser = currentUser;
            if (_currentTask != null)
            {
                setTaskData();
                setAnswerData();
            }
            if (db.Students.FirstOrDefault(p => p.ID_User == _currentUser.ID) != null)
            {
                editTaskButton.Visibility = Visibility.Collapsed;
                deleteTaskButton.Visibility = Visibility.Collapsed;
            }
        }

        public void setTaskData()
        {
            titleTextBlock.Text = _currentTask.Title;
            descriptionTextBlock.Text = _currentTask.Description;
        }

        public void setAnswerData()
        {
            PegasProjectPlanner.AnswerTasks answer = new PegasProjectPlanner.AnswerTasks();
            answer = db.AnswerTasks.FirstOrDefault(p => p.ID_Task.ToString().Contains(_currentTask.ID.ToString()));
            if (answer == null)
            {
                changeButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
                addButton.Visibility = Visibility.Visible;
                attemptTextBlock.Text = "Нет попыток";
                responseStatusTextBlock.Text = "Ответ на задание еще не представлен";
                estimationTextBlock.Text = "Не оценено";
                dateTextBlock.Text = "Ответ на задание еще не представлен";
                fileTextBlock.Text = "-";
                descriptionAnswerTextBlock.Text = "Комментариев (0)";
            }
            else
            {
                _answerTasks = answer;
                changeButton.Visibility = Visibility.Visible;
                deleteButton.Visibility = Visibility.Visible;
                addButton.Visibility = Visibility.Collapsed;
                attemptTextBlock.Text = "Попытка №1";
                responseStatusTextBlock.Text = "Отправлен на оценивание";
                responseStatusBorder.Background = System.Windows.Media.Brushes.PaleGreen; 
                estimationTextBlock.Text = "Не оценено";
                dateTextBlock.Text = answer.DateCompletion;
                
                char delimiter = '\\';
                int index = answer.File.LastIndexOf(delimiter);
                string fileName = answer.File.Substring(index +1);
                fileTextBlock.Text = fileName;
                descriptionAnswerTextBlock.Text = answer.Text; 
            }
        }

        public void onClickButton(object sender, RoutedEventArgs e)
        {
            if(sender == addButton )
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Office Files|*.doc;*.xls;*.ppt;*.docx;*.pptx",
                    Title = "Выберите файл"
                };
                if(openFileDialog.ShowDialog() == true)
                {
                    string file = openFileDialog.FileName;
                    AnswerTasks answerTasks = new AnswerTasks();
                    if(_currentTask != null) answerTasks.ID_Task = _currentTask.ID;
                    if (_currentUser != null) answerTasks.ID_User = _currentUser.ID;
                    answerTasks.Text = "";
                    answerTasks.File = file; 
                    answerTasks.DateCompletion = DateTime.Now.Date.ToString();
                    db.AnswerTasks.Add(answerTasks);
                    saveChangedDB("Данные успешно добавлены!");
                    
                }
            }
            if (sender == changeButton)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Office Files|*.doc;*.xls;*.ppt;*.docx;*.pptx",
                    Title = "Выберите файл"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    string file = openFileDialog.FileName;
                    AnswerTasks answerTasks = _answerTasks;
                    answerTasks.File = file;
                    answerTasks.DateCompletion = DateTime.Now.Date.ToShortDateString();
                    saveChangedDB("Данные успешно изменены!");

                }
            }
            if (sender == deleteButton)
            {
                db.AnswerTasks.Remove(_answerTasks);
                saveChangedDB("Данные успешно удалены!");
            }
            if(sender == editTaskButton)
            {
                frame.NavigationService.Navigate(new AddChangeTask(frame, _currentUser, db.Courses.FirstOrDefault(p=>p.ID == _currentTask.ID_Courses), _currentTask));
            }
            if(sender == deleteTaskButton)
            {
                if(_currentTask != null) db.Tasks.Remove(db.Tasks.First(p => p.ID == _currentTask.ID));
                if (_answerTasks != null) db.AnswerTasks.Remove(_answerTasks);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");
                    frame.NavigationService.Navigate(new TasksPage(frame, _currentUser, db.Courses.First(p=>p.ID==_currentTask.ID_Courses)));
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }

            void saveChangedDB(string messageSucceful)
            {
                try
                {
                    db.SaveChanges();
                    MessageBox.Show(messageSucceful);
                    frame.NavigationService.Navigate(new ConcreteTaskPage(frame, _currentUser, _currentTask));
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }

        
    }
}
