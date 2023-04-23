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
        public int count;
        public Presenter(ViewInterface v) 
        {
            view = v;
            
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

        public void DeleteExpense(int id)
        {
            budget.expenses.Delete(id);
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
            List<BudgetItem> budgetItems = budget.GetBudgetItems(null, null,false,0);
            view.DisplayBudgetItems(budgetItems);
        }

        public void DisplayBudgetItemsByMonth(DateTime? start, DateTime? end, bool filterFlage, int catId)
        {
            List<BudgetItemsByMonth> budgetItems = budget.GetBudgetItemsByMonth(null, null, false, 0);
            view.DisplayBudgetItemsByMonth(budgetItems);
        }

        public void DisplayBudgetItemsByCat(DateTime? start, DateTime? end, bool filterFlage, int catId)
        {
            List<BudgetItemsByCategory> budgetItems = budget.GetBudgetItemsByCategory(null, null, false, 0);
            view.DisplayBudgetCat(budgetItems);
        }

        public void DisplayBudgetItemsByCatAndMonth(DateTime? start, DateTime? end, bool filterFlage, int catId)
        {
            List<Dictionary<string,object>> budgetItems = budget.GetBudgetDictionaryByCategoryAndMonth(null, null, false, 0);
            view.DisplayBudgetCatAndMonth(budgetItems);
        }

    }
}
