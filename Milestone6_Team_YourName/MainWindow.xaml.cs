using ModernWpf;
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
using Microsoft.Win32;

namespace Milestone6_Team_YourName
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ApplicationTheme _theme = ApplicationTheme.Dark;
         

        private static string budgetFolder = "Budgets";
        private string initialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), budgetFolder);
        public MainWindow()
        {
            InitializeComponent();
            expenseDate.SelectedDate = DateTime.Now;
      

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

        private void btn_AddExpense_Clck(object sender, RoutedEventArgs e)
        {
            bool errorWhileAddingAnExpense = false;
            // find a way to 

            if(string.IsNullOrEmpty(expense.Text) || string.IsNullOrEmpty(description.Text) || string.IsNullOrEmpty(amount.Text) )
            {
                errorWhileAddingAnExpense = true;
            }
            if (errorWhileAddingAnExpense)
            {
                MessageBox.Show("Please fill out all of the input fields");
            }
            else
            {
                MessageBox.Show("Expense was successfully added");
                expense.Text = string.Empty;
                description.Text = string.Empty;
                amount.Text = string.Empty;
                //date.DataContext = DateTime.Now;
                // find a way to add this to the expense list and have a display expenses button. 
            }
        }

        private void btn_ClearExpense_Clck(object sender, RoutedEventArgs e)
        {
            expense.Text = string.Empty;
            description.Text = string.Empty;
            amount.Text = string.Empty;
        }

        private void BtnBlue_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Color.FromRgb(0, 255, 255));
            MainGrid.Background = brush;
            AllWindow.Background = brush;
            Brush brushText = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            foreach (var element in stackPanel.Children)
            {
                if (element is TextBlock block)
                {
                    TextBlock text = (TextBlock)element;
                    text.Foreground = brushText;
                }
            }
        }

        private void BtnRed_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            MainGrid.Background = brush;
            AllWindow.Background = brush;
            Brush brushText = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            foreach (var element in stackPanel.Children)
            {
                if (element is TextBlock block)
                {
                    TextBlock text = (TextBlock)element;
                    text.Foreground = brushText;
                }
            }
        }

        private void BtnBlack_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            MainGrid.Background = brush;
            AllWindow.Background = brush;
            Brush brushText = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            foreach (var element in stackPanel.Children)
            {
                if (element is TextBlock block)
                {
                    TextBlock text = (TextBlock)element;
                    text.Foreground = brushText;
                }
            }
        }

        private void BtnLight_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            MainGrid.Background = brush;
            AllWindow.Background = brush;
            Brush brushText = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            btn_AddExpense.Foreground = brushText;
            btn_ClearExpense.Foreground = brushText;


            foreach (var element in stackPanel.Children)
            {
                if (element is TextBlock block)
                {
                    TextBlock text = (TextBlock)element;
                    text.Foreground = brushText;
                }
            }
        }

        private void BtnBeige_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Color.FromRgb(245, 245, 220));
            MainGrid.Background = brush;
            AllWindow.Background = brush;
            Brush brushText = new SolidColorBrush(Color.FromRgb(0, 0, 0));


            btn_AddExpense.Foreground = brushText;
            btn_ClearExpense.Foreground = brushText;

            foreach (var element in stackPanel.Children)
            {
                if (element is TextBlock block)
                {
                    TextBlock text = (TextBlock)element;
                    text.Foreground = brushText;
                }
            }
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            SubmitFile.IsEnabled = true;
            budgetFileName.IsEnabled = true;
        }

        private void Existing_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitFile_Click_1(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (Directory.Exists(initialDirectory))
            {
                File.Create(initialDirectory + "\\" + budgetFileName.Text + ".db");
                openFileDialog.InitialDirectory = initialDirectory;
                currentBudgetFile.Text += " "+ budgetFileName.Text;
                SubmitFile.IsEnabled = false;


            }
        }



    }
}
