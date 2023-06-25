using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class NpcDialogQuestionMessage : NetworkMessage  
    { 
        public  const ushort Id = 2122;
        public override ushort MessageId => Id;

        public int messageId;
        public string[] dialogParams;
        public int[] visibleReplies;

        public NpcDialogQuestionMessage()
        {
        }
        public NpcDialogQuestionMessage(int messageId,string[] dialogParams,int[] visibleReplies)
        {
            this.messageId = messageId;
            this.dialogParams = dialogParams;
            this.visibleReplies = visibleReplies;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (messageId < 0)
            {
                throw new System.Exception("Forbidden value (" + messageId + ") on element messageId.");
            }

            writer.WriteVarInt((int)messageId);
            writer.WriteShort((short)dialogParams.Length);
            for (uint _i2 = 0;_i2 < dialogParams.Length;_i2++)
            {
                writer.WriteUTF((string)dialogParams[_i2]);
            }

            writer.WriteShort((short)visibleReplies.Length);
            for (uint _i3 = 0;_i3 < visibleReplies.Length;_i3++)
            {
                if (visibleReplies[_i3] < 0)
                {
                    throw new System.Exception("Forbidden value (" + visibleReplies[_i3] + ") on element 3 (starting at 1) of visibleReplies.");
                }

                writer.WriteVarInt((int)visibleReplies[_i3]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            string _val2 = null;
            uint _val3 = 0;
            messageId = (int)reader.ReadVarUhInt();
            if (messageId < 0)
            {
                throw new System.Exception("Forbidden value (" + messageId + ") on element of NpcDialogQuestionMessage.messageId.");
            }

            uint _dialogParamsLen = (uint)reader.ReadUShort();
            dialogParams = new string[_dialogParamsLen];
            for (uint _i2 = 0;_i2 < _dialogParamsLen;_i2++)
            {
                _val2 = (string)reader.ReadUTF();
                dialogParams[_i2] = (string)_val2;
            }

            uint _visibleRepliesLen = (uint)reader.ReadUShort();
            visibleReplies = new int[_visibleRepliesLen];
            for (uint _i3 = 0;_i3 < _visibleRepliesLen;_i3++)
            {
                _val3 = (uint)reader.ReadVarUhInt();
                if (_val3 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val3 + ") on elements of visibleReplies.");
                }

                visibleReplies[_i3] = (int)_val3;
            }

        }


    }
}








