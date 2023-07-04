using Giny.AS3;
using Giny.AS3.Enums;
using Giny.AS3.Expressions;
using Giny.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.ProtocolBuilder.Converters
{
    public class DatacenterConverter : DofusConverter
    {
        public override bool WriteDefaultFieldValues => false;

        public DatacenterConverter(AS3File file) : base(file)
        {

        }
        public override string GetNamespace()
        {
            return "Giny.IO.D2OClasses";
        }
        public override string[] Imports => new string[]
        {
            "System",
            "Giny.Core.IO.Interfaces",
            "Giny.IO.D2O",
            "Giny.IO.D2OTypes",
            "System.Collections.Generic",
        };
        public override string GetExtends()
        {
            return File.Extends == string.Empty ? "IDataObject" : File.Extends;
        }
        public override string GetImplements()
        {
            return "IIndexedData";
        }
        protected override List<AS3Method> SelectMethodsToWrite()
        {
            return new List<AS3Method>();
        }
        public string GetD2OModule()
        {
            var field = File.GetField("MODULE");

            if (field == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();

            Append(string.Format("public const string MODULE = {0};", field.GetValue<ConstantStringExpression>().Value), sb);

            return sb.ToString();
        }
        public string GetD2OAttribute()
        {
            var sb = new StringBuilder();
            Append("[D2OClass(\"" + File.ClassName + "\", \"" + File.Package + "\")]", sb);
            return sb.ToString();
        }
        public string GetD2OId()
        {
            StringBuilder sb = new StringBuilder();

            var field = File.GetFields(x => x.Name == "_id" || x.Name == "id").FirstOrDefault();

            if (field != null && (field.RawType == "int" || field.RawType == "uint"))
            {
                Append("public int Id => (int)" + VerifyVariableName(field.Name) + ";", sb);

            }
            else
            {
                Append("public int Id => throw new NotImplementedException();", sb);

            }
            return sb.ToString();
        }
        public override string GetConvertedType(AS3Type type)
        {
            if (type.RawType.Contains("Vector"))
            {
                return "List<" + GetConvertedType(new AS3Type(type.GetGenericType())) + ">";
            }
            return base.GetConvertedType(type);
        }
        public string GetD2OClassProperties()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var field in FieldsToWrite)
            {
                if (field.Name == "APRemoval")
                {

                }
                string variableName = VerifyVariableName(field.Name);

                if (variableName.StartsWith("_"))
                {
                    variableName = variableName.Remove(0, 1);
                }

                if (variableName == "id")
                {
                    variableName += "_";
                }

                if (variableName.First() == '@')
                {
                    variableName = variableName.Remove(0, 1);
                }


                variableName = variableName.FirstCharToUpper();

                if (variableName == File.ClassName)
                {
                    variableName += "_";
                }

                if (variableName == field.Name)
                {
                    variableName = "_" + variableName;
                }

                Append("[D2OIgnore]", sb);
                Append("public " + GetConvertedType(field.Variable.Type) + " " + variableName, sb);
                Append("{", sb);
                PushIndent();
                Append("get", sb);
                Append("{", sb);
                PushIndent();
                Append("return " + VerifyVariableName(field.Variable.Name) + ";", sb);
                PopIndent();
                Append("}", sb);
                Append("set", sb);
                Append("{", sb);
                PushIndent();
                Append(VerifyVariableName(field.Variable.Name) + " = value;", sb);
                PopIndent();
                Append("}", sb);
                PopIndent();
                Append("}", sb);
            }
            return sb.ToString();
        }
        protected override List<AS3Field> SelectFieldsToWrite()
        {
            return File.GetFields(x => (AS3AccessorsEnum.@public).HasFlag(x.Accessor) && x.Modifiers == AS3ModifiersEnum.None).ToList();
        }

        public override void PostPrepare()
        {
            if (GetClassName() == "EffectInstance")
            {
                AddPrivateField("_rawZone");
            }

            if (GetClassName() == "CensoredContent")
            {
                AddPrivateField("_type");
                AddPrivateField("_oldValue");
                AddPrivateField("_newValue");
                AddPrivateField("_lang");
            }

            if (GetClassName() == "Hint")
            {
                AddPrivateField("_categoryId");
            }

        }

        private void AddPrivateField(string name)
        {
            var field = File.GetField(name);
            field.Variable.Name = field.Name.Replace("_", "");
            field.Accessor = AS3AccessorsEnum.@public;
            FieldsToWrite.Add(field);
        }
    }
}
