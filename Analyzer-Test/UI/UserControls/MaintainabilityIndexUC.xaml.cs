using System;
using System.Windows.Controls;
using System.Windows.Media;

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
            CircleProgressBar.IndicatorBrush = new SolidColorBrush(GetColorByValue(value));
        }

        public void ClearClassList()
        {
            classListView.Items.Clear();
        }

        public void AddClass(string name, int value)
        {
            var m = new CircleProgressBarMiniUC(name);
            m.SetValue(value);
            m.SetColor(GetColorByValue(value));
            classListView.Items.Add(m);
        }

        private Color GetColorByValue(int value)
        {
            if (value >= 20)
                return Colors.Green;
            else if (value >= 10 && value < 20)
                return Colors.Yellow;
            else if (value < 10)
                return Colors.Red;
            throw new Exception();
        }
    }
}
