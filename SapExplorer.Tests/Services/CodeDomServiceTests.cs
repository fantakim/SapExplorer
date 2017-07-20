using Microsoft.VisualStudio.TestTools.UnitTesting;
using SapExplorer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace SapExplorer.Services.Tests
{
    [TestClass()]
    public class CodeDomServiceTests
    {
        [TestMethod()]
        public void DoTest()
        {
            var cls = new CodeTypeDeclaration("CodeDOMCreatedClass");
            cls.IsClass = true;
            cls.TypeAttributes = TypeAttributes.Public;

            var ns = new CodeNamespace();
            ns.Types.Add(cls);

            var unit = new CodeCompileUnit();
            unit.Namespaces.Add(ns);

            cls.Members.Add(AddProperty("AAAA", typeof(string), "AAA 코멘트"));
            cls.Members.Add(AddProperty("BBBB", typeof(string), "BBB 코멘트"));
            
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var options = new CodeGeneratorOptions();
            options.BracingStyle = "C";

            using (var sw = new StringWriter())
            {
                provider.GenerateCodeFromCompileUnit(unit, sw, options);

                var result = sw.ToString();
            }
        }

        private CodeMemberField AddProperty(string name, Type type, string comment)
        {
            var property = new CodeMemberField();
            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            property.Name = name;
            property.Name += " { get; set; }";
            property.Type = new CodeTypeReference(type);
            property.Comments.Add(new CodeCommentStatement("<summary>", true));
            property.Comments.Add(new CodeCommentStatement(comment, true));
            property.Comments.Add(new CodeCommentStatement("</summary>", true));

            return property;
        }
    }
}