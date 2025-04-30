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
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginTextBox.Text;

            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Создаем контекст базы данных
                using (var db = new FinanceDbContext())
                {

                    bool userExists = db.Users.Any(u => u.Login == login);

                    if (!userExists)
                    {
                        MessageBox.Show("Пользователь не найден. Пожалуйста, зарегистрируйтесь.",
                                      "Ошибка входа",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Information);

                        var registrationWindow = new RegistrationWindow();
                        registrationWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь найден. Можете войти.",
                                      "Успешно",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Information);

                        // var mainWindow = new MainWindow();
                        // mainWindow.Show();
                        // this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
