using Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone6_Team_YourName
{
    public interface ViewExpenseInterface 
    {
        public void DisplayCatInPopUp(List<Category> categories);

        public void PopulateFields(string oldDescription, string oldAmount, DateTime oldDate, int oldCategoryID);
    }
}
