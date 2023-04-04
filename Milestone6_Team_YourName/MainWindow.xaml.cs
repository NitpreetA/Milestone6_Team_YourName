using Microsoft.Win32;
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

namespace Milestone6_Team_YourName
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string budgetFolder = "Budgets";
        private string initialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), budgetFolder);
        public MainWindow()
        {
            InitializeComponent();
      

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
            int amountInNumericalValues = 0;
            bool properAmountInputField;
            // find a way to 

            properAmountInputField = int.TryParse(amount.Text, out amountInNumericalValues);

            if (!properAmountInputField)
                errorWhileAddingAnExpense = true;
            else
                errorWhileAddingAnExpense = false;

            if (string.IsNullOrEmpty(expense.Text) || string.IsNullOrEmpty(description.Text) || errorWhileAddingAnExpense )
            {
              
                errorWhileAddingAnExpense = true;
            }
            if (errorWhileAddingAnExpense)
            {
                MessageBox.Show("Please fill out all of the form while respecting the input fields data types");
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
    }
}
