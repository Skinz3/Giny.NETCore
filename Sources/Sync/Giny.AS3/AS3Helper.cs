using Giny.AS3.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3
{
    public class AS3Helper
    {
        public static List<AS3Variable> GetParameters(AS3File file, string[] lines, int i)
        {
            string parametersString = Regex.Match(lines[i], @"[(](.*)[)]").Groups[1].Value;

            if (parametersString == string.Empty)
            {
                return new List<AS3Variable>();
            }
            string[] parameters = parametersString.Split(',');
            AS3Variable[] results = new AS3Variable[parameters.Length];

            for (int w = 0; w < parameters.Length; w++)
            {
                var parameter = parameters[w].Split('=').First().Trim(); // we dont care about default value. If we dont --> AS3MethodParameter(AS3Variable variable,BaseExpression value)

                string name = Regex.Match(parameter, @"\w+(?=.?:)").Value;
                string type = Regex.Match(parameter, @"(?<=:).*$").Value;

                results[w] = new AS3Variable(name, type);
            }

            return results.ToList();
        }
        public static BlockDefinition GetBlockDefinitionForMethods(AS3File file, int i)
        {
            var lines = file.GetLines();

            int linesCount = 1;
            for (int w = i + 1; w < lines.Length; w++)
            {
                var indent = file.GetLineIndentLevel(w);
                var line = lines[w];

                linesCount++;
                if (indent == 2)
                {
                    break;
                }
            }

            int contentLineCount = linesCount - 3; // signature + '{' + '}' 

            string[] methodContent = new string[contentLineCount];

            int startIndex = i + 2;
            int endIndex = startIndex + (linesCount - 3);
            return new BlockDefinition(startIndex, endIndex);
        }
        public static BlockDefinition GetBlockDefinitionForInstructions(AS3File file, int i, int startIndent)
        {
            var lines = file.GetLines();

            int linesCount = 1;
            for (int w = i + 1; w < lines.Length; w++)
            {
                var indent = file.GetLineIndentLevel(w);
                var line = lines[w];

                linesCount++;
                if (indent < startIndent)
                {
                    break;
                }
            }

            int contentLineCount = linesCount;

            string[] methodContent = new string[contentLineCount];

            int startIndex = i;
            int endIndex = startIndex + (linesCount);
            return new BlockDefinition(startIndex, endIndex);
        }
        public static List<BaseExpression> BuildExpressions(AS3File file, BlockDefinition blockDefinition)
        {
            var content = new List<BaseExpression>();
            int count = 1;
            for (int i = blockDefinition.StartIndex; i < blockDefinition.EndIndex; i += count)
            {
                count = 1;
                var line = file.GetLine(i);
                if (line != string.Empty && !Regex.Match(line, @"(?:{|})").Success)
                {
                    var expression = ExpressionManager.Construct(file, line, i);
                    count = expression.LineSkip;
                    content.Add(expression);
                }
            }
            return content;
        }
    }
    public struct BlockDefinition
    {
        public int StartIndex;

        public int EndIndex;

        public int LinesCount
        {
            get
            {
                return EndIndex - StartIndex;
            }
        }
        public BlockDefinition(int startIndex, int endIndex)
        {
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
        }
    }
}
