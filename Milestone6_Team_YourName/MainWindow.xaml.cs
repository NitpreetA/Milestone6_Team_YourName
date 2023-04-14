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
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using static Budget.Category;

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
        private bool createdNewCategory = false;



        public MainWindow()
        {
            InitializeComponent();
            p = new Presenter(this);
            
            expenseDate.SelectedDate = DateTime.Now;
            ExpenseFieldState(false);

            p.DisplayDefCatType();
                

            if (!Directory.Exists(initialDirectory))
            {
                Directory.CreateDirectory(initialDirectory);
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
            createCategory.IsEnabled = state;
            btn_CreateNewCategory.IsEnabled = state;
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
                p.Connection(currentBudgetFile.Text,existing);
                ExpenseFieldState(true);
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

        public void DisplayList(List<Category> categories)
        {
           // Category myCategory = new Category(); // i just need an instance of this to be able to add... 

            categoryList.ItemsSource = categories;

        

            if (createdNewCategory)
            {
                //categories.Add();
            }
            createdNewCategory = false;
        }


        private void btn_CreateNewCategory_Click(object sender, RoutedEventArgs e)
        {
            createdNewCategory = true;
            if (createCategory.Text == "")
            {
                MessageBox.Show("Must Input a name for the category you want to add.");
            }
            else
            {
                p.CreateCat(createCategory.Text, CategoryType.SelectedIndex);
                createCategory.Text = ""; // clear the textbox
            }
           


        }

        public void DisplayCatTypes(List<CategoryType> categoryTypes)
        {
            CategoryType.ItemsSource = categoryTypes;
            CategoryType.SelectedIndex = 1;
        }
    }
}
