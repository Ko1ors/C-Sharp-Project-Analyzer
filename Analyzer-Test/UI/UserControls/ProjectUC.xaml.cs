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
    /// Логика взаимодействия для ProjectUC.xaml
    /// </summary>
    public partial class ProjectUC : UserControl
    {
        TotalMetricUC tmUC = new TotalMetricUC();
        MaintainabilityIndexUC miUC = new MaintainabilityIndexUC();
        DepthOfInheritanceUC doiUC = new DepthOfInheritanceUC();
        AverageCyclomaticComplexityUC accUC = new AverageCyclomaticComplexityUC();
        CyclomaticComplexityUC ccUC = new CyclomaticComplexityUC();
        ClassCouplingUC classCouplingUC = new ClassCouplingUC();
        AverageClassCouplingUC avgClassCouplingUC = new AverageClassCouplingUC();
        SourceCodeLinesUC sourceCodeLinesUC = new SourceCodeLinesUC();
        ExecutableCodeLinesUC executableCodeLinesUC = new ExecutableCodeLinesUC();

        public static readonly DependencyProperty ProjectNameProperty = DependencyProperty.Register("ProjectName", typeof(string), typeof(ProjectUC));

        public string ProjectName
        {
            get
            {
                return (string)this.GetValue(ProjectNameProperty);
            }
            set
            {
                this.SetValue(ProjectNameProperty, value.ToUpper());
            }
        }

        public ProjectUC()
        {
            InitializeComponent();
            listView.Items.Add(tmUC);
            listView.Items.Add(miUC);
            listView.Items.Add(doiUC);
            listView.Items.Add(accUC);
            listView.Items.Add(ccUC);
            listView.Items.Add(classCouplingUC);
            listView.Items.Add(avgClassCouplingUC);
            listView.Items.Add(sourceCodeLinesUC);
            listView.Items.Add(executableCodeLinesUC);
        }

        public void SetTotalMetric(string[] metricParams)
        {
            if (metricParams.Length != 7)
                throw new Exception("Params was setted incorrect");
            tmUC.MetricTextBlock1.Text = metricParams[0];
            tmUC.MetricTextBlock2.Text = metricParams[1];
            tmUC.MetricTextBlock3.Text = metricParams[2];
            tmUC.MetricTextBlock4.Text = metricParams[3];
            tmUC.MetricTextBlock5.Text = metricParams[4];
            tmUC.MetricTextBlock6.Text = metricParams[5];
            tmUC.MetricTextBlock7.Text = metricParams[6];
        }
    }
}
