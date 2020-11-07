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
    /// Логика взаимодействия для MaintainabilityIndexMiniUC.xaml
    /// </summary>
    public partial class MaintainabilityIndexMiniUC : UserControl
    {
        public static readonly DependencyProperty ClassNameProperty = DependencyProperty.Register("ClassName", typeof(String), typeof(MaintainabilityIndexMiniUC));
        public String ClassName
        {
            get { return this.GetValue(ClassNameProperty).ToString(); }
            set { this.SetValue(ClassNameProperty, value); }
        }
        public MaintainabilityIndexMiniUC()
        {
            InitializeComponent();
        }

        public MaintainabilityIndexMiniUC(String className)
        {
            InitializeComponent();
            ClassName = className;
        }
    }
}
