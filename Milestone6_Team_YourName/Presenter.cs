﻿using Budget;
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
        private HomeBudget budget;
        public Presenter(ViewInterface v) 
        {
            view = v;
            
        }
        public void Connection(string filename,bool existing)
        {
            budget = new HomeBudget(filename,existing);
            view.DisplayList(budget.categories.List());
            
        }

        public void CreateCat(string description,int index) 
        {
            Category.CategoryType catType = (Category.CategoryType)(index + 1);
            budget.categories.Add(description,catType);
            
            view.DisplayList(budget.categories.List());
        } 
        
        public void DisplayDefCatType()
        {
            //https://www.techiedelight.com/convert-enum-to-list-csharp/ gotten from 
            List<Category.CategoryType> categoryTypes = Enum.GetValues(typeof(Category.CategoryType))
                            .Cast<Category.CategoryType>()
                            .ToList();

            view.DisplayCatTypes(categoryTypes);



        }

    }


}
