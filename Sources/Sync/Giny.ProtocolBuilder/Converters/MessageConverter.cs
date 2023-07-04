using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.AS3;
using Giny.AS3.Enums;
using Giny.AS3.Expressions;

namespace Giny.ProtocolBuilder.Converters
{
    public class MessageConverter : SerializableConverter
    {
        public override bool WriteDefaultFieldValues => false;

        public override string[] Imports => new string[]
        {
            "System.Collections.Generic",
            "Giny.Core.Network.Messages",
            "Giny.Protocol.Types",
            "Giny.Core.IO.Interfaces",
            "Giny.Protocol",
            "Giny.Protocol.Enums",
        };

        public override string BaseClassName => "NetworkMessage";

        public override string GetNamespace()
        {
            return "Giny.Protocol.Messages";
        }
        public MessageConverter(AS3File file) : base(file)
        {

        }

        public override void PostPrepare()
        {
            base.PostPrepare();

            if (GetClassName() == "RawDataMessage")
            {
                var method = GetMethodToWrite("Deserialize");

                method.Expressions.Clear();

                VariableDeclarationExpression l1 = new VariableDeclarationExpression(new AS3Variable("_contentLen", "int"),
                    new MethodCallExpression(File, "reader.ReadVarInt()", 0));

                AssignationExpression l2 = new AssignationExpression("content",
                    new MethodCallExpression(File, "reader.ReadBytes(_contentLen)", 0));

                method.Expressions.Add(l1);
                method.Expressions.Add(l2);
            }

            if (GetClassName() == "ObjectFeedMessage")
            {
                var method = GetMethodToWrite("Deserialize");

                var reference = method.Expressions.OfType<VariableDeclarationExpression>().FirstOrDefault(x => x.Variable.Name == "_mealLen");

                if (reference == null)
                {
                    throw new FormatException("Unable to apply custom rule to ObjectFeedMessage.");
                }

                var index = method.Expressions.IndexOf(reference) + 1;

                AssignationExpression expr = new AssignationExpression("meal", new UnchangedExpression("new ObjectItemQuantity[_mealLen]"));
                method.Expressions.Insert(4, expr);
            }
            if (GetClassName() == "CharacterCreationRequestMessage")
            {
                var method = GetMethodToWrite("Deserialize");
                AssignationExpression expr = new AssignationExpression("colors", new UnchangedExpression("new int[5]"));
                method.Expressions.Insert(0, expr);
            }
        }

        public string GetMessageProtocolId()
        {
            int protocolId = (int)File.GetField("protocolId").GetValue<ConstantExpression>().Value;
            StringBuilder sb = new StringBuilder();
            string modifier = File.Extends == BaseClassName ? string.Empty : "new";
            Append(string.Format("public {0} const ushort Id = {1};", modifier, protocolId), sb);
            Append("public override ushort MessageId => Id;", sb);
            return sb.ToString();
        }


    }
}
