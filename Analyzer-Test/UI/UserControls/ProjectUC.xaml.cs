using Microsoft.CodeAnalysis.CodeMetrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Analyzer_Test.UI.UserControls
{

    /// <summary>
    /// Логика взаимодействия для ProjectUC.xaml
    /// </summary>
    public partial class ProjectUC : UserControl
    {
        DepthOfInheritanceUC doiUC = new DepthOfInheritanceUC();
        AverageCyclomaticComplexityUC accUC = new AverageCyclomaticComplexityUC();
        CyclomaticComplexityUC ccUC = new CyclomaticComplexityUC();
        ClassCouplingUC classCouplingUC = new ClassCouplingUC();
        AverageClassCouplingUC avgClassCouplingUC = new AverageClassCouplingUC();
        SourceCodeLinesUC sourceCodeLinesUC = new SourceCodeLinesUC();
        ExecutableCodeLinesUC executableCodeLinesUC = new ExecutableCodeLinesUC();
        private Dictionary<string, UserControl> ucDictionary = new Dictionary<string, UserControl>();


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
            AddUC(new TotalMetricUC());
            AddUC(new MaintainabilityIndexUC());
            AddUC(new DepthOfInheritanceUC());
            AddUC(new AverageCyclomaticComplexityUC());
            AddUC(new CyclomaticComplexityUC());
            AddUC(new ClassCouplingUC());
            AddUC(new AverageClassCouplingUC());
            AddUC(new SourceCodeLinesUC());
            AddUC(new ExecutableCodeLinesUC());
        }

        private void AddUC(UserControl uc)
        {
            ucDictionary.Add(uc.GetType().Name, uc);
            listView.Items.Add(uc);
        }

        public UserControl GetUC(string uc)
        {
            return ucDictionary.FirstOrDefault(e => e.Key == uc).Value;
        }

        public void SetProject((string,CodeAnalysisMetricData) m)
        {
            var list = GetMaintainabilityIndexByClasses(m.Item2);
            list = list.OrderBy(t => t.Item2).Take(6).ToList();

            var listClassCoupling = GetClassCouplingByClasses(m.Item2);
            int avgClassCoupling = Convert.ToInt32(listClassCoupling.Average(e => e.Item2));
            listClassCoupling = listClassCoupling.Where(e => e.Item2 > 8).ToList();

            var listCC = GetCyclomaticComplexityByMethods(m.Item2);
            int avgCC = Convert.ToInt32(listCC.Average(e => e.Item2));
            listCC = listCC.Where(e => e.Item2 > 10).ToList();

            ProjectName = m.Item1.Split('\\').Last().Split('.').First();

            string[] mp = new string[7];
            mp[0] = $"Project name: {m.Item1.Split('\\').Last().Split('.').First()}";
            mp[1] = $"Maintainability index: {m.Item2.MaintainabilityIndex}";
            mp[2] = $"Cyclomatic complexity: {m.Item2.CyclomaticComplexity}";
            mp[3] = $"Depth of inheritance: {m.Item2.DepthOfInheritance}";
            mp[4] = $"Class coupling: {m.Item2.CoupledNamedTypes.Count}";
            mp[5] = $"Executable lines: {m.Item2.ExecutableLines}";
            mp[6] = $"Source lines: {m.Item2.SourceLines}";
            SetTotalMetric(mp);

            MaintainabilityIndexUC miUC = (MaintainabilityIndexUC) GetUC("MaintainabilityIndexUC");
            miUC.SetValue(m.Item2.MaintainabilityIndex);
            miUC.ClearClassList();
            foreach (var item in list)
            {
                miUC.AddClass(item.Item1, item.Item2);
            }
        }

        private List<(string, int)> GetMaintainabilityIndexByClasses(CodeAnalysisMetricData data)
        {
            var classMainList = new List<(string, int)>();
            if (data.Symbol.Kind == Microsoft.CodeAnalysis.SymbolKind.NamedType)
                classMainList.Add((data.Symbol.Name, data.MaintainabilityIndex));
            foreach (var child in data.Children)
            {
                classMainList.AddRange(GetMaintainabilityIndexByClasses(child));
            }
            return classMainList;
        }

        private List<(string, int)> GetClassCouplingByClasses(CodeAnalysisMetricData data)
        {
            var classMainList = new List<(string, int)>();
            if (data.Symbol.Kind == Microsoft.CodeAnalysis.SymbolKind.NamedType)
                classMainList.Add((data.Symbol.Name, data.CoupledNamedTypes.Count));
            foreach (var child in data.Children)
            {
                classMainList.AddRange(GetClassCouplingByClasses(child));
            }
            return classMainList;
        }

        private List<(string, int)> GetCyclomaticComplexityByMethods(CodeAnalysisMetricData data)
        {
            var methodCCList = new List<(string, int)>();
            if (data.Symbol.Kind == Microsoft.CodeAnalysis.SymbolKind.Method)
                methodCCList.Add((data.Symbol.Name, data.CyclomaticComplexity));
            foreach (var child in data.Children)
            {
                methodCCList.AddRange(GetCyclomaticComplexityByMethods(child));
            }
            return methodCCList;
        }

        public void SetTotalMetric(string[] metricParams)
        {
            if (metricParams.Length != 7)
                throw new Exception("Params was setted incorrect");
            TotalMetricUC tmUC = (TotalMetricUC) GetUC("TotalMetricUC");
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
