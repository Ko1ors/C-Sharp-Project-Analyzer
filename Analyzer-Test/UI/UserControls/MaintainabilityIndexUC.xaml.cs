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
    /// Логика взаимодействия для MaintainabilityIndexUC.xaml
    /// </summary>
    public partial class MaintainabilityIndexUC : UserControl
    {
        public MaintainabilityIndexUC()
        {
            InitializeComponent();
        }

        public void SetValue(int value)
        {
            if (value > 100)
                throw new Exception();
            CircleProgressBar.Value = value;
            if (value >= 20)
                CircleProgressBar.IndicatorBrush = new SolidColorBrush(Colors.Green);
            else if(value >= 10 && value < 20)
                CircleProgressBar.IndicatorBrush = new SolidColorBrush(Colors.Yellow);
            else if(value < 10)
                CircleProgressBar.IndicatorBrush = new SolidColorBrush(Colors.Red);
        }

        public void ClearClassList()
        {
            classListView.Items.Clear();
        }

        public void AddClass(string name,int value)
        {
            var m = new MaintainabilityIndexMiniUC(name);
            m.SetValue(value);
            classListView.Items.Add(m);
        }
    }
}
