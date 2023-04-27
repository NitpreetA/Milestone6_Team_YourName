using Milestone6_Team_YourName;
using Newtonsoft.Json.Bson;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Budget;

namespace TestProject1
{
    public class UnitTest1 : ViewInterface
    {
        private static string budgetFolder = "Budgets";
        private string initialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), budgetFolder);
        private Presenter presenter;

        bool displayCategoryTypes ;
        bool displayList ;
        bool connection;
        bool createCategory;
        bool createExpense;
        bool displayDefCategoryType;

        List<Category> categories;
       

        public void DisplayCatTypes(List<Category.CategoryType> categoryTypes)
        {
            
            displayCategoryTypes = true;
        }

        public void DisplayCategoryList(List<Category> categories)
        {
            this.categories = categories;
            displayList= true;  
        }



        public void Connection()
        {
            connection = true;
        }

        public void CreateCat_AddCategoryToBudget()
        {
            createCategory = true;
        }
        public void CreateExpenses()
        {
            createExpense = true;
        }
        public void DisplayDefCategoryType()
        {
            displayDefCategoryType = true;
        }



        [Fact]
        public void TestConstructor()
        {
            UnitTest1 view = new UnitTest1();
            Presenter presenter = new Presenter(view);

            Assert.IsType<Presenter>(presenter);
        }

        [Fact]
        public void TestDisplayAllCategoryTypes() 
        {
            UnitTest1 view = new UnitTest1();
            Presenter presenter = new Presenter(view);

            // arrange
            var categoryTypes = new List<Category.CategoryType>
            {
                Category.CategoryType.Savings,
                Category.CategoryType.Credit,
                Category.CategoryType.Expense,
                Category.CategoryType.Income,
            };

            // act
            DisplayCatTypes(categoryTypes);
            //view.DisplayCatTypes(categoryTypes); would it be better to call it like this? or it does not matter since its already in here? 
            // makes displayCategorytypes true
            
            // assert
            Assert.True(displayCategoryTypes);
        }
        [Fact]
        public void TestDisplayList()
        {
            // Arrange

            UnitTest1 view = new UnitTest1();
            Presenter presenter = new Presenter(view);
            var categories = new List<Category>
            {
               // keeping it empty
            };

            // act
            DisplayCategoryList(categories);
            // makes displayList true 

            // assert
            Assert.True(displayList);

        }




       
        [Fact]
        public void CreateExpense()
        {
            //arrange
            UnitTest1 view = new UnitTest1();
            presenter = new Presenter(view);
            DateTime dateTime = DateTime.Now;
            string description = "test";
            double amount = 24;
            int categoryId = 1; 
            int addedExpenses =0;

            // act 
            presenter.Connection("test", false);
            presenter.CreateExpenses(dateTime, description, amount, categoryId);
            addedExpenses++;
           // CreateExpenses();

            // assert
            Assert.Equal(addedExpenses, presenter.count);
        }

        [Fact]
        public void CreateCategory()
        {
            //arrange
            UnitTest1 view = new UnitTest1();
            presenter = new Presenter(view);
            presenter.Connection("test", false);

            string description = "test";
            int index = 0;

            //Grab the current list of categories
            int catCount = view.categories.Count;


            // act
            presenter.CreateCat(description, index);
            //  CreateCat_AddCategoryToBudget();

            int newCatCount = view.categories.Count;

            // assert
            Assert.True(newCatCount == catCount + 1);
        }

        [Fact]
        public void DisplayCategoryType()
        {
            // arrange
            UnitTest1 view = new UnitTest1();
            presenter = new Presenter(view);
            displayCategoryTypes = false;


            // act
            presenter.DisplayDefCatType();

            // assert
            Assert.True(view.displayCategoryTypes);

        }

        [Fact]
        public void TestConnection()
        {
            // arrange 

            UnitTest1 view = new UnitTest1();
            presenter = new Presenter(view);
            string filename = ".\\test";
            var existing = false;
            displayList = false;

            //presenter.Connection(initialDirectory + "\\" filename + ".db", existing);

            //act
            presenter.Connection(filename, existing);

            //assert
            Assert.True(view.displayList);

        }

        public void DisplayBudgetItems(List<BudgetItem> budgetItems)
        {
            throw new NotImplementedException();
        }

        public void Filter()
        {
            throw new NotImplementedException();
        }

        public void DisplayBudgetItemsByMonth(List<BudgetItemsByMonth> budgetByMonth)
        {
            throw new NotImplementedException();
        }

        public void DisplayBudgetCat(List<BudgetItemsByCategory> budgetItemsByCategories)
        {
            throw new NotImplementedException();
        }

        public void DisplayBudgetCatAndMonth(List<Dictionary<string, object>> budgetItemsByCategoriesAndMonth)
        {
            throw new NotImplementedException();
        }

        public void DisplayBudgetCatAndMonth(List<Dictionary<string, object>> budgetItemsByCategoriesAndMonth, List<string> categories)
        {
            throw new NotImplementedException();
        }
    }
}