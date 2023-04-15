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
        public void TestAccentBtnRedClicked()
        {
            AccentBtnRedClicked();
            Assert.True(accentBtnRedClicked);
        }

        [Fact]
        public void TestAccentBtnBlueClicked()
        {
            AccentBtnBlueClicked();
            Assert.True(accentBtnBlueClicked);
        }

        [Fact]
        public void TestAccentBtnYellowClicked()
        {
            AccentBtnYellowClicked();
            Assert.True(accentBtnYellowClicked);
        }

        [Fact]
        public void TestAccentBtnOrangeClicked()
        {
            AccentBtnOrangeClicked();
            Assert.True(accentBtnOrangeClicked);
        }

        [Fact]
        public void TestAccentBtnPinkClicked()
        {
            AccentBtnPinkClicked();
            Assert.True(accentBtnPinkClicked);
        }

        [Fact]
        public void TestAccentBtnLavenderClicked()
        {
            AccentBtnLavenderClicked();
            Assert.True(accentBtnLavenderClicked);
        }

        [Fact]
        public void TestAccentBtnBlueGreenClicked()
        {
            AccentBtnBlueGreenClicked();
            Assert.True(accentBtnBlueGreenClicked);
        }

        [Fact]
        public void TestNewFile_Click()
        {
            NewFile_Click();
            Assert.True(newFile_Click);
        }

        [Fact]
        public void TestCreateNewCategory_Click()
        {
            CreateNewCategory_Click();
            Assert.True(createNewCategory);
        }

    }
}