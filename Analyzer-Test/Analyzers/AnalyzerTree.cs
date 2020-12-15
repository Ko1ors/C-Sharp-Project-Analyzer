using System.Linq;
using System.Windows.Controls;

namespace Analyzer_Test.Analyzers
{
    public static class AnalyzerTree
    {
        private static TreeViewItem Generate(AnalyzerBase node)
        {
            var item = new TreeViewItem() { Header = node.GetName() };
            foreach (var child in node.GetChildren() ?? Enumerable.Empty<AnalyzerBase>())
            {
                item.Items.Add(Generate(child));
            }
            return item;
        }

    }
}
