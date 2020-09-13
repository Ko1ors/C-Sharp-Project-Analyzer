using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeMetrics;
using Microsoft.CodeAnalysis.MSBuild;


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

        public void Create()
        {
            MSBuildLocator.RegisterDefaults();
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create(new Dictionary<string, string> { { "CheckForSystemRuntimeDependency", "true" } });
           // ws.LoadMetadataForReferencedProjects = true;
            var slnTask = ws.OpenSolutionAsync(@"C:\Users\Ko1ors\source\repos\Ko1ors\PPZTicketsModel\PPZModel.sln");
            slnTask.Wait();
            var sln = slnTask.Result;
            var builder = ImmutableArray.CreateBuilder<(string, CodeAnalysisMetricData)>();
            foreach (var project in sln.Projects.ToList())
            {
                var com = project.GetCompilationAsync().Result;
                var metric = CodeAnalysisMetricData.ComputeAsync(com.Assembly, new CodeMetricsAnalysisContext(com, CancellationToken.None)).Result;
                builder.Add((project.FilePath, metric));
            }

            var proj = sln.Projects.Single();

            var compilationTask = proj.GetCompilationAsync();
            compilationTask.Wait();
            var compilation = compilationTask.Result;
            Console.WriteLine(compilation.SyntaxTrees.Count());
            foreach (SyntaxTree tree in compilation.SyntaxTrees)
            {

            }
        }
    }
}
