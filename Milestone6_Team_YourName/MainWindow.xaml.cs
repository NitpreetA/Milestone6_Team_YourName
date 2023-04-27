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
using ModernWpf.Controls;
using System.Diagnostics.Eventing.Reader;
using System.Collections;

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
        private Presenter presenter;

        private static string budgetFolder = "Budgets";
        private string initialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), budgetFolder);

        private string openBudget = string.Empty;

        private string lastExpense;
        private string lastDescription;
        private string lastAmount;
        public int expenseId;


        private bool createdNewCategory = false;

        public MainWindow()
        {
            InitializeComponent();
            presenter = new Presenter(this);


            expenseDate.SelectedDate = DateTime.Now;

            ExpenseFieldState(false);
            
            PropertiesSet();
            PropertiesToTheme();
            LastOpenFile();
            
            presenter.DisplayDefCatType();
                
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
                    presenter.Connection(currentBudgetFile.Text, existing);
                    ExpenseFieldState(true);
                }
                else
                {
                    openBudget = "";
                }
            }
        }
        /// <summary>
        /// Enables and disables the fields accordingly based on if a file was selected or not.
        /// </summary>
        /// <param name="state">holds if the field should be active or not</param>
        private void ExpenseFieldState(bool state)
        {
            description.IsEnabled = state;
            amount.IsEnabled = state;
            expenseDate.IsEnabled = state;
            categoryList.IsEnabled = state;
            btn_AddExpense.IsEnabled = state;
            btn_ClearExpense.IsEnabled = state;
            createCategory.IsEnabled = state;
            btn_CreateNewCategory.IsEnabled = state;
            filterByCategory.IsEnabled = state;
            filterByMonth.IsEnabled = state;
            expenseGrid.IsEnabled = state;
            CategoryType.IsEnabled = state;
            filterBySpecificCategory.IsEnabled = state;
            filterStartDate.IsEnabled = state;
            filterEndDate.IsEnabled = state;
            filterFlag.IsEnabled = state;
        }
        /// <summary>
        /// Closes the page once button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_closePage(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Opens a budget file directory to allow user to select a budget file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenBudgetFileLocation(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = initialDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                existing = false;
                currentBudgetFile.Text = openFileDialog.FileName;
                openBudget = currentBudgetFile.Text;
                presenter.Connection(currentBudgetFile.Text,existing);
                ExpenseFieldState(true);
            }
            
        }

        /// <summary>
        /// Adds Expense once all the proper fields are inputted correctly 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AddExpense_Clck(object sender, RoutedEventArgs e)
        {     
            presenter.AddExpense(description.Text, amount.Text, expenseDate.ToString(), categoryList.SelectedIndex);   
        }

        /// <summary>
        /// Clears all inputted fields and resets them to default/starting position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClearExpense_Clck(object sender, RoutedEventArgs e)
        {
            description.Text = string.Empty;
            amount.Text = string.Empty;
            categoryList.SelectedIndex = -1;
            expenseDate.SelectedDate = DateTime.Now;
        }

        
        /// <summary>
        /// Enables the optioons to input a new file once the radio button was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            SubmitFile.IsEnabled = true;
            budgetFileName.IsEnabled = true;

        }

        private void SubmitFile_Click_1(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (Directory.Exists(initialDirectory))
            {
                existing = true;
                presenter.Connection(initialDirectory + "\\" + budgetFileName.Text + ".db", existing);
                ExpenseFieldState(true);
                SubmitFile.IsEnabled = false;
            }
        }

        public void DisplayList(List<Category> categories)
        {

            categoryList.ItemsSource = categories;
            filterBySpecificCategory.ItemsSource = categories; // NITPREET

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
                presenter.CreateCat(createCategory.Text, CategoryType.SelectedIndex);
                createCategory.Text = ""; // clear the textbox
            }
           
        }

        public void DisplayCatTypes(List<CategoryType> categoryTypes)
        {
            CategoryType.ItemsSource = categoryTypes;
            CategoryType.SelectedIndex = 1;
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
            if(! string.IsNullOrEmpty(description.Text) || ! string.IsNullOrEmpty(amount.Text))
            {
                var response = MessageBox.Show("You have unsaved changes. Are you sure you'd " +
                    "like to close the application?", "Closed Changes", MessageBoxButton.YesNo);
                if(response == MessageBoxResult.No) {
                    e.Cancel = true;
                    return; 
                }
            }
            App.Current.Properties["BackgroundColor"] = Window.Background;
            App.Current.Properties["AccentColor"] = _accent;
            App.Current.Properties["LastOpenDB"] = openBudget;
        }
        private void filterByCategory_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }
        public void Filter()
        {
            string start = filterStartDate.ToString();
            string end = filterEndDate.ToString();
            DateTime? startDate;
            DateTime? endDate;

            if (start != string.Empty)
                startDate = DateTime.Parse(start);
            else
            {
                startDate = null;
            }

            if (end != string.Empty)
                endDate = DateTime.Parse(end);
            else
            {
                endDate = null;
            }

            presenter.DisplayBudgetItemsFilter((bool)filterByMonth.IsChecked, (bool)filterByCategory.IsChecked, startDate, endDate, (bool)filterFlag.IsChecked, filterBySpecificCategory.SelectedIndex);
        }
        private void MenuItem_ModifyClick(object sender, RoutedEventArgs e)
        {
            ExpenseWindow expenseWindow = new ExpenseWindow(presenter);
            presenter.IntializeViewExpenseInterface(expenseWindow);
            expenseWindow.Background = Window.Background;
            expenseWindow.Show();

            if (expenseGrid.SelectedItem != null)
            {
                Budget.BudgetItem budgetItemToModify = (Budget.BudgetItem)(expenseGrid.SelectedItem);
                expenseWindow.Background = Window.Background;
                expenseWindow.expenseId = budgetItemToModify.ExpenseID;

                //Unsure if edit window should populate with information from the get-go or not -M
                //expenseWindow.PopulateFields(budgetItemToModify.ShortDescription, budgetItemToModify.Amount.ToString(), budgetItemToModify.Date, budgetItemToModify.CategoryID);
                expenseWindow.Show();

                presenter.ModifyExpense(budgetItemToModify.ExpenseID, budgetItemToModify.Date, budgetItemToModify.CategoryID - 1, budgetItemToModify.Amount, budgetItemToModify.ShortDescription);


            }
            Filter();

        }

        private void MenuItem_DeleteClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("inside delete");
            if (expenseGrid.SelectedItem != null)
            {
                Budget.BudgetItem budgetItemToDelete = (Budget.BudgetItem)(expenseGrid.SelectedItem);
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    presenter.DeleteExpense(budgetItemToDelete.ExpenseID);
                    Filter();
                }
            }
        }

        private void filterByMonth_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        public void DisplayBudgetItemsByMonth(List<BudgetItemsByMonth> budgetByMonth)
        {
            DeleteButton.IsEnabled = false;
            ModifyButton.IsEnabled = false;
            expenseGrid.Columns.Clear();
            expenseGrid.ItemsSource = budgetByMonth;
            expenseGrid.Columns.Clear();
            var col = new DataGridTextColumn();
            col.Header = "Month";
            col.Binding = new Binding("Month");
            expenseGrid.Columns.Add(col);
            col = new DataGridTextColumn();
            col.Header = "Total";
            col.Binding = new Binding("Total");
            expenseGrid.Columns.Add(col);


        }

        public void DisplayBudgetCat(List<BudgetItemsByCategory> budgetItemsByCategories)
        {
            DeleteButton.IsEnabled = false;
            ModifyButton.IsEnabled = false;
            expenseGrid.ItemsSource = budgetItemsByCategories;
            expenseGrid.Columns.Clear();
            var col = new DataGridTextColumn();
            col.Header = "Category";
            col.Binding = new Binding("Category");
            expenseGrid.Columns.Add(col);
            col = new DataGridTextColumn();
            col.Header = "Total";
            col.Binding = new Binding("Total");
            expenseGrid.Columns.Add(col);

        }
        private void filterByCategory_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
          

        }
        public void DisplayBudgetItems(List<BudgetItem> budgetItems)
        {
            DeleteButton.IsEnabled = true;
            ModifyButton.IsEnabled = true;
            expenseGrid.Columns.Clear();
            expenseGrid.ItemsSource = budgetItems;
        }

        private void filterBySpecificCategory_Selected(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void filterFlag_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        /// <summary>
        /// DATE CHANGED
        /// SECTION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void filterEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        public void DisplayBudgetCatAndMonth(List<Dictionary<string, object>> budgetItemsByCategoriesAndMonth,List<string> categories)
        {
            DeleteButton.IsEnabled = false;
            ModifyButton.IsEnabled = false;
            expenseGrid.Columns.Clear();
            expenseGrid.ItemsSource = budgetItemsByCategoriesAndMonth;
            expenseGrid.Columns.Clear();
            var col = new DataGridTextColumn();
            col.Header = "Month";
            col.Binding = new Binding("[Month]");
            expenseGrid.Columns.Add(col);
            foreach (var category in categories) 
            {
                col = new DataGridTextColumn();
                col.Header = category;
                col.Binding = new Binding($"[{category}]");
                expenseGrid.Columns.Add(col);
            }

            col = new DataGridTextColumn(); 
            col.Header = "Total";
            col.Binding = new Binding("[Total]");
            expenseGrid.Columns.Add(col);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ExpenseWindow expenseWindow = new ExpenseWindow(presenter);
            presenter.IntializeViewExpenseInterface(expenseWindow);
            expenseWindow.Background = Window.Background;
            expenseWindow.Show();
        }

        public void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ResetFields()
        {
            description.Text = string.Empty;
            amount.Text = string.Empty;
            expenseDate.SelectedDate = DateTime.Now;
            categoryList.SelectedIndex = -1;
        }
    }
}
