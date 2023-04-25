﻿using ModernWpf;
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
                presenter.Connection(currentBudgetFile.Text,existing);
                filterStartDate.SelectedDate = DateTime.Now;
                filterEndDate.SelectedDate = DateTime.Now;
                ExpenseFieldState(true);
            }
            
        }

        private void btn_AddExpense_Clck(object sender, RoutedEventArgs e)
        {
            bool errorWhileAddingAnExpense = false;

            if (description.Text == lastDescription && amount.Text == lastAmount)
            {
                var response = MessageBox.Show("Current expense is identical to previous expense. Add anyways?", 
                    "Identical Expense", MessageBoxButton.YesNo);
                if (response == MessageBoxResult.No)
                    return;
            }


            if(string.IsNullOrEmpty(description.Text) || string.IsNullOrEmpty(amount.Text) || categoryList.SelectedItem == null)
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
                lastDescription = description.Text;
                lastAmount = amount.Text;
                double expenseAmount = Double.Parse(lastAmount);
                string date = expenseDate.ToString();
                DateTime dateTime = DateTime.Parse(date);
                int catId = categoryList.SelectedIndex;
                presenter.CreateExpenses(dateTime, lastDescription, expenseAmount, catId);

                description.Text = string.Empty;
                amount.Text = string.Empty;


            }
        }

        private void btn_ClearExpense_Clck(object sender, RoutedEventArgs e)
        {
            description.Text = string.Empty;
            amount.Text = string.Empty;
            categoryList.SelectedIndex = -1;
        }

        

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

            presenter.Farfalou((bool)filterByMonth.IsChecked, (bool)filterByCategory.IsChecked, startDate, endDate, (bool)filterFlag.IsChecked, filterBySpecificCategory.SelectedIndex);
        }
      

        private void MenuItem_ModifyClick(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_DeleteClick(object sender, RoutedEventArgs e)
        {

            
        }





        private void filterByMonth_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }



        public void DisplayBudgetItemsByMonth(List<BudgetItemsByMonth> budgetByMonth)
        {
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



        private void filterByCategory_Checked(object sender, RoutedEventArgs e)

        {
            
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
            //expenseGrid.Columns De
        }
        public void DisplayBudgetItems(List<BudgetItem> budgetItems)
        {
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
            expenseWindow.Background = Window.Background;
            expenseWindow.Show();
        }
    }
}
