using RestoApp_Glushkova.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestoApp_Glushkova.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChequePage.xaml
    /// </summary>
    public partial class ChequePage : Page
    {
        public ChequePage()
        {
            InitializeComponent();

            TablesLb.ItemsSource = App.GetContext().Table.ToList();

            OpenChequesLb.ItemsSource = App.GetContext().Cheque.Where(c=>c.IsClosed==false).ToList();

        }

        private void TablesLb_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            App.selectedTable = TablesLb.SelectedItem as Table;
            if (App.selectedTable.IsFree)
            {
                NavigationService.Navigate(new CreateChequePage());
            }
            else
            {
                MessageBox.Show("Стол занят. Используйте меню\"Открытые чеки\"для его изменения");
            }
        }

        private void OpenChequesLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.selectedCheque = OpenChequesLb.SelectedItem as Cheque;
            if (App.enteredWaiter.WaiterId == App.selectedCheque.WaiterId)
            {
                NavigationService.Navigate(new EditChequePage());
            }
            else
            {
                MessageBox.Show("Вы можее редактировать только свои чеки !");
            }
        }
    }
}
