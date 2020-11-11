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

namespace Analyzer_Test.UI.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CircleProgressBarMiniUC.xaml
    /// </summary>
    public partial class CircleProgressBarMiniUC : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(String), typeof(CircleProgressBarMiniUC));
        public String Title
        {
            get { return this.GetValue(TitleProperty).ToString(); }
            set { this.SetValue(TitleProperty, value); }
        }
        public CircleProgressBarMiniUC()
        {
            InitializeComponent();
            CircleProgressBar.TextBlock.FontSize = 12;
        }

        public CircleProgressBarMiniUC(String name)
        {
            InitializeComponent();
            CircleProgressBar.TextBlock.FontSize = 12;
            Title = name;
        }

        public void SetValue(int value)
        {
            CircleProgressBar.Value = value;
        }
        public void SetTextValue(int value)
        {
            CircleProgressBar.SetTextValue(value);
        }

        public void SetColor(Color color)
        {
            CircleProgressBar.IndicatorBrush = new SolidColorBrush(color);
        }
    }
}
