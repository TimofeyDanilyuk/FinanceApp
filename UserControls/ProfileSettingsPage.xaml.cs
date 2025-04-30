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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinanceApp.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ProfileSettingsPage.xaml
    /// </summary>
    public partial class ProfileSettingsPage : UserControl
    {
        private User _currentUser;

        public ProfileSettingsPage(User user)
        {
            InitializeComponent();
            _currentUser = user;
            LoadUserData();
        }

        private void LoadUserData()
        {
            LoginTextBox.Text = _currentUser.Login;
            PasswordBox.Password = _currentUser.Password;
            FIOTextBox.Text = _currentUser.FIO;
            TelegramNumberTextBox.Text = _currentUser.TelegramNumber;
            TLUserNameTextBox.Text = _currentUser.TLUserName;
            TLChatIdTextBox.Text = _currentUser.TLChatId.ToString();
        }

        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Text = PasswordBox.Password;
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = PasswordTextBox.Text;
            PasswordBox.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password) ||
                string.IsNullOrWhiteSpace(FIOTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new FinanceDbContext())
            {
                var user = context.Users
                    .Where(x=>x.Id == _currentUser.Id)
                    .FirstOrDefault();

                user.Login = LoginTextBox.Text;
                user.Password = PasswordBox.Password;
                user.FIO = FIOTextBox.Text;
                user.TelegramNumber = TelegramNumberTextBox.Text;
                user.TLUserName = TLUserNameTextBox.Text;

                context.SaveChanges();
            }

            MessageBox.Show("Данные успешно сохранены", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
