using Giny.AS3;
using Giny.AS3.Enums;
using Giny.AS3.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.ProtocolBuilder.Converters
{
    public class TypeConverter : SerializableConverter
    {
        public override bool WriteDefaultFieldValues => false;

        public override string[] Imports => new string[]
        {
            "System.Collections.Generic",
            "Giny.Core.IO.Interfaces",
            "Giny.Protocol",
            "Giny.Protocol.Enums",
        };

        public override string BaseClassName => "";

        public override string GetNamespace()
        {
            return "Giny.Protocol.Types";
        }
        public TypeConverter(AS3File file) : base(file)
        {

        }
        public override string GetExtends()
        {
            return base.GetExtends();
        }
        public override void Prepare(Dictionary<string, AS3File> context)
        {
            if (GetClassName() == "GameRolePlayGroupMonsterInformations")
            {
                var variable = new AS3Variable("staticInfos", "GroupMonsterStaticInformations");

                AS3Field field = new AS3Field(variable,
                    AS3AccessorsEnum.@public, AS3ModifiersEnum.None, new EmptyExpression());

                FieldsToWrite.Add(field);
                File.Fields.Add(field);// append field

                var ctor = MethodsToWrite.FirstOrDefault(x => x.IsConstructor && x.Parameters.Count > 0);

                ctor.Parameters.Add(variable);
            }


            base.Prepare(context);

        }
        public override void PostPrepare()
        {
            base.PostPrepare();

            if (GetExtends() == BaseClassName)
            {
                GetMethodToWrite("Serialize").SetModifiers(AS3ModifiersEnum.@virtual);
                GetMethodToWrite("Deserialize").SetModifiers(AS3ModifiersEnum.@virtual);
            }
        }

        public string GetTypeProtocolId()
        {
            int protocolId = (int)File.GetField("protocolId").GetValue<ConstantExpression>().Value;
            StringBuilder sb = new StringBuilder();
            string modifier = this.GetExtends() == string.Empty ? "virtual" : "override";

            if (GetExtends() == string.Empty)
            {
                sb.AppendLine(string.Format("public const ushort Id = {0};", protocolId));
            }
            else
            {
                sb.AppendLine(string.Format("public new const ushort Id = {0};", protocolId));
            }

            sb.AppendLine("public " + modifier + " ushort TypeId => Id;");
            return sb.ToString();
        }
    }
}
