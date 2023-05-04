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
    /// Логика взаимодействия для AddPositionWindow.xaml
    /// </summary>
    public partial class AddPositionWindow : Window
    {
        public AddPositionWindow()
        {
            InitializeComponent();
            PositionCmb.ItemsSource = App.GetContext().Category.ToList();
        }
    
    private void CreateChequeBtn_Click(object sender, RoutedEventArgs e)
    {
       
    }
    private void TitleTb_TextChanged(object sender, RoutedEventArgs e)
    {
    }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Position newPosition = new Position
            {
                Title = PositionNameTb.Text,

                Cost = Convert.ToDecimal(CostTb.Text),

                CategoryId = PositionCmb.SelectedIndex + 1
            };

            App.GetContext().Position.Add(newPosition);

            App.GetContext().SaveChanges();

            MessageBox.Show("Позиция успешно создана");

            Close();
        }

        private void CostTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}