using Milestone6_Team_YourName;
using Newtonsoft.Json.Bson;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Budget;

namespace TestProject1
{
    public class UnitTest1 : ViewInterface
    {
        bool displayCategoryTypes= false;
        bool displayList = false;

       

        public void DisplayCatTypes(List<Category.CategoryType> categoryTypes)
        {
            displayCategoryTypes = true;
        }

        public void DisplayList(List<Category> categories)
        {
            displayList= true;  
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
            DisplayList(categories);
            // makes displayList true 

            // assert
            Assert.True(displayList);

        }
    }
}