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


        public void GetCategories()
        {

            expenseView.DisplayCatInPopUp(budget.categories.List()); 
        }

        public void Connection(string filename,bool existing)
        {
            budget = new HomeBudget(filename,existing);
            view.DisplayList(budget.categories.List());
            view.Filter();
            
        }

        public void CreateCat(string description,int index) 
        {
            Category.CategoryType catType = (Category.CategoryType)(index + 1);

            List<Category> currentCategories = budget.categories.List();

            foreach (Category c in currentCategories)
            {
                if (c.Description == description)
                {
                    MessageBox.Show("Category already exists");
                    return;
                }
            }

            budget.categories.Add(description,catType);
            view.DisplayList(budget.categories.List());
        }

        public void CreateExpenses(DateTime date, string description, double amount, int catId)
        {
            count++;
            budget.expenses.Add(date, catId + 1, amount, description);
            view.Filter();
        }

        public void DisplayDefCatType()
        {
            //https://www.techiedelight.com/convert-enum-to-list-csharp/ gotten from 
            List<Category.CategoryType> categoryTypes = Enum.GetValues(typeof(Category.CategoryType))
                            .Cast<Category.CategoryType>()
                            .ToList();

            view.DisplayCatTypes(categoryTypes);
        }

       public void DisplayBudgetItems(DateTime ? start, DateTime ? end, bool filterFlage,int catId)
        {
            List<BudgetItem> budgetItems = budget.GetBudgetItems(start, end,filterFlage,catId);
            view.DisplayBudgetItems(budgetItems);
        }

        public void DisplayBudgetItemsByMonth(DateTime? start, DateTime? end, bool filterFlage, int catId)
        {
            List<BudgetItemsByMonth> budgetItems = budget.GetBudgetItemsByMonth(start, end, filterFlage, catId);
            view.DisplayBudgetItemsByMonth(budgetItems);
        }

        public void DisplayBudgetItemsByCat(DateTime? start, DateTime? end, bool filterFlage, int catId)
        {
            List<BudgetItemsByCategory> budgetItems = budget.GetBudgetItemsByCategory(start, end, filterFlage, catId);
            view.DisplayBudgetCat(budgetItems);
        }

        public void DisplayBudgetItemsByCatAndMonth(DateTime? start, DateTime? end, bool filterFlage, int catId)
        {
            List<Dictionary<string,object>> budgetItems = budget.GetBudgetDictionaryByCategoryAndMonth(start, end, filterFlage,catId);
            List<string> categories = new List<string>();
            foreach(Category item in budget.categories.List()) 
            {
            
            categories.Add(item.Description);
            }
            view.DisplayBudgetCatAndMonth(budgetItems,categories);
        }

        public void DisplayBudgetItemsFilter(bool budgetByMonth,bool budgetByCat,DateTime? startDate,DateTime? endDate,bool categoryChecked,int catId) 
        {
            catId++;
            if (budgetByMonth && budgetByCat)
            {
                
                DisplayBudgetItemsByCatAndMonth(startDate, endDate, categoryChecked, catId);

            }
            else if (budgetByCat)
            {

                DisplayBudgetItemsByCat(startDate, endDate, categoryChecked, catId);

            }
            else if (budgetByMonth)
            {
             
                DisplayBudgetItemsByMonth(startDate, endDate, categoryChecked, catId);

            }
            else
            {

                DisplayBudgetItems(startDate, endDate, categoryChecked, catId);

            }

        }
        public void DeleteExpense(int id)
        {
            budget.expenses.Delete(id);
        }

        public void ModifyExpense(int id, DateTime date, int categoryId, double amount, string description)
        {
            budget.expenses.UpdateProperties(id, date, categoryId+1, amount, description);
            view.Filter();

        }

        public void AddExpense(string description,string expenseAmount, string date,int catId)
        {
            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(expenseAmount) || catId == -1)
            {
                view.DisplayMessage("Please fill out all of the input fields");
            }
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



    }
}
