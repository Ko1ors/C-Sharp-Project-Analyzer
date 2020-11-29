using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeMetrics;
using Microsoft.CodeAnalysis.MSBuild;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Analyzer_Test
{
    public static class ProjectExtension
    {
        private static bool isWorkspaceRegistered;
        public static MSBuildWorkspace CreateWorkspace()
        {
            if (!isWorkspaceRegistered)
            {
                MSBuildLocator.RegisterDefaults();
                isWorkspaceRegistered = true;
            }
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            ws.LoadMetadataForReferencedProjects = true;
            return ws;
        }

        public static Solution TryOpenSolution(MSBuildWorkspace ws, string solutionFilePath)
        {
            if (ws != null && solutionFilePath != null && File.Exists(solutionFilePath))
            {
                Task<Solution> slnTask = ws.OpenSolutionAsync(solutionFilePath);
                slnTask.Wait();
                return slnTask.Result;
            }
            return null;
        }

        public static ImmutableArray<(string, CodeAnalysisMetricData)>? TryComputeSolutionMetric(ImmutableArray<(string, Compilation)>? comList)
        {
            if (comList?.Count() > 0)
            {
                var builder = ImmutableArray.CreateBuilder<(string, CodeAnalysisMetricData)>();
                foreach (var com in comList)
                {
                    var metric = CodeAnalysisMetricData.ComputeAsync(com.Item2.Assembly, new CodeMetricsAnalysisContext(com.Item2, CancellationToken.None)).Result;
                    builder.Add((com.Item1, metric));
                }
                return builder.ToImmutable();
            }
            return null;
        }

        public static ImmutableArray<(string, Compilation)>? TryCompileSolution(Solution sln)
        {
            if (sln != null && sln.Projects.Count() > 0)
            {
                var builder = ImmutableArray.CreateBuilder<(string, Compilation)>();
                foreach (var project in sln.Projects.ToList())
                {
                    var compilationTask = project.GetCompilationAsync();
                    compilationTask.Wait();
                    builder.Add((project.FilePath, compilationTask.Result));
                }
                return builder.ToImmutable();
            }
            return null;
        }
    }
}
