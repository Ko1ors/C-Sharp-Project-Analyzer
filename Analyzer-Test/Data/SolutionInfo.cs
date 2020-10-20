﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Data
{
    public class SolutionInfo
    {
        public MSBuildWorkspace ws;
        public string solutionFilePath;
        public Solution sln;
    }
}
