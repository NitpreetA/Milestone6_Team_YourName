using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget;

namespace Milestone6_Team_YourName
{
    public interface ViewInterface
    {
        void DisplayList(List<Category> categories);

        void DisplayCatTypes(List<Category.CategoryType> categoryTypes);

        public void DisplayBudgetItems(List<BudgetItem> budgetItems);

        public void Filter();

        public void DisplayBudgetItemsByMonth(List<BudgetItemsByMonth> budgetByMonth);

        public void DisplayBudgetCat(List<BudgetItemsByCategory> budgetItemsByCategories);

        public void DisplayBudgetCatAndMonth(List<Dictionary<string,object>> budgetItemsByCategoriesAndMonth);
    }
}
