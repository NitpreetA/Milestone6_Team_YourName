﻿using ModernWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Milestone6_Team_YourName
{
    public partial class MainWindow : Window, ViewInterface
    {
        //-------------------------------------------------
        //                   Accents Menu
        //-------------------------------------------------

        private void AccentBtnRed_Click(object sender, RoutedEventArgs e)
        {
            _accent = Colors.Red;
            ThemeManager.Current.AccentColor = _accent;
        }

        private void AccentBtnBlue_Click(object sender, RoutedEventArgs e)
        {
            _accent = Colors.Blue;
            ThemeManager.Current.AccentColor = _accent;
        }

        private void AccentBtnYellow_Click(object sender, RoutedEventArgs e) => SetAccent("#FFB900");

        private void AccentBtnOrange_Click(object sender, RoutedEventArgs e) => SetAccent("#F7630C");

        private void AccentBtnPink_Click(object sender, RoutedEventArgs e) => SetAccent("#E3008C");

        private void AccentBtnLavender_Click(object sender, RoutedEventArgs e) => SetAccent("#615FAE");

        private void AccentBtnBlueGreen_Click(object sender, RoutedEventArgs e) => SetAccent("#00B294");

        private void SetAccent(string colorCode)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colorCode);
            _accent = color;
            ThemeManager.Current.AccentColor = _accent;
        }

        //-------------------------------------------------
        //                  Background Menu
        //-------------------------------------------------

        private void BgBtnDefaultDark_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Colors.Black);
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            Window.Background = brush;

        }

        private void BgBtnDefaultLight_Click(object sender, RoutedEventArgs e)
        {
            Brush brush = new SolidColorBrush(Colors.White);
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            Window.Background = brush;
        }

        private void BgBtnBlue_Click(object sender, RoutedEventArgs e) => SetBackground("#1DA9E2");

        private void BgBtnSalmon_Click(object sender, RoutedEventArgs e) => SetBackground("#FA8072");

        private void BgBtnEmerald_Click(object sender, RoutedEventArgs e) => SetBackground("#28643C");

        private void BgBtnLavender_Click(object sender, RoutedEventArgs e) => SetBackground("#615FAE");

        private void BgBtnCrimson_Click(object sender, RoutedEventArgs e) => SetBackground("#9A0E2A");

        private void SetBackground(string colorCode)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colorCode);
            Brush brush = new SolidColorBrush(color);

            Window.Background = brush;
        }
    }
}