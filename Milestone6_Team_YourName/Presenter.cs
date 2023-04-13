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
        public Presenter() 
        {
            
        }
        public void Connection(string filename)
        {
            HomeBudget budget = new HomeBudget(filename);
            
        }

    }


}
