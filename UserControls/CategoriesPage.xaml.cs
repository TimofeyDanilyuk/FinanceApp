using FinanceLibrary.Data;
using FinanceLibrary.Enum;
using FinanceLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : UserControl, INotifyPropertyChanged
    {
        private string _categoryName;
        private string _selectedTransactionType;
        private ObservableCollection<Category> _categories;
        private readonly User currentUser;

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        public string SelectedTransactionType
        {
            get => _selectedTransactionType;
            set
            {
                _selectedTransactionType = value;
                OnPropertyChanged(nameof(SelectedTransactionType));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CategoriesPage(User user)
        {
            InitializeComponent();
            currentUser = user;
            DataContext = this;
            LoadCategories();
        }

        private async void LoadCategories()
        {
            try
            {
                using (var context = new FinanceDbContext())
                {
                    var categories = await context.Categories
                        .Where(c => c.UserId == currentUser.Id)
                        .ToListAsync();

                    Categories = new ObservableCollection<Category>(categories);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                MessageBox.Show("Введите название категории!");
                return;
            }

            if (string.IsNullOrEmpty(SelectedTransactionType))
            {
                MessageBox.Show("Выберите тип транзакции!");
                return;
            }

            try
            {
                using (var context = new FinanceDbContext())
                {
                    var transactionType = SelectedTransactionType == "Доход"
                        ? TransactionType.Income
                        : TransactionType.Expense;

                    var user = context.Users.Find(currentUser.Id);

                    var category = new Category
                    {
                        Name = CategoryName,
                        Type = transactionType,
                        UserId = user.Id,
                        User = user
                    };

                    context.Entry(category.User).State = EntityState.Unchanged;

                    context.Categories.Add(category);
                    context.SaveChanges();

                    Categories.Add(category);
                    CategoryName = string.Empty;
                    SelectedTransactionType = null;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : ex.Message;

                MessageBox.Show($"Ошибка: {errorMessage}");
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesDataGrid.SelectedItem is Category selectedCategory)
            {
                try
                {
                    using (var context = new FinanceDbContext())
                    {
                        context.Categories.Attach(selectedCategory);
                        context.Categories.Remove(selectedCategory);
                        context.SaveChanges();
                        Categories.Remove(selectedCategory);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}");
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
