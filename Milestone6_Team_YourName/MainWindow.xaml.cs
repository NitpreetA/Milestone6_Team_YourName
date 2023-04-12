using ModernWpf;
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
using System.IO;
using Microsoft.Win32;

namespace Milestone6_Team_YourName
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ApplicationTheme _theme = ApplicationTheme.Dark;
        public Color _accent = Colors.Blue;
         

        private static string budgetFolder = "Budgets";
        private string initialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), budgetFolder);
        
        
        public MainWindow()
        {
            InitializeComponent();
            expenseDate.SelectedDate = DateTime.Now;
      

            if (!Directory.Exists(initialDirectory))
            {
                Directory.CreateDirectory(initialDirectory);
            }
        }

        private void btn_closePage(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOpenBudgetFileLocation(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = initialDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
            }
            
        }

        private void btn_AddExpense_Clck(object sender, RoutedEventArgs e)
        {
            bool errorWhileAddingAnExpense = false;
            // find a way to 

            if(string.IsNullOrEmpty(expense.Text) || string.IsNullOrEmpty(description.Text) || string.IsNullOrEmpty(amount.Text) )
            {
                errorWhileAddingAnExpense = true;
            }
            if (errorWhileAddingAnExpense)
            {
                MessageBox.Show("Please fill out all of the input fields");
            }
            else
            {
                MessageBox.Show("Expense was successfully added");
                expense.Text = string.Empty;
                description.Text = string.Empty;
                amount.Text = string.Empty;
                //date.DataContext = DateTime.Now;
                // find a way to add this to the expense list and have a display expenses button. 
            }
        }

        private void btn_ClearExpense_Clck(object sender, RoutedEventArgs e)
        {
            expense.Text = string.Empty;
            description.Text = string.Empty;
            amount.Text = string.Empty;
        }

        //-------------------------------------------------
        //                   Accents Menu
        //-------------------------------------------------

        private void AccentBtnRed_Click(object sender, RoutedEventArgs e)
        {
            _accent = Colors.Red;
            ThemeManager.Current.AccentColor = _accent;
        }

        private void AccentBtnBlue_Click(object sender, RoutedEventArgs e)
        {
            _accent = Colors.Blue;
            ThemeManager.Current.AccentColor = _accent;
        }
        
        private void AccentBtnYellow_Click(object sender, RoutedEventArgs e) => SetAccent("#FFB900");

        private void AccentBtnOrange_Click(object sender, RoutedEventArgs e) => SetAccent("#F7630C");

        private void AccentBtnPink_Click(object sender, RoutedEventArgs e) => SetAccent("#E3008C");

        private void AccentBtnLavender_Click(object sender, RoutedEventArgs e) => SetAccent("#615FAE");

        private void AccentBtnBlueGreen_Click(object sender, RoutedEventArgs e) => SetAccent("#00B294");

        private void SetAccent(string colorCode)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colorCode);
            _accent = color;
            ThemeManager.Current.AccentColor = _accent;
        }

        //-------------------------------------------------
        //                  Background Menu
        //-------------------------------------------------

        private void BgBtnDefaultDark_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Colors.Black);
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            Window.Background = brush;

        }

        private void BgBtnDefaultLight_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Colors.White);
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            Window.Background = brush;
        }

        private void BgBtnBlue_Click(object sender, RoutedEventArgs e) => SetBackground("#1DA9E2");

        private void BgBtnSalmon_Click(object sender, RoutedEventArgs e) => SetBackground("#FA8072");

        private void BgBtnEmerald_Click(object sender, RoutedEventArgs e) => SetBackground("#28643C");

        private void BgBtnLavender_Click(object sender, RoutedEventArgs e) => SetBackground("#615FAE");

        private void BgBtnCrimson_Click(object sender, RoutedEventArgs e) => SetBackground("#9A0E2A");
        
        private void SetBackground(string colorCode)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colorCode);
            Brush brush = new SolidColorBrush(color);

            Window.Background = brush;
        }

    }
}
