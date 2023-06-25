using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class MapNpcQuestInfo  
    { 
        public const ushort Id = 8860;
        public virtual ushort TypeId => Id;

        public double mapId;
        public int[] npcsIdsWithQuest;
        public GameRolePlayNpcQuestFlag[] questFlags;

        public MapNpcQuestInfo()
        {
        }
        public MapNpcQuestInfo(double mapId,int[] npcsIdsWithQuest,GameRolePlayNpcQuestFlag[] questFlags)
        {
            this.mapId = mapId;
            this.npcsIdsWithQuest = npcsIdsWithQuest;
            this.questFlags = questFlags;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            writer.WriteShort((short)npcsIdsWithQuest.Length);
            for (uint _i2 = 0;_i2 < npcsIdsWithQuest.Length;_i2++)
            {
                writer.WriteInt((int)npcsIdsWithQuest[_i2]);
            }

            writer.WriteShort((short)questFlags.Length);
            for (uint _i3 = 0;_i3 < questFlags.Length;_i3++)
            {
                (questFlags[_i3] as GameRolePlayNpcQuestFlag).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            int _val2 = 0;
            GameRolePlayNpcQuestFlag _item3 = null;
            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of MapNpcQuestInfo.mapId.");
            }

            uint _npcsIdsWithQuestLen = (uint)reader.ReadUShort();
            npcsIdsWithQuest = new int[_npcsIdsWithQuestLen];
            for (uint _i2 = 0;_i2 < _npcsIdsWithQuestLen;_i2++)
            {
                _val2 = (int)reader.ReadInt();
                npcsIdsWithQuest[_i2] = (int)_val2;
            }

            uint _questFlagsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _questFlagsLen;_i3++)
            {
                _item3 = new GameRolePlayNpcQuestFlag();
                _item3.Deserialize(reader);
                questFlags[_i3] = _item3;
            }

        }


    }
}








