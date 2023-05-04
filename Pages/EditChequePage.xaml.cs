using RestoApp_Glushkova.Model;
using RestoApp_Glushkova.Windows;
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

namespace RestoApp_Glushkova.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditChequePage.xaml
    /// </summary>
    public partial class EditChequePage : Page
    {
        //Итоговая цена
        private decimal totalCost;
        //Выбранная позиция
        private Position _selectedPosition;

        private List<Category> categories;
        private List<Position> positions;

        public EditChequePage()
        {
            InitializeComponent();

            totalCost = App.selectedCheque.TotalCost;
            TotalCostTbl.Text = GetTotalCost(PositionsLV);
            //Добавление информации о столе и времени
            TableTbl.Text = App.selectedCheque.Table.Title;
            DateTbl.Text = "Открыт: " + App.selectedCheque.OpeningDate;
            //Добавление позиций в ListBox
            EarlierPositionsLV.ItemsSource = App.GetContext().ChequePosition.Where(cp => cp.ChequeId == App.selectedCheque.ChequeId).ToList();
            //Добавленик категорий в  ComboBox
            categories = App.GetContext().Category.ToList();
            categories.Insert(0, new Category { Title = "Все категории" });
            CategoryCmb.ItemsSource = categories;
            CategoryCmb.SelectedValuePath = "CategoryId";
            CategoryCmb.DisplayMemberPath = "Title";
            CategoryCmb.SelectedIndex = 0;
        }

        private void CategoryCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            positions = App.GetContext().Position.ToList();

            if (CategoryCmb.SelectedIndex != 0)
            {
                positions = positions.Where(p => p.Category.CategoryId == CategoryCmb.SelectedIndex).ToList();
                PositionsLB.ItemsSource = positions;
            }
            else
            {
                PositionsLB.ItemsSource = positions;
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            positions = App.GetContext().Position.ToList();

            if (SearchTb.Text != string.Empty)
            {
                if (SearchTb.Text != "Поиск по названию")
                {
                    positions = positions.Where(p => p.Title.ToLower().Contains(SearchTb.Text.ToLower())).ToList();
                    PositionsLB.ItemsSource = positions;
                }
            }
            else
            {
                PositionsLB.ItemsSource = positions;
            }
        }

        private void PositionsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPosition = PositionsLB.SelectedItem as Position;

            if (_selectedPosition != null)
            {
                PositionsLV.Items.Add(_selectedPosition);
                TotalCostTbl.Text = GetTotalCost(PositionsLV);
            }

            PositionsLB.SelectedItem = -1;
        }

        private void PositionsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PositionsLV.Items.Remove((sender as ListView).SelectedItem as Position);
            TotalCostTbl.Text = GetTotalCost(PositionsLV);
        }


        private void EditChequeBth_Click(object sender, RoutedEventArgs e)
        {
            App.selectedCheque.TotalCost = totalCost;

            foreach (Position position in PositionsLV.Items)
            {
                ChequePosition chequePosition = new ChequePosition
                {
                    ChequeId = App.selectedCheque.ChequeId,
                    PositionId = position.PositionId,
                };
                App.GetContext().ChequePosition.Add(chequePosition);
                App.GetContext().SaveChanges();

                
            }

            MessageBox.Show("Новые позиции добавлены в чек!");
        }

        private void PayBtn_Click(object sender, RoutedEventArgs e)
        {
            PaumentWindow paymentWindow = new PaumentWindow();
            paymentWindow.ShowDialog();

            if (paymentWindow.DialogResult == true)
           {
                NavigationService.Navigate(new ChequePage());
            }
        }
        private string GetTotalCost(ListView positionsLv)
        {
            totalCost = App.selectedCheque.TotalCost;

            foreach (Position position in positionsLv.Items)
            {
                totalCost += position.Cost;
            }
            return "К оплате: " + totalCost.ToString() + "₽";
        }
    }
}
