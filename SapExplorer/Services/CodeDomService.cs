using SapExplorer.Core;
using SapExplorer.Models;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SapExplorer.Services
{
    public class CodeDomService
    {
        private readonly CodeCompileUnit unit;
        private readonly CodeNamespace ns;

        /// <summary>
        /// ctor
        /// </summary>
        public CodeDomService()
        {
            unit = new CodeCompileUnit();
            ns = new CodeNamespace();

            unit.Namespaces.Add(ns);
        }

        public string Generate(string functionName, List<ParameterModel> parameters)
        {
            var cls = new CodeTypeDeclaration(functionName);
            cls.IsClass = true;
            cls.TypeAttributes = TypeAttributes.Public;

            foreach(var parameter in parameters)
            {
                var member = AddProperty(parameter);
                cls.Members.Add(member);

                var structure = parameter.Structure;
                if (structure != null)
                    AddClass(structure);
            }

            ns.Types.Add(cls);

            var provider = CodeDomProvider.CreateProvider("CSharp");
            var options = new CodeGeneratorOptions() { BracingStyle = "C" };

            using (var sw = new StringWriter())
            {
                provider.GenerateCodeFromCompileUnit(unit, sw, options);
                return sw.ToString().Replace("//;", string.Empty);
            }
        }

        public void AddClass(StructureModel structure)
        {
            var cls = new CodeTypeDeclaration(structure.Name);
            cls.IsClass = true;
            cls.TypeAttributes = TypeAttributes.Public;

            foreach(var field in structure.Fields)
            {
                var member = AddProperty(field);
                cls.Members.Add(member);
            }

            ns.Types.Add(cls);
        }

        private CodeMemberField AddProperty(ParameterModel model)
        {
            var field = new CodeMemberField();
            field.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            field.Name = model.Name;
            field.Name += " { get; set; }//";
            field.Comments.Add(new CodeCommentStatement("<summary>", true));
            field.Comments.Add(new CodeCommentStatement(model.Documentation, true));
            field.Comments.Add(new CodeCommentStatement("</summary>", true));

            if (model.Structure != null)
            {
                var t = new CodeTypeReference(typeof(List<>));
                t.TypeArguments.Add(model.Structure.Name);
                field.Type = t;
            }
            else
            {
                field.Type = new CodeTypeReference(GetParameterType(model.DataType));
            }

            return field;
        }

        private CodeMemberField AddProperty(FieldModel model)
        {
            var parameter = new ParameterModel
            {
                Name = model.Name,
                DataType = model.DataType,
                Documentation = model.Documentation
            };

            return AddProperty(parameter);
        }

        private Type GetParameterType(string dataType)
        {
            switch (dataType)
            {
                case "CHAR":
                case "STRING":
                    return typeof(String);
                case "DATE":
                case "TIME":
                    return typeof(DateTime);
                case "BCD":
                    return typeof(Decimal);
                case "FLOAT":
                    return typeof(Double);
                case "INT1":
                case "INT2":
                case "INT4":
                case "NUM":
                    return typeof(Int32);
                default:
                    throw new SapException(string.Format("[{0}] type is not supported", dataType));
            }
        }
    }
}
