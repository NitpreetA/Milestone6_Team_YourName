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
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace Milestone6_Team_YourName
{
   
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
        private int counter = 0;
        public int expenseId;

        int currentRowCount = 0;


        private bool createdNewCategory = false;

        #region Constructor
        public MainWindow()
        {
            //Setup Window and Presenter
            InitializeComponent();
            presenter = new Presenter(this);
            presenter.DisplayDefCatType();

            //Set default values and disable the current window
            expenseDate.SelectedDate = DateTime.Now;
            ExpenseFieldState(false);
            
            //Run through persistent settings
            PropertiesSet();
            PropertiesToTheme();
            LastOpenFile();

            presenter.DisplayDefCatType();

            if (!Directory.Exists(initialDirectory))
                Directory.CreateDirectory(initialDirectory);
        }
        #endregion

        #region File Code

        #region LastOpenFile
        /// <summary>
        /// Asks the user if they wish to open their last opened database, should that database still exist
        /// </summary>
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
        #endregion

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
                presenter.Connection(currentBudgetFile.Text, existing);
                ExpenseFieldState(true);
            }
        }
        /// <summary>
        /// enables the options to create a new file once the radio button has been selected.
        /// </summary>
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            SubmitFile.IsEnabled = true;
            budgetFileName.IsEnabled = true;

        }

        /// <summary>
        /// Creates the file once filed inputed and button was pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitFile_Click_1(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(initialDirectory))
            {
                existing = true;
                presenter.Connection(initialDirectory + "\\" + budgetFileName.Text + ".db", existing);
                ExpenseFieldState(true);
                SubmitFile.IsEnabled = false;
            }
        }

        #endregion



        #region ExpenseFieldState
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
            searchButton.IsEnabled = state;
            searchBarText.IsEnabled = state;
        }
        #endregion

        /// <summary>
        /// Closes the page once button is clicked
        /// </summary>
        private void btn_closePage(object sender, RoutedEventArgs e) => Close();

        #region Expenses

        #region Add Expense
        /// <summary>
        /// Adds Expense once all the proper fields are inputted correctly 
        /// </summary>
        private void btn_AddExpense_Clck(object sender, RoutedEventArgs e)
        => presenter.AddExpense(description.Text, amount.Text, expenseDate.ToString(), categoryList.SelectedIndex);
        #endregion

        #region ClearExpense_Click
        /// <summary>
        /// Clears all inputted fields and resets them to default/starting position
        /// </summary>
        private void btn_ClearExpense_Clck(object sender, RoutedEventArgs e)
        {
            //description.Text = string.Empty;
            //amount.Text = string.Empty;
            categoryList.SelectedIndex = -1;
            expenseDate.SelectedDate = DateTime.Now;
        }
        #endregion

        #endregion

        #region ComboBoxFill

        #region DisplayCategoryList
        /// <summary>
        /// Fills the comboboxes for Categories, one for the Add Expense menu, and one for the filter menu.
        /// </summary>
        /// <param name="categories">List of categories to fill the combo box with.</param>
        public void DisplayCategoryList(List<Category> categories)
        {

            categoryList.ItemsSource = categories;
            filterBySpecificCategory.ItemsSource = categories;

            createdNewCategory = false;
        }
        #endregion

        #region DisplayCatTypes
        /// <summary>
        /// Fills the combobox with the default Cat Types
        /// </summary>
        /// <param name="categoryTypes">A list of the default cat types.</param>
        public void DisplayCatTypes(List<CategoryType> categoryTypes)
        {
            CategoryType.ItemsSource = categoryTypes;
            CategoryType.SelectedIndex = 1;
        }
        #endregion

        #endregion

        #region Create New Category Click
        /// <summary>
        /// Creates a category if all fields are properly inputted 
        /// </summary>
        private void btn_CreateNewCategory_Click(object sender, RoutedEventArgs e)
        {
            if (createCategory.Text == "")
                MessageBox.Show("Must Input a name for the category you want to add.");
            else
            {
                createdNewCategory = true;
                presenter.CreateCat(createCategory.Text, CategoryType.SelectedIndex);
                createCategory.Text = "";
            }
        }
        #endregion

        #region Persistent Settings

        #region PropertiesSet
        /// <summary>
        /// Gets all the persistent settings saved on application closing and sets them to the current session.
        /// </summary>
        private void PropertiesSet()
        {
            if (!App.Current.Properties.Contains("BackgroundColor"))
                App.Current.Properties.Add("BackgroundColor", Window.Background);

            if (!App.Current.Properties.Contains("AccentColor"))
                App.Current.Properties.Add("AccentColor", _accent);

            if (!App.Current.Properties.Contains("LastOpenDB"))
                App.Current.Properties.Add("LastOpenDB", "");
        }
        #endregion

        #region PropertiesToTheme
        /// <summary>
        /// Translates the saved accent and background settings to the current session's accent and background style.
        /// </summary>
        private void PropertiesToTheme()
        {
            string accent = App.Current.Properties["AccentColor"].ToString();
            string background = App.Current.Properties["BackgroundColor"].ToString();
            SetAccent(accent);
            SetBackground(background);
        }
        #endregion

        #region Window Closing
        /// <summary>
        /// Saves the current properties (colors and open DB) to persistent storage on window close.
        /// If there are unsaved changes in the add expense area, a warning box will be shown before-hand.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Unnsaved Changes warning box check
            if(! string.IsNullOrEmpty(description.Text) || ! string.IsNullOrEmpty(amount.Text))
            {
                var response = MessageBox.Show("You have unsaved changes. Are you sure you'd " +
                    "like to close the application?", "Closed Changes", MessageBoxButton.YesNo);
                if (response == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            App.Current.Properties["BackgroundColor"] = Window.Background;
            App.Current.Properties["AccentColor"] = _accent;
            App.Current.Properties["LastOpenDB"] = openBudget;
        }
        #endregion

        #endregion

        #region Filter

        public void Filter()
        {
            string start = filterStartDate.ToString();
            string end = filterEndDate.ToString();
            DateTime? startDate;
            DateTime? endDate;

            // Determine if the expenses are filtered between certain days
            if (start != string.Empty)
                startDate = DateTime.Parse(start);
            else
                startDate = null;

            if (end != string.Empty)
                endDate = DateTime.Parse(end);
            else
                endDate = null;

            presenter.DisplayBudgetItemsFilter((bool)filterByMonth.IsChecked, (bool)filterByCategory.IsChecked, startDate, endDate, (bool)filterFlag.IsChecked, filterBySpecificCategory.SelectedIndex);
        }

        private void filterByCategory_Click(object sender, RoutedEventArgs e) => Filter();

        private void filterByMonth_Click(object sender, RoutedEventArgs e) => Filter();

        private void filterBySpecificCategory_Selected(object sender, RoutedEventArgs e) => Filter();

        private void filterFlag_Click(object sender, RoutedEventArgs e) => Filter();

        private void filterStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e) => Filter();

        private void filterEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e) => Filter();

        #endregion

        #region Modify Menu

        #region Modify Click
        /// <summary>
        /// Opens an Expense Window to edit the currently selected expense
        /// </summary>
        private void MenuItem_ModifyClick(object sender, RoutedEventArgs e)
        {
            int index = 0;

            if (expenseGrid.SelectedItem != null)
            {
                //Grab the Expense
                Budget.BudgetItem budgetItemToModify = (Budget.BudgetItem)(expenseGrid.SelectedItem);
                index = expenseGrid.SelectedIndex;

                //Setup the Expense Window
                ExpenseWindow expenseWindow = new ExpenseWindow(presenter, budgetItemToModify);
                expenseWindow.Background = Window.Background;

                //Open Window
                expenseWindow.ShowDialog();
            }
            Filter();
            if (expenseGrid.Items.Count == 0)
                expenseGrid.SelectedIndex = -1;
            else if (index == expenseGrid.Items.Count)
                expenseGrid.SelectedIndex = index - 1;
            else
                expenseGrid.SelectedIndex = index;

        }
        #endregion

        #region Delete Click
        /// <summary>
        /// Asks for confirmation before deleting a selected expense
        /// </summary>
        private void MenuItem_DeleteClick(object sender, RoutedEventArgs e)
        {
            int index = 0;
            if (expenseGrid.SelectedItem != null)
            {

                //Grab the Expense
                Budget.BudgetItem budgetItemToDelete = (Budget.BudgetItem)(expenseGrid.SelectedItem);
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete?", "Delete Confirmation", MessageBoxButton.YesNo);
                
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    index = expenseGrid.SelectedIndex;
                    presenter.DeleteExpense(budgetItemToDelete.ExpenseID);
                }
            }
            Filter();

            if (expenseGrid.Items.Count == 0)
                expenseGrid.SelectedIndex = -1;
            else if (index == expenseGrid.Items.Count)
                expenseGrid.SelectedIndex = index - 1;
            else
                expenseGrid.SelectedIndex = index;
        }
        #endregion

        #endregion

        #region Displays
        public void DisplayBudgetItemsByMonth(List<BudgetItemsByMonth> budgetByMonth)
        {
            DeleteButton.IsEnabled = false;
            ModifyButton.IsEnabled = false;
            searchBarText.IsEnabled = false;
            searchButton.IsEnabled = false;
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
            searchBarText.IsEnabled = false;
            searchButton.IsEnabled = false;
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

        public void DisplayBudgetItems(List<BudgetItem> budgetItems)
        {
            searchBarText.IsEnabled = true;
            searchButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
            ModifyButton.IsEnabled = true;
            expenseGrid.Columns.Clear();
            expenseGrid.ItemsSource = budgetItems;
        }


        public void DisplayBudgetCatAndMonth(List<Dictionary<string, object>> budgetItemsByCategoriesAndMonth, List<string> categories)
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

        public void DisplayMessage(string message) => MessageBox.Show(message);

        #endregion

        public void ResetFields()
        {
            description.Text = string.Empty;
            amount.Text = string.Empty;
        }

        private void btn_SearchBarClick(object sender, RoutedEventArgs e)
        {
            var searchedExpense = searchBarText.Text.ToLower();
            var budgetItemsInGrid = expenseGrid.ItemsSource as List<BudgetItem>;

            var foundBudgetItemByShortDescription = budgetItemsInGrid.FindAll(budgetItems => budgetItems.ShortDescription.ToLower().Contains(searchedExpense));
            var foundBudgetItemByAmount = budgetItemsInGrid.FindAll(budgetItems => budgetItems.Amount.ToString().Contains(searchedExpense));

            if (searchedExpense == string.Empty)
            {
                MessageBox.Show("Search bar is empty");
                resetAllBackgrounds(null);

            }
            else if (foundBudgetItemByShortDescription.Count == 0 && foundBudgetItemByAmount.Count ==0)
            {
                MessageBox.Show("Expense not found");
                resetAllBackgrounds(null);
            }
            else
            {
                if(foundBudgetItemByAmount.Count != 0)
                {
                    int foundItemsNumber = foundBudgetItemByAmount.Count();
                    var foundItemIndex = budgetItemsInGrid.IndexOf(foundBudgetItemByAmount[counter % foundItemsNumber]);
                    var item = expenseGrid.Items.GetItemAt(foundItemIndex);
                    expenseGrid.ScrollIntoView(item);

                    // highlight 
                    var rowContainer = expenseGrid.ItemContainerGenerator.ContainerFromIndex(foundItemIndex) as DataGridRow;
                    resetAllBackgrounds(null);
                    rowContainer.Background = Brushes.LightGray; // basically we want the highlight the selected index instead of everything that matches
                    counter++;
                }
                else
                {
                    int foundItemsNumber = foundBudgetItemByShortDescription.Count();
                    var foundItemIndex = budgetItemsInGrid.IndexOf(foundBudgetItemByShortDescription[counter % foundItemsNumber]);
                    var item = expenseGrid.Items.GetItemAt(foundItemIndex);
                    expenseGrid.ScrollIntoView(item);

                    // highlight 
                    var rowContainer = expenseGrid.ItemContainerGenerator.ContainerFromIndex(foundItemIndex) as DataGridRow;
                    resetAllBackgrounds(null);
                    rowContainer.Background = Brushes.LightGray; // basically we want the highlight the selected index instead of everything that matches
                    counter++;

                }

            }
        }

        private void resetAllBackgrounds(Brush background)
        {
            for (int i = 0; i < expenseGrid.Items.Count; i++)
            {
                var rowContainer = expenseGrid.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                rowContainer.Background = background;
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            if (expenseGrid.SelectedItem !=null )
            {
                MenuItem_ModifyClick(sender, e);
            }
        }

        
    }
}
