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

namespace Milestone6_Team_YourName
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ApplicationTheme _theme = ApplicationTheme.Dark;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_closePage(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ApplicationTheme = (ThemeManager.Current.ApplicationTheme == ApplicationTheme.Dark) ? ApplicationTheme.Light : ApplicationTheme.Dark;
            ThemeManager.Current.AccentColor = Colors.Red;
        }
    }
}
