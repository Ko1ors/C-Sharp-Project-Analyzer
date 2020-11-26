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
    /// Логика взаимодействия для ClassCouplingUC.xaml
    /// </summary>
    public partial class ClassCouplingUC : UserControl
    {
        public ClassCouplingUC()
        {
            InitializeComponent();
        }

        public void SetValue(int value)
        {
            CircleProgressBar.Value = 100;
            CircleProgressBar.IndicatorBrush = new SolidColorBrush(Colors.Gray);
            CircleProgressBar.SetTextValue(value);
        }
    }
}
