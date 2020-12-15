using Analyzer_Test.Data;
using Analyzer_Test.Handlers.ProjectHandlers;
using Analyzer_Test.UI.UserControls;
using Microsoft.CodeAnalysis.CodeMetrics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
 
namespace Analyzer_Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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


        public MainWindow()
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
            LoadRecentSolutions();
        }

        private void LoadRecentSolutions()
        {
            var list = ProjectExtension.GetRecentSolutions();
            solutionListView.Items.Clear();
            solutionListView.ItemsSource = list;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HideStartDialog()
        {
            StartDialog.Visibility = Visibility.Hidden;
        }

        private void OpenSolution(String path)
        {
            HideStartDialog();
            var handler = ProjectHandler.SetHandlers();
            var si = new Data.SolutionInfo();
            si.solutionFilePath = path;
            var result = handler.Handle(si);
            if (result.Status == "7")
            {
                var m = result.Metric.Value[0];

                var list = GetMaintainabilityIndexByClasses(m.Item2);
                list = list.OrderBy(t => t.Item2).Take(6).ToList();

                var listClassCoupling = GetClassCouplingByClasses(m.Item2);
                int avgClassCoupling = Convert.ToInt32(listClassCoupling.Average(e => e.Item2));
                listClassCoupling = listClassCoupling.Where(e => e.Item2 > 8).ToList();

                var listCC = GetCyclomaticComplexityByMethods(m.Item2);
                int avgCC = Convert.ToInt32(listCC.Average(e => e.Item2));
                listCC = listCC.Where(e => e.Item2 > 10).ToList();

                tmUC.MetricTextBlock1.Text = $"Project name: {m.Item1.Split('\\').Last().Split('.').First()}";
                tmUC.MetricTextBlock2.Text = $"Maintainability index: {m.Item2.MaintainabilityIndex}";
                tmUC.MetricTextBlock3.Text = $"Cyclomatic complexity: {m.Item2.CyclomaticComplexity}";
                tmUC.MetricTextBlock4.Text = $"Depth of inheritance: {m.Item2.DepthOfInheritance}";
                tmUC.MetricTextBlock5.Text = $"Class coupling: {m.Item2.CoupledNamedTypes.Count}";
                tmUC.MetricTextBlock6.Text = $"Executable lines: {m.Item2.ExecutableLines}";
                tmUC.MetricTextBlock7.Text = $"Source lines: {m.Item2.SourceLines}";
                miUC.SetValue(m.Item2.MaintainabilityIndex);
                miUC.ClearClassList();
                foreach (var item in list)
                {
                    miUC.AddClass(item.Item1, item.Item2);
                }

                accUC.SetValue(avgCC);
                accUC.ClearMethodList();
                foreach (var item in listCC)
                {
                    accUC.AddMethod(item.Item1, item.Item2);
                }

                avgClassCouplingUC.SetValue(avgClassCoupling);
                avgClassCouplingUC.ClearClassList();
                foreach (var item in listClassCoupling)
                {
                    avgClassCouplingUC.AddClass(item.Item1, item.Item2);
                }

                ccUC.SetValue(m.Item2.CyclomaticComplexity);
                classCouplingUC.SetValue(m.Item2.CoupledNamedTypes.Count);
                sourceCodeLinesUC.SetValue((int)m.Item2.SourceLines);
                executableCodeLinesUC.SetValue((int)m.Item2.ExecutableLines);
                doiUC.SetValue(m.Item2.DepthOfInheritance.GetValueOrDefault());
                listView.Visibility = Visibility.Visible;

                var solutions = new List<SolutionShortInfo>() { si };
                solutions.AddRange(ProjectExtension.GetRecentSolutions());
                solutions = solutions.GroupBy(e => e.solutionFilePath).Select(e => e.First()).ToList();
                ProjectExtension.SaveRecentSolutions(solutions);
            }
        }

        private void OpenSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "solution files (*.sln)|*.sln";
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                OpenSolution(path);   
            }
        }  
        
        private List<(string, int)> GetMaintainabilityIndexByClasses(CodeAnalysisMetricData data)
        {
            var classMainList = new List<(string, int)>();
            if (data.Symbol.Kind == Microsoft.CodeAnalysis.SymbolKind.NamedType)
                classMainList.Add((data.Symbol.Name,data.MaintainabilityIndex));
            foreach(var child in data.Children)
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

        private void solutionListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var solution = (sender as ListView).SelectedItem as SolutionShortInfo;
            OpenSolution(solution.solutionFilePath);
        }
    }
}
