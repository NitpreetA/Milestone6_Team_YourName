using Newtonsoft.Json.Bson;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace TestProject1
{
    public class UnitTest1 
    {

        
        public bool accentBtnRedClicked;
        public bool accentBtnBlueClicked;
        public bool accentBtnYellowClicked;
        public bool accentBtnOrangeClicked;
        public bool accentBtnPinkClicked;
        public bool accentBtnLavenderClicked;
        public bool accentBtnBlueGreenClicked;
        public bool createNewCategory;
        public bool newFile_Click;

        public void AccentBtnRedClicked()
        {
            accentBtnRedClicked = true;
        }

        public void AccentBtnBlueClicked()
        {
            accentBtnBlueClicked = true;
        }

        public void AccentBtnYellowClicked()
        {
            accentBtnYellowClicked = true;
        }

        public void AccentBtnOrangeClicked()
        {
            accentBtnOrangeClicked = true;
        }

        public void AccentBtnPinkClicked()
        {
            accentBtnPinkClicked = true;
        }

        public void AccentBtnLavenderClicked()
        {
            accentBtnLavenderClicked = true;
        }

        public void AccentBtnBlueGreenClicked()
        {
            accentBtnBlueGreenClicked = true;
        }

        public void NewFile_Click()
        {
            newFile_Click = true;
        }

        public void CreateNewCategory_Click()
        {
            createNewCategory = true;
        }




        [Fact]
        public void TestCreateNewCategory()
        {
            UnitTest1 test = new UnitTest1();

            test.CreateNewCategory_Click();
          
            Assert.True(createNewCategory);
           
        }

    }
}