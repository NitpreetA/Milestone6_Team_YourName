using Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone6_Team_YourName
{
    internal class Presenter
    {
        private ViewInterface view;
        public Presenter(ViewInterface v) 
        {
            view = v;
            
        }
        public void Connection(string filename,bool existing)
        {
            HomeBudget budget = new HomeBudget(filename,existing);
            view.DisplayList(budget.categories.List());
            
        }

    }


}
