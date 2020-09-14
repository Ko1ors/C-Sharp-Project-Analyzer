using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeMetrics;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


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
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            // ws.LoadMetadataForReferencedProjects = true;
            Task<Solution> slnTask = ws.OpenSolutionAsync(@"C:\Users\Ko1ors\source\repos\Ko1ors\InventoryPrice\InventoryPrice.sln");
            slnTask.Wait();

            var sln = slnTask.Result;
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
            Console.WriteLine(compilation.SyntaxTrees.Count());
            foreach (SyntaxTree tree in compilation.SyntaxTrees)
            {

            }
        }
    }
}
