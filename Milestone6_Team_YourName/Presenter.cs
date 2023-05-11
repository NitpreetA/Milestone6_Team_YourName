using Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Milestone6_Team_YourName
{
    public class Presenter
    {
        private ViewInterface view;
        private HomeBudget budget;
        private ViewExpenseInterface expenseView;
        public int count;

        public Presenter(ViewInterface v) 
        {
            view = v; 
        }

        public void IntializeViewExpenseInterface(ViewExpenseInterface expenseView)
        {
            this.expenseView = expenseView;
            GetCategories();
        }


        public void GetCategories() => expenseView.DisplayCatInPopUp(budget.categories.List()); 
        

        #region Connection
        /// <summary>
        /// Opens or creates a new budget file and begins the connection process
        /// </summary>
        /// <param name="filename">Filename of the budget database file</param>
        /// <param name="existing">Boolean denoting the existence of the file</param>
        public void Connection(string filename,bool existing)
        {
            budget = new HomeBudget(filename,existing);
            view.DisplayCategoryList(budget.categories.List());
            view.Filter();
        }
        #endregion

        #region Create Cat
        /// <summary>
        /// Creates a new Category from the description and index passed in from the View
        /// </summary>
        /// <param name="description">Description of the new category</param>
        /// <param name="index">Index of the category type of the new category</param>
        public void CreateCat(string description,int index) 
        {
            Category.CategoryType catType = (Category.CategoryType)(index + 1);

            List<Category> currentCategories = budget.categories.List();
            List<string> categoryDescriptions = new List<string>();
            
            foreach (Category category in currentCategories)
                categoryDescriptions.Add(category.Description);

            if(categoryDescriptions.Contains(description))
            {
                MessageBox.Show("Category already exists");
                return;
            }

            budget.categories.Add(description,catType);
            view.DisplayCategoryList(budget.categories.List());
        }
        #endregion

        #region Desplay Def Cat Type
        /// <summary>
        /// Displays a list of Category Types in the View for the user to select from when creating a new Category
        /// </summary>
        public void DisplayDefCatType()
        {
            //https://www.techiedelight.com/convert-enum-to-list-csharp/ gotten from 
            List<Category.CategoryType> categoryTypes = Enum.GetValues(typeof(Category.CategoryType))
                            .Cast<Category.CategoryType>().ToList();

            view.DisplayCatTypes(categoryTypes);
        }
        #endregion

        //TODO: Re-do tests and make functions in this region private
        #region Display Budget Items Filter
        public void DisplayBudgetItemsFilter(bool budgetByMonth, bool budgetByCat, DateTime? startDate, DateTime? endDate, bool categoryChecked, int catId)
        {
            catId++;
            if (budgetByMonth && budgetByCat)
                DisplayBudgetItemsByCatAndMonth(startDate, endDate, categoryChecked, catId);
            else if (budgetByCat)
                DisplayBudgetItemsByCat(startDate, endDate, categoryChecked, catId);
            else if (budgetByMonth)
                DisplayBudgetItemsByMonth(startDate, endDate, categoryChecked, catId);
            else
                DisplayBudgetItems(startDate, endDate, categoryChecked, catId);
        }


        #region Display Budget Items Helper Methods

        #region Display Budget Items
        public void DisplayBudgetItems(DateTime ? start, DateTime ? end, bool filterFlage,int catId)
        {
            List<BudgetItem> budgetItems = budget.GetBudgetItems(start, end,filterFlage,catId);
            view.DisplayBudgetItems(budgetItems);
        }
        #endregion

        #region Display Budget Items By Month
        public void DisplayBudgetItemsByMonth(DateTime? start, DateTime? end, bool filterFlage, int catId)
        {
            List<BudgetItemsByMonth> budgetItems = budget.GetBudgetItemsByMonth(start, end, filterFlage, catId);
            view.DisplayBudgetItemsByMonth(budgetItems);
        }
        #endregion

        #region Display Budget Items By Category
        public void DisplayBudgetItemsByCat(DateTime? start, DateTime? end, bool filterFlage, int catId)
        {
            List<BudgetItemsByCategory> budgetItems = budget.GetBudgetItemsByCategory(start, end, filterFlage, catId);
            view.DisplayBudgetCat(budgetItems);
        }
        #endregion

        #region Display Budget Items By Category And Month
        public void DisplayBudgetItemsByCatAndMonth(DateTime? start, DateTime? end, bool filterFlage, int catId)
        {
            List<Dictionary<string,object>> budgetItems = budget.GetBudgetDictionaryByCategoryAndMonth(start, end, filterFlage,catId);
            List<string> categories = new List<string>();
            foreach(Category item in budget.categories.List()) 
                categories.Add(item.Description);
            view.DisplayBudgetCatAndMonth(budgetItems,categories);
        }
        #endregion

        #endregion

        #endregion

        #region Delete Expense
        /// <summary>
        /// Deletes an expense by ID.
        /// </summary>
        /// <param name="id">The ID to delete.</param>
        public void DeleteExpense(int id)
        {
            budget.expenses.Delete(id);
        }
        #endregion

        #region Modify Expense
        /// <summary>
        /// Modifies an existing expense by ID, provided that all of the input fields are filled out.
        /// </summary>
        /// <param name="id">The ID of the expense being edited.</param>
        /// <param name="date">The date of the ID to modify.</param>
        /// <param name="categoryId">The id of the category of the modified expense.</param>
        /// <param name="amount">The updated amount of the expense in string form</param>
        /// <param name="description">The updated description of the expense.</param>
        /// <returns></returns>
        public bool ModifyExpense(int id, DateTime date, int categoryId, string amount, string description)
        {
            if (string.IsNullOrEmpty(description) || categoryId == -1 || string.IsNullOrEmpty(amount)) {
                MessageBox.Show("Please fill out all of the input fields");
                return false;
            }

            double currentAmount = Double.Parse(amount);
            budget.expenses.UpdateProperties(id, date, categoryId + 1, currentAmount, description);
            view.Filter();
            return true;
        }
        #endregion

        #region Add Expense
        /// <summary>
        /// Adds an expense to the database and updates the view, provided that all of the input fields are filled out.
        /// </summary>
        /// <param name="description">The description of the added expense.</param>
        /// <param name="expenseAmount">The cost of the added expense in string form.</param>
        /// <param name="date">The date of the added expense in string form.</param>
        /// <param name="catId">The selected category id of the new expense.</param>
        public void AddExpense(string description,string expenseAmount, string date,int catId)
        {
            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(expenseAmount) || catId == -1)
                view.DisplayMessage("Please fill out all of the input fields");
            else
            {
                double amount = Double.Parse(expenseAmount);
                DateTime dateTime = DateTime.Parse(date);

                CreateExpenses(dateTime, description, amount, catId);
                view.DisplayMessage("Succesfuly added expense");
                view.Filter();
                view.ResetFields();
            }
        }

        //Helper method for AddExpense
        private void CreateExpenses(DateTime date, string description, double amount, int catId)
        {
            count++;
            budget.expenses.Add(date, catId + 1, amount, description);
            view.Filter();
        }
        #endregion

        #region Set Up Expense Window
        /// <summary>
        /// Populates a newly opened Expense Window with the details of the unedited expense.
        /// </summary>
        /// <param name="expense">The expense a user is editing</param>
        public void SetUpExpenseWindow(Budget.BudgetItem expense)
        {
            string oldDescription = expense.ShortDescription;
            string oldAmount = expense.Amount.ToString();
            DateTime oldDate = expense.Date;
            //The -1 is because the ComboBox is 0-indexed but the database is 1-indexed.
            int oldCategory = expense.CategoryID - 1;
            expenseView.PopulateFields(oldDescription, oldAmount, oldDate, oldCategory);
        }
        #endregion
    }

}
