﻿using System;
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

namespace Analyzer_Test.UI.UserControls
{
    /// <summary>
    /// Логика взаимодействия для DepthOfInheritanceUC.xaml
    /// </summary>
    public partial class DepthOfInheritanceUC : UserControl
    {
        private int min = 1;
        private int greenMax = 10;
        private int yellowMax = 15;

        public DepthOfInheritanceUC()
        {
            InitializeComponent();
        }
        public void SetValue(int value)
        {
            if (value < min)
                throw new Exception();

            if (value <= greenMax)
            {
                CircleProgressBar.Value = 100;
                CircleProgressBar.IndicatorBrush = new SolidColorBrush(Colors.Green);
            }
            else if (value > greenMax && value <= yellowMax)
            {
                CircleProgressBar.Value = 50;
                CircleProgressBar.IndicatorBrush = new SolidColorBrush(Colors.Yellow);
            }
            else if (value > yellowMax)
            {
                CircleProgressBar.Value = 10;
                CircleProgressBar.IndicatorBrush = new SolidColorBrush(Colors.Red);
            }
            CircleProgressBar.SetTextValue(value);
        }
    }
}
