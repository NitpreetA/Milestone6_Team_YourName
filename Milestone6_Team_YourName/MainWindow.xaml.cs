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
using Budget;

namespace Milestone6_Team_YourName
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window, ViewInterface
    {
        private bool existing;
        public ApplicationTheme _theme = ApplicationTheme.Dark;
        internal Color _accent = Colors.Blue;
        private Presenter p;

        private static string budgetFolder = "Budgets";
        private string initialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), budgetFolder);

        private string openBudget = string.Empty;
        
        
        public MainWindow()
        {
            InitializeComponent();
            p = new Presenter(this);
            
            expenseDate.SelectedDate = DateTime.Now;
            ExpenseFieldState(false);

            
            PropertiesSet();
            PropertiesToTheme();
            LastOpenFile();         
                

            if (!Directory.Exists(initialDirectory))
            {
                Directory.CreateDirectory(initialDirectory);
            }
        }

        private void LastOpenFile()
        {
            string lastOpenDB = App.Current.Properties["LastOpenDB"].ToString();

            if (File.Exists(lastOpenDB))
            {
                var response = MessageBox.Show("You had a database open last time, would you like " +
                    "to reopen it?", "Database Relaunching", MessageBoxButton.YesNo);
                if (response == MessageBoxResult.Yes)
                {
                    currentBudgetFile.Text = lastOpenDB;
                    openBudget = currentBudgetFile.Text;
                    p.Connection(currentBudgetFile.Text, existing);
                    ExpenseFieldState(true);
                }
                else
                {
                    openBudget = lastOpenDB;
                }
            }
        }

        private void ExpenseFieldState(bool state)
        {
            expense.IsEnabled = state;
            description.IsEnabled = state;
            amount.IsEnabled = state;
            expenseDate.IsEnabled = state;
            categoryList.IsEnabled = state;
            btn_AddExpense.IsEnabled = state;
            btn_ClearExpense.IsEnabled = state;
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
                existing = false;
                currentBudgetFile.Text = openFileDialog.FileName;
                openBudget = currentBudgetFile.Text;
                p.Connection(currentBudgetFile.Text,existing);
                ExpenseFieldState(true);
            }
            
        }

        private void btn_AddExpense_Clck(object sender, RoutedEventArgs e)
        {
            bool errorWhileAddingAnExpense = false;
            // find a way to 

            //if

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
            categoryList.SelectedIndex = -1;
        }

        

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            SubmitFile.IsEnabled = true;
            budgetFileName.IsEnabled = true;

        }

        private void Existing_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SubmitFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitFile_Click_1(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (Directory.Exists(initialDirectory))
            {

                
                existing = true;
                p.Connection(initialDirectory + "\\" + budgetFileName.Text + ".db", existing);
                ExpenseFieldState(true);
                SubmitFile.IsEnabled = false;
            }
        }

        public void DiplayList(List<Category> categories)
        {
            categoryList.ItemsSource = categories;
        }

        private void PropertiesSet()
        {
            if (!App.Current.Properties.Contains("BackgroundColor"))
                App.Current.Properties.Add("BackgroundColor", Window.Background);

            if (!App.Current.Properties.Contains("AccentColor"))
                App.Current.Properties.Add("AccentColor", _accent);

            if (!App.Current.Properties.Contains("LastOpenDB"))
                App.Current.Properties.Add("LastOpenDB", "");
        }

        private void PropertiesToTheme()
        {
            string accent = App.Current.Properties["AccentColor"].ToString();
            string background = App.Current.Properties["BackgroundColor"].ToString();
            SetAccent(accent);
            SetBackground(background);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Properties["BackgroundColor"] = Window.Background;
            App.Current.Properties["AccentColor"] = _accent;
            App.Current.Properties["LastOpenDB"] = openBudget;
        }
    }
}
