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
    /// Логика взаимодействия для AverageCyclomaticComplexityUC.xaml
    /// </summary>
    public partial class AverageCyclomaticComplexityUC : UserControl
    {
        private int min = 1;
        private int greenMax = 10;
        private int yellowMax = 20;

        public AverageCyclomaticComplexityUC()
        {
            InitializeComponent();
        }

        public void SetValue(int value)
        {
            if (value < min)
                throw new Exception();
            CircleProgressBar.Value = ConvertValue(value);
            CircleProgressBar.IndicatorBrush = new SolidColorBrush(GetColorByValue(value));
            CircleProgressBar.SetTextValue(value);
        }

        public void ClearMethodList()
        {
            methodListView.Items.Clear();
        }

        public void AddMethod(string name, int value)
        {
            var m = new CircleProgressBarMiniUC(name);
            m.SetValue(ConvertValue(value));
            m.SetColor(GetColorByValue(value));
            m.SetTextValue(value);
            methodListView.Items.Add(m);
        }

        private int ConvertValue(int value)
        {
            if (value <= greenMax)
                return 100;
            else if (value > greenMax && value <= yellowMax)
                return 50;
            else if (value > yellowMax)
                return 10;

            throw new Exception();
        }

        private Color GetColorByValue(int value)
        {
            if (value <= greenMax)
                return Colors.Green;
            else if (value > greenMax && value <= yellowMax)
                return Colors.Yellow;
            else if (value > yellowMax)
                return Colors.Red;

            throw new Exception();
        }
    }
}
