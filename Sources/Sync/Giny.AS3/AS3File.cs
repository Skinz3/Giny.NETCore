using Giny.AS3.Enums;
using Giny.AS3.Expressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3
{
    public class AS3File
    {
        public const char BRACKET_START_DELIMITER = '{';

        public const char BRACKET_END_DELIMITER = '}';

        private string[] m_lines;

        public string Package
        {
            get;
            set;
        }
        public string ClassName
        {
            get;
            set;
        }
        public string FileName
        {
            get
            {
                return Path.GetFileName(FilePath);
            }
        }
        public string FilePath
        {
            get;
            private set;
        }


        public AS3AccessorsEnum ClassAccessor
        {
            get;
            set;
        }
        public AS3Method[] Methods
        {
            get;
            private set;
        }
        public List<AS3Field> Fields
        {
            get;
            private set;
        }
        public string[] Imports
        {
            get;
            private set;
        }
        public string[] Implementations
        {
            get;
            private set;
        }
        public string Extends
        {
            get;
            set;
        }
        private List<KeyValuePair<int, int>> Brackets
        {
            get;
            set;
        }

        private bool ParseMethods
        {
            get;
            set;
        }
        public AS3File(string filePath, bool parseMethods = true)
        {
            this.FilePath = filePath;
            this.m_lines = File.ReadAllLines(filePath);

            for (int i = 0; i < m_lines.Length; i++)
            {
                m_lines[i] = m_lines[i].Replace("this.", string.Empty);
            }
            this.Brackets = GetBrackets();
            this.ParseMethods = parseMethods;
            this.Open();
        }

        private void Open()
        {
            this.ClassName = GetMatch(@"(?<=\bclass\s)(\w+)", 0);
            // this.Package = GetMatch(@"(?<=package).*$", 0).Trim();
            this.Imports = InitImports();
            this.Extends = GetMatch(@"(?<=\bextends\s)(\w+)", 0).Trim();
            this.ClassAccessor = InitClassAccessor();
            this.Implementations = InitImplementations();
            this.Fields = InitFields();
            if (ParseMethods)
                this.Methods = InitMethods();
        }

        private AS3Method[] InitMethods()
        {
            List<AS3Method> methods = new List<AS3Method>();

            for (int i = 0; i < m_lines.Length; i++)
            {
                if (Regex.Match(m_lines[i], "(?:function)").Success && GetLineIndentLevel(i) == 2)
                {
                    methods.Add(new AS3Method(this, m_lines, i));
                }
            }

            return methods.ToArray();
        }

        public void RenameField(string oldName, string newName)
        {
            var field = Fields.FirstOrDefault(x => x.Name == oldName);

            if (field != null)
            {
                field.Variable.Name = newName;
            }

        }

        public IEnumerable<AS3Field> GetFields(Func<AS3Field, bool> predicate)
        {
            return Fields.Where(predicate);
        }
        public AS3Field GetField(string name)
        {
            return Fields.FirstOrDefault(x => x.Name == name);
        }
        public AS3Method GetMethod(string name)
        {
            return Methods.FirstOrDefault(x => x.Name == name);
        }
        public AS3Method GetMethod(Predicate<AS3Method> predicate)
        {
            return Array.Find(Methods, predicate);
        }
        public AS3Method[] GetMethods(Predicate<AS3Method> predicate)
        {
            return Array.FindAll(Methods, predicate);
        }
        private List<AS3Field> InitFields()
        {
            List<AS3Field> fields = new List<AS3Field>();

            for (int i = 0; i < m_lines.Length; i++)
            {
                if (Regex.Match(m_lines[i], @"(?: var | const )").Success && GetLineIndentLevel(i) == 2)
                {
                    fields.Add(new AS3Field(this, m_lines[i], i));
                }
            }

            return fields;
        }
        private AS3AccessorsEnum InitClassAccessor()
        {
            string match = GetMatch(@"\w+(?=\s+class)", 0);
            return match.ParseEnum<AS3AccessorsEnum>();
        }
        private string[] InitImplementations()
        {
            string implements = GetMatch(@"(?<=implements).*$", 0).Trim();
            return implements == string.Empty ? new string[0] : implements.Split(',');
        }
        private int GetFirstLineIndex(string line)
        {
            return Array.IndexOf(m_lines, line);
        }
        public string GetLine(int index)
        {
            return m_lines[index];
        }
        public int GetLineIndentLevel(int lineIndex)
        {
            for (int i = 0; i < Brackets.Count - 1; i++)
            {
                var bracket = Brackets.ElementAt(i);
                var nextBracket = Brackets.ElementAt(i + 1);

                if (lineIndex >= bracket.Key && lineIndex < nextBracket.Key)
                {
                    return bracket.Value;
                }
            }

            return 0;
        }
        private string[] InitImports()
        {
            string[] importLines = Array.FindAll(m_lines, x => x.Trim().StartsWith("import"));
            string[] result = new string[importLines.Length];

            for (int i = 0; i < importLines.Length; i++)
            {
                var match = Regex.Match(importLines[i], @"(?<=\ import )(.*?)(?=\;)");
                result[i] = match.Value;
            }
            return result;
        }
        private string GetMatch(string pattern, int index = 1)
        {
            var matchedLine = m_lines.ToList().Find(entry => Regex.IsMatch(entry, pattern, RegexOptions.None));

            if (matchedLine == null)
                return "";

            Match match = Regex.Match(matchedLine, pattern, RegexOptions.Multiline);
            string result = match.Groups[index].Value;
            return result = string.IsNullOrWhiteSpace(result) ? null : result;
        }
        private List<KeyValuePair<int, int>> GetBrackets()
        {
            int currentIdent = 0;

            var result = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < m_lines.Length; i++)
            {
                if (m_lines[i].Contains(BRACKET_START_DELIMITER))
                {
                    currentIdent++;
                    result.Add(new KeyValuePair<int, int>(i, currentIdent));
                }

                if (m_lines[i].Contains(BRACKET_END_DELIMITER))
                {
                    currentIdent--;
                    result.Add(new KeyValuePair<int, int>(i, currentIdent));
                }
            }

            return result;
        }
        private string GetLine(string contains)
        {
            return m_lines.FirstOrDefault(x => x.Contains(contains));
        }
        private string[] GetLines(string contains)
        {
            return Array.FindAll(m_lines, x => x.Contains(contains));
        }
        public string[] GetLines()
        {
            return m_lines;
        }

        public AS3Method CreateEmptyConstructor()
        {
            return new AS3Method(this.ClassName, new List<AS3Variable>(), new AS3Type(string.Empty), new List<BaseExpression>(), AS3AccessorsEnum.@public, AS3ModifiersEnum.None);
        }
        public AS3Method CreateConstructor(AS3AccessorsEnum accessor)
        {

            var fields = GetFields(x => x.Accessor == accessor && x.Modifiers == AS3ModifiersEnum.None).ToArray();

            AS3Variable[] parameters = new AS3Variable[fields.Length];
            BaseExpression[] expressions = new BaseExpression[fields.Length];

            for (int i = 0; i < fields.Count(); i++)
            {
                parameters[i] = fields[i].Variable;
                expressions[i] = new AssignationExpression("this." + fields[i].Name, new VariableNameExpression(fields[i].Name));
            }

            return new AS3Method(ClassName, parameters.ToList(), new AS3Type(string.Empty), expressions.ToList(), AS3AccessorsEnum.@public, AS3ModifiersEnum.None);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("ClassAccessor: " + ClassAccessor);

            stringBuilder.AppendLine("ClassName: " + ClassName);

            stringBuilder.AppendLine("Package: " + Package);

            stringBuilder.AppendLine("Extends: " + Extends);

            stringBuilder.AppendLine("Implementations: ");

            if (Implementations.Length > 0)
                stringBuilder.AppendLine("-" + string.Join(Environment.NewLine + "-", Implementations));

            stringBuilder.AppendLine("Imports: ");

            if (Imports.Length > 0)
                stringBuilder.AppendLine("-" + string.Join(Environment.NewLine + "-", Imports));

            stringBuilder.AppendLine("Methods: " + Methods.Length);
            stringBuilder.AppendLine("Fields: " + Fields.Count);
            return stringBuilder.ToString();

        }

    }
}
