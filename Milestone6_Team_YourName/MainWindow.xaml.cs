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

        private void btn_AddExpense(object sender, RoutedEventArgs e)
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

        private void btn_ClearExpense(object sender, RoutedEventArgs e)
        {
            expense.Text = string.Empty;
            description.Text = string.Empty;
            amount.Text = string.Empty;
        }

        private void btn_ChangeTheme(object sender, RoutedEventArgs e)
        {
           //Using the ModernWPF library, change the dark theme's colors to the following:
           //Background: Red
           //Foreground: Blue
           //Accent Color: Green
           //After changing the dark theme's colors, change the window's theme to dark.

            ColorPaletteResources dark = new ColorPaletteResources();
            dark. = new SolidColorBrush(Colors.Red);


        }
    }
}
