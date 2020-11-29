using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Data
{
    public class SolutionShortInfo
    {
        public string solutionFilePath { get; set; }

        public string GetName()
        {
           return solutionFilePath.Split('\\').Last().Split('.').First();
        }

        public override string ToString()
        {
            return GetName();
        }
    }
}
