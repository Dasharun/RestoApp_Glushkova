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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestoApp_Glushkova.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateChequePage.xaml
    /// </summary>
    public partial class CreateChequePage : Page
    {

        //Списки для работ с фильтрацией 
        List<Category> categories = new List<Category>();
        decimal totalCost;
        public CreateChequePage()
        {
            InitializeComponent();

            // Добавление позиций меню в ListBox     
            PositionLb.ItemsSource = App.GetContext().Position.ToList();

            //Добавление категории в ComboBox 
            categories = App.GetContext().Category.ToList();

            categories.Insert(0, new Category { Title = "Все категории" });
            FilterCmb.ItemsSource = categories;
            TableTbl.DataContext = App.selectedTable;
            DateTbl.Text = " Открыт" + DateTime.Now.ToString();

        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
           

            if (SearchTb.Text != string.Empty)
            {
                PositionLb.ItemsSource = App.GetContext().Position.Where(p => p.Title.Contains(SearchTb.Text)).ToList();
            }
            else
            {
                PositionLb.ItemsSource = App.GetContext().Position.ToList();
            }
        }



        private void PositionsLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PositionsLv.Items.Remove(PositionsLv.SelectedItem as Position);
            TotalCostTbl.Text = GetTotalCost();
        }

        private string GetTotalCost()
        {
             totalCost = 0;

            foreach (Position position in PositionsLv.Items)
            {
                totalCost += position.Cost;
            }

            return "К оплате: " + totalCost + " ₽";
        }

        private void PositionLb_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            App.selectedPosition = PositionLb.SelectedItem as Position;
            if (App.selectedPosition != null)
            {
                PositionsLv.Items.Add(App.selectedPosition);
                TotalCostTbl.Text = GetTotalCost();
            }
            PositionLb.SelectedIndex = -1;
        }

        private void CreateChequeBtn_Click(object sender, RoutedEventArgs e)
        { 

            Cheque newCheque = new Cheque
        {
            Title =GenerateChequeNumber(),
                TotalCost = totalCost,
                IsClosed = false,
                OpeningDate = DateTime.Now,
                WaiterId = App.enteredWaiter.WaiterId,
                TableId = App.selectedTable.TableId
        };

            if (App.selectedTable.IsFree)
            {
                // Изменение статуса стола
               foreach (Table table in App.GetContext().Table.ToList())
                {
                    if (table.Equals(App.selectedTable))
                    {
                        table.IsFree = false;
                    }
                }
                App.GetContext().Cheque.Add(newCheque);
                App.GetContext().SaveChanges();

            }
            else
            {
                MessageBox.Show("Этот стол уже занят! Чек создан");
            }
            foreach (Position position in PositionsLv.Items)
            {
                ChequePosition chequePosition = new ChequePosition
                {
                    ChequeId = newCheque.ChequeId,
                    PositionId = position.PositionId,
                };

            App.GetContext().ChequePosition.Add(chequePosition);
            App.GetContext().SaveChanges();
            }
            MessageBox.Show("Чек успешно создан!");
        }

        private void FilterCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (FilterCmb.SelectedIndex != 0)
            {
                PositionLb.ItemsSource = App.GetContext().Position.Where(p => p.Category.CategoryId == FilterCmb.SelectedIndex).ToList();
            }
            else 
            {
                PositionLb.ItemsSource = App.GetContext().Position.ToList();
            }
        }

        private void CategoryCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TotalCostTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PayBtn_Click(object sender, RoutedEventArgs e)
        {

        }


        private void PositionLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.selectedPosition = PositionLb.SelectedItem as Position;
            if (App.selectedPosition != null)
            {
                PositionsLv.Items.Add(App.selectedPosition);

                TotalCostTbl.Text = GetTotalCost();
            }
            PositionLb.SelectedItem = -1;
        }
        private void Page_Loaded(object sender, SelectionChangedEventArgs e)
        {
            PositionLb.ItemsSource = App.GetContext().Position.ToList();
        }

        private void PositionBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPositionWindow addPositionWindow = new AddPositionWindow();
            addPositionWindow.ShowDialog();
        }


        private string GenerateChequeNumber()
        {
            DateTime current = DateTime.Now;
            return $"Чек №{current.Day}{current.Month}{current.Year}-{current.Hour}{current.Minute}";

        }
    }
}
