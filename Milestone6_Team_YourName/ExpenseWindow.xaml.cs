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
        private int expenseId;


        public ExpenseWindow(Presenter presenter, Budget.BudgetItem expense)
        {
            currentPresenter = presenter;
            InitializeComponent();
            currentPresenter.IntializeViewExpenseInterface(this);
            currentPresenter.SetUpExpenseWindow(expense);
            expenseId = expense.ExpenseID;
        }

        #region PopulateFields
        /// <summary>
        /// Sets all fields within the Edit Expense window to the values of the expense that is being edited.
        /// </summary>
        /// <param name="oldDescription">Passed description of the expense prior to edits.</param>
        /// <param name="oldAmount">Passed amount of the expense prior to edits.</param>
        /// <param name="oldDate">Passed date of the expense prior to edits.</param>
        /// <param name="oldCategoryID">Passed category ID of the expense prior to edits.</param>
        public void PopulateFields(string oldDescription, string oldAmount, DateTime oldDate, int oldCategoryID)
        {
            description.Text = oldDescription;
            amount.Text = oldAmount;
            expenseDate.SelectedDate = oldDate;
            expenseWindowCatList.SelectedIndex = oldCategoryID;
            
        }
        #endregion

        #region DisplayCatInPopUp
        /// <summary>
        /// Populates the Category List combo-box with information passed in from the Presenter
        /// </summary>
        /// <param name="categories"></param>
        public void DisplayCatInPopUp(List<Category> categories)
        {
            expenseWindowCatList.ItemsSource= categories;
        }
        #endregion

        #region Edit Expense Click
        private void btn_EditExpense_Click(object sender, RoutedEventArgs e)
        {
            lastDescription = description.Text;
            lastAmount = amount.Text;
            string date = expenseDate.ToString();
            DateTime dateTime = DateTime.Parse(date);
            int catId = expenseWindowCatList.SelectedIndex;

            bool success = currentPresenter.ModifyExpense(expenseId, dateTime, catId, lastAmount, lastDescription);

            if (success)
            {
                MessageBox.Show("Expense was successfully added");
                Close();
            }
        }
        #endregion

        #region Clear Expense Click
        /// <summary>
        /// Resets all the input fields of the opened Expense Window when the Clear button is clicked.
        /// </summary>
        private void btn_ClearExpense_Click(object sender, RoutedEventArgs e)
        {
            description.Text = string.Empty;
            amount.Text = string.Empty;
            expenseWindowCatList.SelectedIndex = -1;
            expenseDate.SelectedDate = DateTime.Now;
        }
        #endregion
    }
}
