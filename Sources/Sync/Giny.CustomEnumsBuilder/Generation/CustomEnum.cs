using Giny.IO.D2I;
using Giny.IO.D2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.EnumsBuilder.Generation
{
    public abstract class CustomEnum
    {
        private static Dictionary<string, string> ConversionRules = new Dictionary<string, string>()
        {
            { "[",string.Empty },
            { "]",string.Empty },
            { "'",string.Empty },
            { "(",string.Empty },
            { ")",string.Empty },
            { ":",string.Empty },
            { "-","_" },
            { " ", "_" },
            { "!",string.Empty },
            { ".","_" },
            { "/",string.Empty },
            {"\"",string.Empty },
            { "?",string.Empty },
            { "²",string.Empty },
            { "$",string.Empty },
            { "*",string.Empty },
            { "%",string.Empty },
            { "+",string.Empty },
            { ",",string.Empty },
            { "&",string.Empty },
            { "|",string.Empty },
            { "–","_" },
        };

        public abstract string ClassName { get; }

        protected abstract string GenerateEnumContent(List<D2OReader> readers, D2IFile d2i);

        public string Generate(List<D2OReader> readers, D2IFile d2i)
        {
            string content = GenerateEnumContent(readers, d2i);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("namespace Giny.Protocol.Custom.Enums");
            sb.AppendLine("{");
            sb.AppendLine("    public enum " + ClassName);
            sb.AppendLine("    {");
            sb.AppendLine(content);
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        protected virtual string ApplyRules(string line)
        {
            if (line == "Natasha Manka's Workshop")
            {

            }

            string value = "";

            bool prevIsLower = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (char.IsUpper(c))
                {
                    if (i > 0 && prevIsLower)
                    {
                        value += "_";
                    }
                    value += char.ToUpper(c);
                    prevIsLower = false;
                }
                else
                {
                    value += char.ToUpper(c);
                    prevIsLower = true;
                }

            }

            foreach (var rule in ConversionRules)
            {
                value = value.Replace(rule.Key, rule.Value);
            }


            string tmp = "";

            if (value[0] != '_')
            {
                tmp += value[0];
            }

            for (int i = 1; i < value.Length; i++)
            {
                if (!(value[i] == '_' && value[i - 1] == '_'))
                {
                    tmp += value[i];
                }
            }
            return "        " + tmp;
        }
    }
}


