using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TitlesAndOrnamentsListMessage : NetworkMessage  
    { 
        public  const ushort Id = 3990;
        public override ushort MessageId => Id;

        public short[] titles;
        public short[] ornaments;
        public short activeTitle;
        public short activeOrnament;

        public TitlesAndOrnamentsListMessage()
        {
        }
        public TitlesAndOrnamentsListMessage(short[] titles,short[] ornaments,short activeTitle,short activeOrnament)
        {
            this.titles = titles;
            this.ornaments = ornaments;
            this.activeTitle = activeTitle;
            this.activeOrnament = activeOrnament;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)titles.Length);
            for (uint _i1 = 0;_i1 < titles.Length;_i1++)
            {
                if (titles[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + titles[_i1] + ") on element 1 (starting at 1) of titles.");
                }

                writer.WriteVarShort((short)titles[_i1]);
            }

            writer.WriteShort((short)ornaments.Length);
            for (uint _i2 = 0;_i2 < ornaments.Length;_i2++)
            {
                if (ornaments[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + ornaments[_i2] + ") on element 2 (starting at 1) of ornaments.");
                }

                writer.WriteVarShort((short)ornaments[_i2]);
            }

            if (activeTitle < 0)
            {
                throw new System.Exception("Forbidden value (" + activeTitle + ") on element activeTitle.");
            }

            writer.WriteVarShort((short)activeTitle);
            if (activeOrnament < 0)
            {
                throw new System.Exception("Forbidden value (" + activeOrnament + ") on element activeOrnament.");
            }

            writer.WriteVarShort((short)activeOrnament);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _val2 = 0;
            uint _titlesLen = (uint)reader.ReadUShort();
            titles = new short[_titlesLen];
            for (uint _i1 = 0;_i1 < _titlesLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of titles.");
                }

                titles[_i1] = (short)_val1;
            }

            uint _ornamentsLen = (uint)reader.ReadUShort();
            ornaments = new short[_ornamentsLen];
            for (uint _i2 = 0;_i2 < _ornamentsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of ornaments.");
                }

                ornaments[_i2] = (short)_val2;
            }

            activeTitle = (short)reader.ReadVarUhShort();
            if (activeTitle < 0)
            {
                throw new System.Exception("Forbidden value (" + activeTitle + ") on element of TitlesAndOrnamentsListMessage.activeTitle.");
            }

            activeOrnament = (short)reader.ReadVarUhShort();
            if (activeOrnament < 0)
            {
                throw new System.Exception("Forbidden value (" + activeOrnament + ") on element of TitlesAndOrnamentsListMessage.activeOrnament.");
            }

        }


    }
}








