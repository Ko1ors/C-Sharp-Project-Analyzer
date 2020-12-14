using Analyzer_Test.Data;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeMetrics;
using Microsoft.CodeAnalysis.MSBuild;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Analyzer_Test
{
    public static class ProjectExtension
    {
        /// <summary>
        /// Checks if workspace has already been registered
        /// </summary>
        private static bool isWorkspaceRegistered;

        /// <summary>
        /// Path to file with recently opened solutions
        /// </summary>
        private static readonly string solutionsPath = AppDomain.CurrentDomain.BaseDirectory + "recentsolutions";


        /// <summary>
        /// Create and return the MSBuild Workspace
        /// </summary>
        /// <returns>the MSBuildWorkspace object</returns>
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


        /// <summary>
        /// Create and return the Project Solution
        /// </summary>
        /// <returns>the Project Solution object</returns>
        /// <param name="ws">Used to load MSBuild projects and solutions</param>
        /// <param name="solutionFilePath">Used to specify path to solution file</param>
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

        /// <summary>
        /// Compute and return the Solution Metrics
        /// </summary>
        /// <returns>Immutable array of Solution Metrics</returns>
        /// <param name="comList">Immutable array of Project Compilations</param>
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


        /// <summary>
        /// Compile the Solution
        /// </summary>
        /// <returns>Immutable array of Project Compilation</returns>
        /// <param name="sln">Used to represents a set of projects and their source code</param>
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


        /// <summary>
        /// Get a list of recently opened solutions and return it
        /// </summary>
        /// <returns>List of recently opened solutions</returns>
        public static List<SolutionShortInfo> GetRecentSolutions()
        {
            if (File.Exists(solutionsPath))
                return JsonConvert.DeserializeObject<List<SolutionShortInfo>>(File.ReadAllText(solutionsPath));
            return new List<SolutionShortInfo>();
        }

        /// <summary>
        /// Save a list of recently opened solutions
        /// </summary>
        /// <param name="solutions">Used to represents a list of short information about solutions</param>
        public static void SaveRecentSolutions(List<SolutionShortInfo> solutions)
        {
            File.WriteAllText(solutionsPath, JsonConvert.SerializeObject(solutions));
        }
    }
}
