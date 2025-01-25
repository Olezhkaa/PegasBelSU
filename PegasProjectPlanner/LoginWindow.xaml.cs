﻿using System;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        PegasProjectPlanerEntities db = new PegasProjectPlanerEntities(); 
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = db.Users.FirstOrDefault(p => p.Login == LoginTextBox.Text && p.Password == PasswordBox.Password);
            if (LoginTextBox.Text.Equals("") ||  PasswordBox.Password.Equals(""))
            {
                MessageBox.Show("Заполните все поля");
            }
            else if (currentUser == null)
            {
                MessageBox.Show("Введены не правильные данные");
            }
            else
            {
                var mainWindow = new MainWindow(currentUser);
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
