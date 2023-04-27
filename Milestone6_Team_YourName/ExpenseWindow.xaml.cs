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
    public partial class ExpenseWindow : ViewExpenseInterface
    {
        Presenter currentPresenter;

        private string lastDescription;
        private string lastAmount;
        public int expenseId;

        public ExpenseWindow(Presenter presenter)
        {
            currentPresenter = presenter;
            InitializeComponent();

            expenseDate.SelectedDate = DateTime.Now;
        }

        public void DisplayCatInPopUp(List<Category> categories)
        {
            expenseWindowCatList.ItemsSource= categories;
        }

        private void btn_EditExpense_Click(object sender, RoutedEventArgs e)
        {
            bool errorWhileAddingAnExpense = false;

            if (description.Text == lastDescription && amount.Text == lastAmount)
            {
                var response = MessageBox.Show("Current expense is identical to previous expense. Add anyways?",
                    "Identical Expense", MessageBoxButton.YesNo);
                if (response == MessageBoxResult.No)
                    return;
            }

            if (string.IsNullOrEmpty(description.Text) || string.IsNullOrEmpty(amount.Text) || expenseWindowCatList.SelectedItem == null)
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
                int catId = expenseWindowCatList.SelectedIndex;


                currentPresenter.ModifyExpense(expenseId, dateTime, catId,expenseAmount, lastDescription);

                description.Text = string.Empty;
                amount.Text = string.Empty;
                //currentPresenter.DisplayBudgetItems();
            }
        }

        private void btn_CreateNewCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_ClearExpense_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
