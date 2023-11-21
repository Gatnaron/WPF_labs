using meeeh.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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

namespace meeeh
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public static SmartStoreEntities1 DataEntitiesEmployee { get; set; } = new SmartStoreEntities1();
        public ObservableCollection<Product> ListEmployee;
        public Page1()
        {
            InitializeComponent();
            ListEmployee = new ObservableCollection<Product>();
        }

        private bool isDirty = true;
        public static bool canSave = true;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetEmployee();
            DataGridEmployee.SelectedIndex = 0;
        }
        private void GetEmployee()
        {
            var queryEmployee = (from employee in DataEntitiesEmployee.Products.ToList()
                                 orderby employee.ID_P
                                 select employee).ToList();
            foreach (Product emp in queryEmployee)
            {
                ListEmployee.Add(emp);
                Console.WriteLine(emp.Name.ToString());
            }
            DataGridEmployee.ItemsSource = ListEmployee;
        }
        private void RewriteEmployee()
        {
            DataEntitiesEmployee = new SmartStoreEntities1();
            ListEmployee.Clear();
            GetEmployee();

        }
        private void UndoCommandBinding_Executed(object sender,
 ExecutedRoutedEventArgs e)
        {
            RewriteEmployee();
            DataGridEmployee.IsReadOnly = true;
            MessageBox.Show("Отмена");
            isDirty = true;
        }
        private void CutCommandBinding_Executed(object sender,
ExecutedRoutedEventArgs e)
        {
            DataGridEmployee.IsReadOnly = false;
            DataGridEmployee.BeginEdit();
            MessageBox.Show("Редактирование");
            isDirty = true;
        }
        private void FindCommandBinding_Executed(object sender,
ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Поиск по фильтру(ам)");
            FindByName(this.TextBox1.Text, this.TextBox2.Text, this.TextBox3.Text);
            isDirty = true;


        }
        private void FindByName(string name, string brand, string code)
        {
            DataEntitiesEmployee = new SmartStoreEntities1();
            ListEmployee.Clear();
            var queryEmployee = (from product in DataEntitiesEmployee.Products
                                 where product.Name.Contains(name)
                                 where product.Brand.Contains(brand)
                                 where product.code.Contains(code)
                                 select product).ToList();
            foreach (Product pd in queryEmployee)
            {
                ListEmployee.Add(pd);
            }
            if(ListEmployee.Count > 0)
            {
                DataGridEmployee.ItemsSource = ListEmployee;
            }
            else
            {
                MessageBox.Show("Товыры не найдены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                GetEmployee();
            }
        }
        private void NewCommandBinding_Executed(object sender,
ExecutedRoutedEventArgs e)
        {
            Product product = new Product();
            product.Name = "Название";
            product.Price = 0;
            product.Brand = "Фирма";
            product.ComProtocol = "ПротоколС";
            product.code = "КодТ";
            try {
                DataEntitiesEmployee.Products.AddOrUpdate(product);
                ListEmployee.Add(product);
                MessageBox.Show("Создание");
                isDirty = false;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    "Ошибка добавления данных" + ex.ToString());
            }
        }
        private void DeleteCommandBinding_Executed(object sender,
ExecutedRoutedEventArgs e)
        {
            Product product = DataGridEmployee.SelectedItem as Product;
                MessageBoxResult result = MessageBox.Show("Удалить данные", "Предупреждение", MessageBoxButton.OKCancel);
                if(result == MessageBoxResult.OK)
                {

                    DataEntitiesEmployee.Products.Remove(product);
                    DataEntitiesEmployee.SaveChanges();
                    DataGridEmployee.SelectedIndex = DataGridEmployee.SelectedIndex == 0 ? 1 :DataGridEmployee.SelectedIndex - 1;
                    ListEmployee.Remove(product);
                }
                else
                {
                    MessageBox.Show("Выберете строку для удаления");
                }
        }
        private void SaveCommandBinding_Executed(object sender,
ExecutedRoutedEventArgs e)
        {
            DataEntitiesEmployee.SaveChanges();
            MessageBox.Show("Сохранение");
            canSave = true;
            DataGridEmployee.IsReadOnly = true;
        }


        private void CutCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }
        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = canSave;
        }

        private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }
        private void NewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }

        private void UndoCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }
        private void FindCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }

        private void DataGridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
