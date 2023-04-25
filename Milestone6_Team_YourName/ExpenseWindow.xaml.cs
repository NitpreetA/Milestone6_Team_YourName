using Budget;
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
using static Budget.Category;

namespace Milestone6_Team_YourName
{
    /// <summary>
    /// Interaction logic for ExpenseWindow.xaml
    /// </summary>
    public partial class ExpenseWindow : ViewInterface
    {
        Presenter currentPresenter;

        private string lastExpense;
        private string lastDescription;
        private string lastAmount;

        private bool createdNewCategory = false;

        public ExpenseWindow(Presenter presenter)
        {
            currentPresenter = presenter;
            InitializeComponent();

            expenseDate.SelectedDate = DateTime.Now;
        }

        private void btn_ClearExpense_Click(object sender, RoutedEventArgs e)
        {
            description.Text = string.Empty;
            amount.Text = string.Empty;
            categoryList.SelectedIndex = -1;
        }

        private void btn_AddExpense_Click(object sender, RoutedEventArgs e)
        {
            bool errorWhileAddingAnExpense = false;

            if (description.Text == lastDescription && amount.Text == lastAmount)
            {
                var response = MessageBox.Show("Current expense is identical to previous expense. Add anyways?",
                    "Identical Expense", MessageBoxButton.YesNo);
                if (response == MessageBoxResult.No)
                    return;
            }

            if (string.IsNullOrEmpty(description.Text) || string.IsNullOrEmpty(amount.Text) || categoryList.SelectedItem == null)
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
                currentPresenter.CreateExpenses(dateTime, lastDescription, expenseAmount, catId);

                description.Text = string.Empty;
                amount.Text = string.Empty;

                currentPresenter.DisplayBudgetItems();
            }
        }

        public void DisplayList(List<Category> categories)
        {

            categoryList.ItemsSource = categories;

            if (createdNewCategory)
            {
                //categories.Add();
            }
            createdNewCategory = false;
        }

        public void DisplayBudgetItems(List<BudgetItem> budgetItems)
        {

            //expenseGrid.ItemsSource = budgetItems;
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
                currentPresenter.CreateCat(createCategory.Text, CategoryType.SelectedIndex);
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
