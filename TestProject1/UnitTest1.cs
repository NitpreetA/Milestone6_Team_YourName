using Milestone6_Team_YourName;
using Newtonsoft.Json.Bson;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Budget;

namespace TestProject1
{
    [Collection("Sequential")]
    public class UnitTest1 : ViewInterface
    {
        

        private static string budgetFolder = "Budgets";
        private string initialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), budgetFolder);
        private Presenter presenter;

        bool displayCategoryTypes;
        bool displayCatList;
        bool connection;
        bool createCategory;
        bool createExpense;
        bool displayDefCategoryType;

        bool displayBudgetItems;
        bool filter;
        bool displayBudgetItemsByMonth;
        bool displayBudgetCat;
        bool displayBudgetCatAndMonth;
        bool displayBudgetCatAndMonthSpecificCat;

        bool displayMessage;
        bool resetFields;


        List<Category> categories;

        public void DisplayCategoryList(List<Category> categories)
        {
            this.categories = categories;
            displayCatList = true;
        }

        public void DisplayMessage(string message)
        {
            displayMessage = true;
        }

        public void ResetFields()
        {
            resetFields = true;
        }


        public void DisplayCatTypes(List<Category.CategoryType> categoryTypes)
        {

            displayCategoryTypes = true;
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
        public void DisplayBudgetItems(List<BudgetItem> budgetItems)
        {
            displayBudgetItems = true;
        }

        public void Filter()
        {
            filter = true;
        }

        public void DisplayBudgetItemsByMonth(List<BudgetItemsByMonth> budgetByMonth)
        {
            displayBudgetItemsByMonth = true;
        }

        public void DisplayBudgetCat(List<BudgetItemsByCategory> budgetItemsByCategories)
        {
            displayBudgetCat = true;
        }

        public void DisplayBudgetCatAndMonth(List<Dictionary<string, object>> budgetItemsByCategoriesAndMonth, List<string> categories)
        {
            displayBudgetCatAndMonth = true;
        }


        //good
        [Fact]

        public void TestConstructor()
        {
            UnitTest1 view = new UnitTest1();
            Presenter presenter = new Presenter(view);

            Assert.IsType<Presenter>(presenter);
        }

        //good
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
            presenter.DisplayDefCatType();


            // assert
            Assert.True(view.displayCategoryTypes);
        }



        // good
        [Fact]
        public void DisplayBudgetItems_Testing()
        {
            // Arrange
            var view = new UnitTest1();
            var presenter = new Presenter(view);
            var budgetItems = new List<BudgetItem>();
            presenter.Connection("test", false);

            // Act
            presenter.DisplayBudgetItems(null,null,false,1);


            // Assert
            Assert.True(view.displayBudgetItems);
        }



        [Fact]
        public void TestDisplayBudgetItemsByMonth()
        {
            // Arrange
            var view = new UnitTest1();
            var presenter = new Presenter(view);
            var budgetItemsByMonth = new List<BudgetItemsByMonth>();
            presenter.Connection("test", false);
            // Act
            presenter.DisplayBudgetItemsByMonth(DateTime.Now, DateTime.Now, false, 0);
            // Assert
            Assert.True(view.displayBudgetItemsByMonth);
            presenter.CloseDB();
        }

        [Fact]
        public void TestDisplayBudgetCat()
        {
            var view = new UnitTest1();
            Presenter presenter = new Presenter(view);
            var budgetItemsByMonth = new List<BudgetItemsByMonth>();
            presenter.Connection("test", false);

            //arrange 
            presenter.DisplayBudgetItemsByCat(DateTime.Now, DateTime.Now, false, 0);

            Assert.True(view.displayBudgetCat);
        }

        [Fact]
        public void TestDisplayBudgetCatAndMonth()
        {
            
            // Arrange
            var view = new UnitTest1();
            var presenter = new Presenter(view);
            var budgetItemsByCatAndByMonth = new List<Dictionary<string, object>>();
            var categories = new List<Category>();
            
            presenter.Connection("test", false);
            // act
            presenter.DisplayBudgetItemsByCatAndMonth(DateTime.Now, DateTime.Now, false, 0);

            //assert
            Assert.True(view.displayBudgetCatAndMonth);
            presenter.CloseDB();
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
            int addedExpenses = 0;

            // act 

            presenter.Connection("test", false);

            //check
            Assert.True(view.displayCatList);
            Assert.True(view.filter);

            presenter.AddExpense(description, amount.ToString(),DateTime.Now.ToString() ,categoryId);
            addedExpenses++;



            // assert
            Assert.True(view.displayMessage);
            Assert.True(view.resetFields);
            Assert.Equal(addedExpenses, presenter.count);
            Assert.True(view.filter);
            presenter.CloseDB();
        }

        [Fact]
        public void CreateCategory()
        {
            
            //arrange
            UnitTest1 view = new UnitTest1();
            presenter = new Presenter(view);
            presenter.Connection("test", true);

            string description = "test";
            int index = 0;

            //Grab the current list of categories
            int catCount = view.categories.Count;

            displayCatList = false;
            // act
            presenter.CreateCat(description, index);
            //  CreateCat_AddCategoryToBudget();

            int newCatCount = view.categories.Count;

            // assert
            Assert.True(view.displayCatList);  //Change displayList to DisplayCategoriesList
            Assert.True(newCatCount != catCount);
            presenter.CloseDB();
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
            displayCatList = false;

            //act
            presenter.Connection(filename, existing);

            //assert
            Assert.True(view.displayCatList);
            Assert.True(view.filter);

            presenter.CloseDB();

        }


    }
}