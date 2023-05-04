using RestoApp_Glushkova.Model;
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

namespace RestoApp_Glushkova.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void PincodeBth_Click(object sender, RoutedEventArgs e)
        {
            PincodePb.Password += (sender as Button).Content.ToString();
            if (PincodePb.Password.Length == 4)
            {
            //Процесс автрризации

            // 1 Найти нужный объект из таблицы по условию
            Waiter waiter = App.GetContext().Waiter.FirstOrDefault(w => PincodePb.Password == w.Pincode);

                //2 реализовать условие
                if (waiter != null)

            {
                    App.enteredWaiter = waiter;
                MainWindow mainWindow= new MainWindow(); //инициализирует окно
                mainWindow.Show();// jnrhsdftn jryj
                Close(); // закрывает тек окно
            }

            else
            {
                MessageBox.Show("Неправильный пароль");
                PincodePb.Clear(); // очищает PasswordBox
            }
            }
        }

        private void DeletePincodeBth_Click(object sender, RoutedEventArgs e)
        {
         if (PincodePb.Password.Length>0)
            {
                //удаление символов с конца 
                // возвращает индекс последнего символа в строке 
                PincodePb.Password = PincodePb.Password.Remove(PincodePb.Password.Length - 1);
            }

        }
    }
}

