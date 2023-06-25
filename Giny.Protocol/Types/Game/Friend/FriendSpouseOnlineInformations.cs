using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FriendSpouseOnlineInformations : FriendSpouseInformations  
    { 
        public new const ushort Id = 3147;
        public override ushort TypeId => Id;

        public double mapId;
        public short subAreaId;
        public bool inFight;
        public bool followSpouse;

        public FriendSpouseOnlineInformations()
        {
        }
        public FriendSpouseOnlineInformations(double mapId,short subAreaId,bool inFight,bool followSpouse,int spouseAccountId,long spouseId,string spouseName,short spouseLevel,byte breed,byte sex,EntityLook spouseEntityLook,GuildInformations guildInfo,byte alignmentSide)
        {
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.inFight = inFight;
            this.followSpouse = followSpouse;
            this.spouseAccountId = spouseAccountId;
            this.spouseId = spouseId;
            this.spouseName = spouseName;
            this.spouseLevel = spouseLevel;
            this.breed = breed;
            this.sex = sex;
            this.spouseEntityLook = spouseEntityLook;
            this.guildInfo = guildInfo;
            this.alignmentSide = alignmentSide;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,inFight);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,followSpouse);
            writer.WriteByte((byte)_box0);
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            byte _box0 = reader.ReadByte();
            inFight = BooleanByteWrapper.GetFlag(_box0,0);
            followSpouse = BooleanByteWrapper.GetFlag(_box0,1);
            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of FriendSpouseOnlineInformations.mapId.");
            }

            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of FriendSpouseOnlineInformations.subAreaId.");
            }

        }


    }
}








