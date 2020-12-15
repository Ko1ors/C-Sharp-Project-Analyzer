
using Analyzer_Test.Handlers.ProjectHandlers;
using System;

namespace Analyzer_Test
{
    public static class Driver
    {
        public static void Test(string solutionPath)
        {
            var si = new Data.SolutionInfo();
            si.solutionFilePath = solutionPath;
            var wh = new WorkspaceHandler();
            var sh = new SolutionHandler();
            var ch = new CompilationHandler();
            var mh = new MetricHandler();
            wh.SetHandler(sh);
            sh.SetHandler(ch);
            ch.SetHandler(mh);
            var result = wh.Handle(si);
            Console.WriteLine($"Status: {result.Status}");
            Console.WriteLine($"Message: {result.Message}");
            foreach (var m in result.Metric)
            {
                Console.WriteLine($"Project name: {m.Item1}");
                Console.WriteLine($"Maintainability index: {m.Item2.MaintainabilityIndex}");
                Console.WriteLine($"Cyclomatic complexity: {m.Item2.CyclomaticComplexity}");
                Console.WriteLine($"Depth of inheritance: {m.Item2.DepthOfInheritance}");
                Console.WriteLine($"Executable lines: {m.Item2.ExecutableLines}");
                Console.WriteLine($"Source lines: {m.Item2.SourceLines}");
            }
        }
    }
}
