using FinanceApp.UserControls;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _currentUser;

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            LoadDashboard();
        }
        private void SetActiveButton(Button activeButton)
        {
            var buttons = new[] { DashboardBtn, TransactionsBtn, CategoriesBtn,
                                BudgetsBtn, ReportsBtn, ProfileSettingsBtn };

            foreach (var button in buttons)
            {
                button.Style = (Style)FindResource("NavButtonStyle");
            }

            activeButton.Style = (Style)FindResource("ActiveNavButtonStyle");
        }

        private void LoadDashboard()
        {
            MainContent.Content = new DashboardPage();
            SetActiveButton(DashboardBtn);
        }

        private void DashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadDashboard();
        }

        private void TransactionsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CategoriesBtn_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void BudgetsBtn_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void ReportsBtn_Click(object sender, RoutedEventArgs e)
        {
        
        }

        private void ProfileSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ProfileSettingsPage(_currentUser);
            SetActiveButton(ProfileSettingsBtn);
        }
    }
}
