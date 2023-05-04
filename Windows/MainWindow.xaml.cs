using RestoApp_Glushkova.Pages;
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

namespace RestoApp_Glushkova
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WaiterTbl.DataContext = App.enteredWaiter;
            // 1. Обращаемся к элементу по имени
            //2. осуществляет переход к странице
            //3. Внутри метода навигате иницилизируем страницу 
            MainFrm.Navigate(new ChequePage());
        }

        private void GoBackBth_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrm.CanGoBack)
            {
                MainFrm.GoBack();
            }
        }
    }
}
