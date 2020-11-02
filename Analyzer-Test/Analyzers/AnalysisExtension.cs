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

        public static bool HasAttributeOnAncestorOrSelf(this CSharpSyntaxNode node, string attributeName)
        {
            var parentMethod = (BaseMethodDeclarationSyntax)node.FirstAncestorOrSelfOfType(typeof(MethodDeclarationSyntax), typeof(ConstructorDeclarationSyntax));
            if (parentMethod?.AttributeLists.HasAttribute(attributeName) ?? false)
                return true;
            var type = (TypeDeclarationSyntax)node.FirstAncestorOrSelfOfType(typeof(ClassDeclarationSyntax), typeof(StructDeclarationSyntax));
            while (type != null)
            {
                if (type.AttributeLists.HasAttribute(attributeName))
                    return true;
                type = (TypeDeclarationSyntax)type.FirstAncestorOfType(typeof(ClassDeclarationSyntax), typeof(StructDeclarationSyntax));
            }
            var property = node.FirstAncestorOrSelfOfType<PropertyDeclarationSyntax>();
            if (property?.AttributeLists.HasAttribute(attributeName) ?? false)
                return true;
            var accessor = node.FirstAncestorOrSelfOfType<AccessorDeclarationSyntax>();
            if (accessor?.AttributeLists.HasAttribute(attributeName) ?? false)
                return true;
            var anInterface = node.FirstAncestorOrSelfOfType<InterfaceDeclarationSyntax>();
            if (anInterface?.AttributeLists.HasAttribute(attributeName) ?? false)
                return true;
            var anEvent = node.FirstAncestorOrSelfOfType<EventDeclarationSyntax>();
            if (anEvent?.AttributeLists.HasAttribute(attributeName) ?? false)
                return true;
            var anEnum = node.FirstAncestorOrSelfOfType<EnumDeclarationSyntax>();
            if (anEnum?.AttributeLists.HasAttribute(attributeName) ?? false)
                return true;
            var field = node.FirstAncestorOrSelfOfType<FieldDeclarationSyntax>();
            if (field?.AttributeLists.HasAttribute(attributeName) ?? false)
                return true;
            var eventField = node.FirstAncestorOrSelfOfType<EventFieldDeclarationSyntax>();
            if (eventField?.AttributeLists.HasAttribute(attributeName) ?? false)
                return true;
            var parameter = node as ParameterSyntax;
            if (parameter?.AttributeLists.HasAttribute(attributeName) ?? false)
                return true;
            var aDelegate = node as DelegateDeclarationSyntax;
            if (aDelegate?.AttributeLists.HasAttribute(attributeName) ?? false)
                return true;
            return false;
        }

        public static bool HasAttribute(this SyntaxList<AttributeListSyntax> attributeLists, string attributeName) =>
            attributeLists.SelectMany(a => a.Attributes).Any(a => a.Name.ToString().EndsWith(attributeName, StringComparison.OrdinalIgnoreCase));

        public static T FirstAncestorOrSelfOfType<T>(this SyntaxNode node) where T : SyntaxNode =>
            (T)node.FirstAncestorOrSelfOfType(typeof(T));

        public static SyntaxNode FirstAncestorOrSelfOfType(this SyntaxNode node, params Type[] types)
        {
            var currentNode = node;
            while (true)
            {
                if (currentNode == null) break;
                foreach (var type in types)
                {
                    if (currentNode.GetType() == type) return currentNode;
                }
                currentNode = currentNode.Parent;
            }
            return null;
        }

        public static T FirstAncestorOfType<T>(this SyntaxNode node) where T : SyntaxNode
        {
            var currentNode = node;
            while (true)
            {
                var parent = currentNode.Parent;
                if (parent == null) break;
                var tParent = parent as T;
                if (tParent != null) return tParent;
                currentNode = parent;
            }
            return null;
        }

        public static SyntaxNode FirstAncestorOfType(this SyntaxNode node, params Type[] types)
        {
            var currentNode = node;
            while (true)
            {
                var parent = currentNode.Parent;
                if (parent == null) break;
                foreach (var type in types)
                {
                    if (parent.GetType() == type) return parent;
                }
                currentNode = parent;
            }
            return null;
        }
    }
}
