using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ServerSettingsMessage : NetworkMessage  
    { 
        public  const ushort Id = 1715;
        public override ushort MessageId => Id;

        public string lang;
        public byte community;
        public byte gameType;
        public bool isMonoAccount;
        public short arenaLeaveBanTime;
        public int itemMaxLevel;
        public bool hasFreeAutopilot;

        public ServerSettingsMessage()
        {
        }
        public ServerSettingsMessage(string lang,byte community,byte gameType,bool isMonoAccount,short arenaLeaveBanTime,int itemMaxLevel,bool hasFreeAutopilot)
        {
            this.lang = lang;
            this.community = community;
            this.gameType = gameType;
            this.isMonoAccount = isMonoAccount;
            this.arenaLeaveBanTime = arenaLeaveBanTime;
            this.itemMaxLevel = itemMaxLevel;
            this.hasFreeAutopilot = hasFreeAutopilot;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,isMonoAccount);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,hasFreeAutopilot);
            writer.WriteByte((byte)_box0);
            writer.WriteUTF((string)lang);
            if (community < 0)
            {
                throw new System.Exception("Forbidden value (" + community + ") on element community.");
            }

            writer.WriteByte((byte)community);
            writer.WriteByte((byte)gameType);
            if (arenaLeaveBanTime < 0)
            {
                throw new System.Exception("Forbidden value (" + arenaLeaveBanTime + ") on element arenaLeaveBanTime.");
            }

            writer.WriteVarShort((short)arenaLeaveBanTime);
            if (itemMaxLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + itemMaxLevel + ") on element itemMaxLevel.");
            }

            writer.WriteInt((int)itemMaxLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            isMonoAccount = BooleanByteWrapper.GetFlag(_box0,0);
            hasFreeAutopilot = BooleanByteWrapper.GetFlag(_box0,1);
            lang = (string)reader.ReadUTF();
            community = (byte)reader.ReadByte();
            if (community < 0)
            {
                throw new System.Exception("Forbidden value (" + community + ") on element of ServerSettingsMessage.community.");
            }

            gameType = (byte)reader.ReadByte();
            arenaLeaveBanTime = (short)reader.ReadVarUhShort();
            if (arenaLeaveBanTime < 0)
            {
                throw new System.Exception("Forbidden value (" + arenaLeaveBanTime + ") on element of ServerSettingsMessage.arenaLeaveBanTime.");
            }

            itemMaxLevel = (int)reader.ReadInt();
            if (itemMaxLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + itemMaxLevel + ") on element of ServerSettingsMessage.itemMaxLevel.");
            }

        }


    }
}








