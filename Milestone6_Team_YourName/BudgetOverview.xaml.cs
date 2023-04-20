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

namespace Milestone6_Team_YourName
{
    /// <summary>
    /// Interaction logic for BudgetOverview.xaml
    /// </summary>
    public partial class BudgetOverview : Window
    {
        public BudgetOverview()
        {
            InitializeComponent();
        }

        private class Bud
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Type { get; set; }
            public DateOnly Date { get ; set; }
            public double Amount { get; set; }

            public Bud(int id, string name, string cat, string type, DateOnly date, double amount)
            {
                ID = id;
                Name = name;
                Category = cat;
                Type = type;
                Date = date;
                Amount = amount;
            }
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var Budgets = new List<Bud>();
            
            for(int i = 1; i < 7; i++)
            {
                Bud newBudget = new Bud(i, "Test", "Category", "Cat Type", DateOnly.FromDateTime(DateTime.Now), i * 15.5);
                Budgets.Add(newBudget);
            }

            var grid = sender as DataGrid;
            grid.ItemsSource = Budgets;
        }
    }
}
