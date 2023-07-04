using Giny.AS3.Enums;
using Giny.AS3.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.AS3
{
    public class AS3Variable
    {
        public string Name;

        public AS3Type Type;

        public AS3Variable(string name, string rawType)
        {
            this.Name = name;
            this.Type = new AS3Type(rawType);
        }
        public override string ToString()
        {
            return Type.RawType + " " + Name;
        }
    }
    public class AS3Type
    {
        public string RawType;

        public AS3TypeEnum AS3TypeEnum;

        public string GetGenericType()
        {
            return Regex.Match(RawType, @"(?<=\.).*$").Value.RemoveChars('<', '>');
        }
        public AS3Type(string rawType)
        {
            this.RawType = rawType;
            this.AS3TypeEnum = RawType.ParseEnum<AS3TypeEnum>();
        }
    }

    public class AS3Field
    {
        public AS3Variable Variable
        {
            get;
            private set;
        }
        public string Name
        {
            get
            {
                return Variable.Name;
            }
        }
        public string RawType
        {
            get
            {
                return Variable.Type.RawType;
            }
        }
        public AS3AccessorsEnum Accessor
        {
            get;
            set;
        }
        public AS3ModifiersEnum Modifiers
        {
            get;
            set;
        }
        public BaseExpression Value
        {
            get;
            private set;
        }
        public AS3Field(AS3Variable variable, AS3AccessorsEnum accessors, AS3ModifiersEnum modifiers, BaseExpression value)
        {
            this.Variable = variable;
            this.Accessor = accessors;
            this.Modifiers = modifiers;
            this.Value = value;
        }
        public AS3Field(AS3File file, string line, int i)
        {
            string rawType = (line.Contains("=") ? rawType = Regex.Match(line, @"[:](.*)[=]").Groups[1].Value : Regex.Match(line, @"[:](.*)[;]").Groups[1].Value).Trim();
            Variable = new AS3Variable(Regex.Match(line, @"\w+(?=.?:)").Value, rawType);
            Accessor = Regex.Match(line, @"(?:public|private|protected)").Value.ParseEnum<AS3AccessorsEnum>();
            Modifiers = Regex.Match(line, @"(?:const|static)").Value.ParseEnum<AS3ModifiersEnum>();
            string value = Regex.Match(line, @"(?<==).*$").Value;
            Value = ExpressionManager.Construct(file, value, i);
        }
        public void ChangeType(string rawType)
        {
            this.Variable.Type = new AS3Type(rawType);
        }
        public T GetValue<T>() where T : BaseExpression
        {
            return Value as T;
        }
        public override string ToString()
        {
            return Name + ":" + RawType;
        }
    }
}
