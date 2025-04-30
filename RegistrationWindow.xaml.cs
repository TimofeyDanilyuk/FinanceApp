using FinanceLibrary.Data;
using FinanceLibrary.Models;
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
using System.Windows.Shapes;

namespace FinanceApp
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var context = new FinanceDbContext();

            string fullName = FullNameTextBox.Text;
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Все поля обязательны для заполнения", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool loginExists = context.Users.Any(u => u.Login == login);

            if (loginExists)
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newUser = new User
            {
                FIO = fullName,
                Login = login,
                Password = password,
                TelegramNumber = "0",
                TLChatId = 0,
                TLUserName = "0"
            };

            context.Users.Add(newUser);
            context.SaveChanges();

            MessageBox.Show("Регистрация прошла успешно!", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            var authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }
    }
}
