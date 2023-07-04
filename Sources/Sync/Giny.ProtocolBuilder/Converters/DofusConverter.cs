using Giny.AS3;
using Giny.AS3.Converter;
using Giny.AS3.Enums;
using Giny.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.ProtocolBuilder.Converters
{
    public abstract class DofusConverter : DefaultConverter
    {
        public abstract string[] Imports
        {
            get;
        }
        public DofusConverter(AS3File file) : base(file)
        {

        }

      
        public override string GetConvertedType(AS3Type type)
        {
            if (type.RawType == "ByteArray")
            {
                return "byte[]";
            }
            if (type.RawType.Contains("Vector"))
            {
                return GetConvertedType(new AS3Type(type.GetGenericType())) + "[]";
            }
            return base.GetConvertedType(type);
        }
        public override string GetImports()
        {
            List<string> results = new List<string>();

            foreach (var import in Imports)
            {
                results.Add("using " + import + ";");
            }
            return string.Join(Environment.NewLine, results);
        }

        protected override List<AS3Field> SelectFieldsToWrite()
        {
            return File.GetFields(x => x.Accessor == AS3AccessorsEnum.@public && x.Modifiers == AS3ModifiersEnum.None).ToList();
        }
        protected override List<AS3Method> SelectMethodsToWrite()
        {
            return File.GetMethods(x => x.Accessor == AS3AccessorsEnum.@public).ToList();
        }

        public abstract void PostPrepare();
    }
}
