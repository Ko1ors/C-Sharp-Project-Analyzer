﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers
{
    public static class AnalysisExtension
    {
        public static bool IsGenerated(this SyntaxNode node) => (node?.SyntaxTree?.IsGenerated() ?? false) || (node?.HasAttributeOnAncestorOrSelf(generatedCodeAttributes) ?? false);

        public static bool IsGenerated(this SyntaxTree tree) => (tree.FilePath?.IsOnGeneratedFile() ?? false) || tree.HasAutoGeneratedComment();

        public static bool IsOnGeneratedFile(this string filePath) =>
            Regex.IsMatch(filePath, @"(\\service|\\TemporaryGeneratedFile_.*|\\assemblyinfo|\\assemblyattributes|\.(g\.i|g|designer|generated|assemblyattributes))\.(cs|vb)$",
                RegexOptions.IgnoreCase);

        private static readonly string[] generatedCodeAttributes = new string[] { "DebuggerNonUserCode", "GeneratedCode", "DebuggerNonUserCodeAttribute", "GeneratedCodeAttribute" };

        public static bool HasAutoGeneratedComment(this SyntaxTree tree)
        {
            var root = tree.GetRoot();
            if (root == null) return false;
            var firstToken = root.GetFirstToken();
            SyntaxTriviaList trivia;
            if (firstToken == default(SyntaxToken))
            {
                var token = ((CompilationUnitSyntax)root).EndOfFileToken;
                if (!token.HasLeadingTrivia) return false;
                trivia = token.LeadingTrivia;
            }
            else
            {
                if (!firstToken.HasLeadingTrivia) return false;
                trivia = firstToken.LeadingTrivia;
            }

            var comments = trivia.Where(t => t.IsKind(SyntaxKind.SingleLineCommentTrivia) || t.IsKind(SyntaxKind.MultiLineCommentTrivia));
            return comments.Any(t =>
            {
                var s = t.ToString();
                return s.Contains("<auto-generated") || s.Contains("<autogenerated");
            });
        }

    public static bool HasAttributeOnAncestorOrSelf(this SyntaxNode node, string attributeName)
        {
            var csharpNode = node as CSharpSyntaxNode;
            if (csharpNode == null) throw new Exception("Node is not a C# node");
            return csharpNode.HasAttributeOnAncestorOrSelf(attributeName);
        }

        public static bool HasAttributeOnAncestorOrSelf(this SyntaxNode node, params string[] attributeNames)
        {
            var csharpNode = node as CSharpSyntaxNode;
            if (csharpNode == null) throw new Exception("Node is not a C# node");
            foreach (var attributeName in attributeNames)
                if (csharpNode.HasAttributeOnAncestorOrSelf(attributeName)) return true;
            return false;
        }
    }
}
