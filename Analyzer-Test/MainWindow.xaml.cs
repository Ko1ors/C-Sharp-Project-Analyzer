using Analyzer_Test.Analyzers;
using Analyzer_Test.Analyzers.Design;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeMetrics;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Analyzer_Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Create();
        }

        public MSBuildWorkspace CreateWorkspace()
        {
            MSBuildLocator.RegisterDefaults();
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            ws.LoadMetadataForReferencedProjects = true;
            return ws;
        }

        public Solution OpenSolution(MSBuildWorkspace ws, string solutionFilePath)
        {
            Task<Solution> slnTask = ws.OpenSolutionAsync(solutionFilePath);
            slnTask.Wait();
            return slnTask.Result;
        }


        public ImmutableArray<(string, CodeAnalysisMetricData)> ComputeSolutionMetric(Solution sln)
        {
            var builder = ImmutableArray.CreateBuilder<(string, CodeAnalysisMetricData)>();
            foreach (var project in sln.Projects.ToList())
            {
                var compilationTask = project.GetCompilationAsync();
                compilationTask.Wait();
                var com = compilationTask.Result;
                var metric = CodeAnalysisMetricData.ComputeAsync(com.Assembly, new CodeMetricsAnalysisContext(com, CancellationToken.None)).Result;
                builder.Add((project.FilePath, metric));
            }
            return builder.ToImmutable();
        }

        public void AnalyzeSolution(Solution sln)
        {
            foreach (var project in sln.Projects.ToList())
            {
                var compilationTask = project.GetCompilationAsync();
                compilationTask.Wait();
                var com = compilationTask.Result;
                AllAnalyzers.compilation = com;
                foreach (SyntaxTree tree in com.SyntaxTrees)
                {
                    AnalyzeTree(tree);
                    var node = tree.GetRoot();
                    DesignAnalyzers.Analyze(node);
                }
            }
        }

        public void AnalyzeTree(SyntaxTree tree)
        {
            var node = tree.GetRoot();
            if (node.ChildNodes().Count() > 0)
                AnalyzeNodes(node.ChildNodes().ToList());
            DesignAnalyzers.Analyze(node);
        }

        public void AnalyzeNodes(List<SyntaxNode> nodeList)
        {
            foreach (var node in nodeList)
            {
                if (node.ChildNodes().Count() > 0)
                    AnalyzeNodes(node.ChildNodes().ToList());
                DesignAnalyzers.Analyze(node);
            }
        }

        public void Create()
        {
            var ws = CreateWorkspace();
            var sln = OpenSolution(ws, @"C:\Users\Ko1ors\source\repos\WpfApp5\WpfApp5.sln");
            var metricList = ComputeSolutionMetric(sln).ToList();
            metricList.ForEach(e => textBox.Text += e.ToString() + "\n");
            AnalyzeSolution(sln);

            /*
            MSBuildLocator.RegisterDefaults();
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            ws.LoadMetadataForReferencedProjects = true;
            Task<Solution> slnTask = ws.OpenSolutionAsync(@"C:\Users\Ko1ors\source\repos\WpfApp5\WpfApp5.sln");
            slnTask.Wait();

            var sln = slnTask.Result;
            var b = ComputeSolutionMetric(sln);
            var builder = ImmutableArray.CreateBuilder<(string, CodeAnalysisMetricData)>();
            foreach (var project in sln.Projects.ToList())
            {
                var com = project.GetCompilationAsync().Result;
                var metric = CodeAnalysisMetricData.ComputeAsync(com.Assembly, new CodeMetricsAnalysisContext(com, CancellationToken.None)).Result;
                textBox.Text += metric.ToString() + "\n";
                builder.Add((project.FilePath, metric));
            }

            var proj = sln.Projects.Single();

            var compilationTask = proj.GetCompilationAsync();
            compilationTask.Wait();
            var compilation = compilationTask.Result;
            AllAnalyzers.compilation = compilation;
            Console.WriteLine(compilation.SyntaxTrees.Count());
            foreach (SyntaxTree tree in compilation.SyntaxTrees)
            {
                AnalyzeTree(tree);
                var node = tree.GetRoot();
                DesignAnalyzers.Analyze(node);
            }*/
        }
    }
}
