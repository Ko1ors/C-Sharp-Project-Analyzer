using Analyzer_Test.Analyzers;
using Analyzer_Test.Analyzers.Design;
using Analyzer_Test.Handlers.ProjectHandlers;
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

        public MainWindow()
        {
            InitializeComponent();
            Create();
        }

        /*
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
        }*/

        private TreeViewItem GenerateTree(AnalyzerBase node)
        {
            var item = new TreeViewItem() { Header = node.GetName() };
            foreach (var child in node.GetChildren() ?? Enumerable.Empty<AnalyzerBase>())
            {
                item.Items.Add(GenerateTree(child));
            }
            return item;
        }

        public void Create()
        {
            var si = new Data.SolutionInfo();
            si.solutionFilePath = @"C:\Users\Ko1ors\source\repos\WpfApp5\WpfApp5.sln";
            var w = new WorkspaceHandler();
            var s = new SolutionHandler();
            var m = new MetricHandler();
            AnalyzerBase root = new AnalyzerComposite(new AllAnalyzers());
            AnalyzerBase da = new AnalyzerComposite(new DesignAnalyzers());
            da.Add(new AnalyzerLeaf(new CatchEmptyAnalyzer()));
            da.Add(new AnalyzerLeaf(new MakeMethodStaticAnalyzer()));
            root.Add(da);
            tree.Items.Add(GenerateTree(root));
            w.SetHandler(s);
            s.SetHandler(m);
            var result = w.Handle(si);



            //var ws = ProjectCreator.CreateWorkspace();
            // var sln = ProjectCreator.OpenSolution(ws, @"C:\Users\Ko1ors\source\repos\WpfApp5\WpfApp5.sln");
            //var metricList = ProjectCreator.ComputeSolutionMetric(sln).ToList();
            //metricList.ForEach(e => textBox.Text += e.ToString() + "\n");
            //AnalyzeSolution(sln);

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
