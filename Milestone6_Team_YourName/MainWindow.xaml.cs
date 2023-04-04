using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Milestone6_Team_YourName
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string budgetFolder = "Budgets";
        private string initialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), budgetFolder);
        public MainWindow()
        {
            InitializeComponent();
            comboBox1.Items.Add("Chicken");
            comboBox1.Items.Add("Nuggets");
            comboBox1.Items.Add("Pizza");

            if (!Directory.Exists(initialDirectory))
            {
                Directory.CreateDirectory(initialDirectory);
            }
        }

        private void btn_closePage(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOpenBudgetFileLocation(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = initialDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
            }
            
        }
    }
}
