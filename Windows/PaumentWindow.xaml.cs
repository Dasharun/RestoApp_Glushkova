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
    /// Логика взаимодействия для PaumentWindow.xaml
    /// </summary>
    public partial class PaumentWindow : Window
    {
        public PaumentWindow()
        {
            InitializeComponent();

            ToPayTbl.Text = $"К оплате:" + App.selectedCheque.TotalCost.ToString() + "руб.";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.selectedCheque.IsClosed = true;
            App.selectedCheque.Table.IsFree = true;

            App.GetContext().SaveChanges();
            DialogResult = true;
            MessageBox.Show("Заказ успешно оплачен!");
            Close();
        }


        private void BankCostTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            BankCostTb.Text = App.selectedCheque.TotalCost.ToString();
        }

        private void CostTb_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeTbl.Text = GetChange();
        }
        

        private void CostTb_GotFocus(object sender, RoutedEventArgs e)
        {

        ChangeTbl.Text = GetChange();

        }

        private string GetChange()
        {
            if (CostTb.Text != string.Empty)
            {
                decimal chequeCash = App.selectedCheque.TotalCost;
                decimal cash = decimal.Parse(CostTb.Text);
                return "Сдача" + (cash - chequeCash).ToString() + "₽";
            }
            return "0";
        }
    }
}
