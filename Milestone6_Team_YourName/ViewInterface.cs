using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget;

namespace Milestone6_Team_YourName
{
    interface ViewInterface
    {
        void DisplayList(List<Category> categories);

        void DisplayCatTypes(List<Category.CategoryType> categoryTypes);
    }
}
