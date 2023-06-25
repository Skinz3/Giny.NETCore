using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IdolListMessage : NetworkMessage  
    { 
        public  const ushort Id = 5686;
        public override ushort MessageId => Id;

        public short[] chosenIdols;
        public short[] partyChosenIdols;
        public PartyIdol[] partyIdols;

        public IdolListMessage()
        {
        }
        public IdolListMessage(short[] chosenIdols,short[] partyChosenIdols,PartyIdol[] partyIdols)
        {
            this.chosenIdols = chosenIdols;
            this.partyChosenIdols = partyChosenIdols;
            this.partyIdols = partyIdols;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)chosenIdols.Length);
            for (uint _i1 = 0;_i1 < chosenIdols.Length;_i1++)
            {
                if (chosenIdols[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + chosenIdols[_i1] + ") on element 1 (starting at 1) of chosenIdols.");
                }

                writer.WriteVarShort((short)chosenIdols[_i1]);
            }

            writer.WriteShort((short)partyChosenIdols.Length);
            for (uint _i2 = 0;_i2 < partyChosenIdols.Length;_i2++)
            {
                if (partyChosenIdols[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + partyChosenIdols[_i2] + ") on element 2 (starting at 1) of partyChosenIdols.");
                }

                writer.WriteVarShort((short)partyChosenIdols[_i2]);
            }

            writer.WriteShort((short)partyIdols.Length);
            for (uint _i3 = 0;_i3 < partyIdols.Length;_i3++)
            {
                writer.WriteShort((short)(partyIdols[_i3] as PartyIdol).TypeId);
                (partyIdols[_i3] as PartyIdol).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _val2 = 0;
            uint _id3 = 0;
            PartyIdol _item3 = null;
            uint _chosenIdolsLen = (uint)reader.ReadUShort();
            chosenIdols = new short[_chosenIdolsLen];
            for (uint _i1 = 0;_i1 < _chosenIdolsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of chosenIdols.");
                }

                chosenIdols[_i1] = (short)_val1;
            }

            uint _partyChosenIdolsLen = (uint)reader.ReadUShort();
            partyChosenIdols = new short[_partyChosenIdolsLen];
            for (uint _i2 = 0;_i2 < _partyChosenIdolsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of partyChosenIdols.");
                }

                partyChosenIdols[_i2] = (short)_val2;
            }

            uint _partyIdolsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _partyIdolsLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<PartyIdol>((short)_id3);
                _item3.Deserialize(reader);
                partyIdols[_i3] = _item3;
            }

        }


    }
}








