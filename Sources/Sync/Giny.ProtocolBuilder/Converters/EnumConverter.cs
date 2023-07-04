using Giny.AS3;
using Giny.AS3.Converter;
using Giny.AS3.Enums;
using Giny.AS3.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.ProtocolBuilder.Converters
{
    public class EnumConverter : DofusConverter
    {
        public override bool WriteDefaultFieldValues => false;

        public EnumConverter(AS3File file) : base(file)
        {

        }
        public override string GetNamespace()
        {
            return "Giny.Protocol.Enums";
        }
        public override string[] Imports => new string[]
        {
            "System"
        };
        protected override List<AS3Field> SelectFieldsToWrite()
        {
            return File.GetFields(x => x.Accessor == AS3AccessorsEnum.@public && x.Modifiers == AS3ModifiersEnum.@static).ToList();
        }
        public string GetEnumFields()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var field in FieldsToWrite)
            {
                Append(string.Format("{0} = {1},", field.Name, field.GetValue<ConstantExpression>().Value), sb);
            }

            return sb.ToString();
        }

        public override void PostPrepare()
        {
          
        }
    }
}