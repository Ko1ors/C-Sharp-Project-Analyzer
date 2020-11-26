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
    /// Логика взаимодействия для AverageClassCouplingUC.xaml
    /// </summary>
    public partial class AverageClassCouplingUC : UserControl
    {
        private int min = 0;
        private int greenMax = 8;


        public AverageClassCouplingUC()
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

        public void ClearClassList()
        {
            classListView.Items.Clear();
        }

        public void AddClass(string name, int value)
        {
            var m = new CircleProgressBarMiniUC(name);
            m.SetValue(ConvertValue(value));
            m.SetColor(GetColorByValue(value));
            m.SetTextValue(value);
            classListView.Items.Add(m);
        }

        private int ConvertValue(int value)
        {
            if (value <= greenMax)
                return 100;
            else 
                return 10;
        }

        private Color GetColorByValue(int value)
        {
            if (value <= greenMax)
                return Colors.Green;
            else
                return Colors.Red;
        }
    }
}
