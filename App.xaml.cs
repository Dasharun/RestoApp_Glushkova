using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RestoApp_Glushkova.Model;

namespace RestoApp_Glushkova
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static RestoDb_GlushkovaEntities _context;

        // Вошедший официан
        public static Waiter enteredWaiter;

        // выбранный стол

        public static Table selectedTable;
        //выбранная позиция

        public static Position selectedPosition;

        public static Cheque selectedCheque;
       

        public static RestoDb_GlushkovaEntities GetContext()
        {
            if (_context == null)
            {
                _context = new RestoDb_GlushkovaEntities();
            }
            return _context;
        }

    }
}
