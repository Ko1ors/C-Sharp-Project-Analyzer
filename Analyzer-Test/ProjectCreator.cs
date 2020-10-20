using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeMetrics;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Analyzer_Test
{
    public static class ProjectCreator
    {
        public static MSBuildWorkspace CreateWorkspace()
        {
            MSBuildLocator.RegisterDefaults();
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            ws.LoadMetadataForReferencedProjects = true;
            return ws;
        }

        public static Solution OpenSolution(MSBuildWorkspace ws, string solutionFilePath)
        {
            Task<Solution> slnTask = ws.OpenSolutionAsync(solutionFilePath);
            slnTask.Wait();
            return slnTask.Result;
        }


        public static ImmutableArray<(string, CodeAnalysisMetricData)> ComputeSolutionMetric(Solution sln)
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
    }
}
