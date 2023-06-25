using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;
using Version = Giny.Protocol.Types.Version;

namespace Giny.Protocol.Messages
{ 
    public class IdentificationMessage : NetworkMessage  
    { 
        public  const ushort Id = 7907;
        public override ushort MessageId => Id;

        public Version version;
        public string lang;
        public byte[] credentials;
        public short serverId;
        public bool autoconnect;
        public bool useCertificate;
        public bool useLoginToken;
        public long sessionOptionalSalt;
        public short[] failedAttempts;

        public IdentificationMessage()
        {
        }
        public IdentificationMessage(Version version,string lang,byte[] credentials,short serverId,bool autoconnect,bool useCertificate,bool useLoginToken,long sessionOptionalSalt,short[] failedAttempts)
        {
            this.version = version;
            this.lang = lang;
            this.credentials = credentials;
            this.serverId = serverId;
            this.autoconnect = autoconnect;
            this.useCertificate = useCertificate;
            this.useLoginToken = useLoginToken;
            this.sessionOptionalSalt = sessionOptionalSalt;
            this.failedAttempts = failedAttempts;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,autoconnect);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,useCertificate);
            _box0 = BooleanByteWrapper.SetFlag(_box0,2,useLoginToken);
            writer.WriteByte((byte)_box0);
            version.Serialize(writer);
            writer.WriteUTF((string)lang);
            writer.WriteVarInt((int)credentials.Length);
            for (uint _i3 = 0;_i3 < credentials.Length;_i3++)
            {
                writer.WriteByte((byte)credentials[_i3]);
            }

            writer.WriteShort((short)serverId);
            if (sessionOptionalSalt < -9.00719925474099E+15 || sessionOptionalSalt > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sessionOptionalSalt + ") on element sessionOptionalSalt.");
            }

            writer.WriteVarLong((long)sessionOptionalSalt);
            writer.WriteShort((short)failedAttempts.Length);
            for (uint _i9 = 0;_i9 < failedAttempts.Length;_i9++)
            {
                if (failedAttempts[_i9] < 0)
                {
                    throw new System.Exception("Forbidden value (" + failedAttempts[_i9] + ") on element 9 (starting at 1) of failedAttempts.");
                }

                writer.WriteVarShort((short)failedAttempts[_i9]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _val3 = 0;
            uint _val9 = 0;
            byte _box0 = reader.ReadByte();
            autoconnect = BooleanByteWrapper.GetFlag(_box0,0);
            useCertificate = BooleanByteWrapper.GetFlag(_box0,1);
            useLoginToken = BooleanByteWrapper.GetFlag(_box0,2);
            version = new Version();
            version.Deserialize(reader);
            lang = (string)reader.ReadUTF();
            uint _credentialsLen = (uint)reader.ReadVarInt();
            credentials = new byte[_credentialsLen];
            for (uint _i3 = 0;_i3 < _credentialsLen;_i3++)
            {
                _val3 = (int)reader.ReadByte();
                credentials[_i3] = (byte)_val3;
            }

            serverId = (short)reader.ReadShort();
            sessionOptionalSalt = (long)reader.ReadVarLong();
            if (sessionOptionalSalt < -9.00719925474099E+15 || sessionOptionalSalt > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sessionOptionalSalt + ") on element of IdentificationMessage.sessionOptionalSalt.");
            }

            uint _failedAttemptsLen = (uint)reader.ReadUShort();
            failedAttempts = new short[_failedAttemptsLen];
            for (uint _i9 = 0;_i9 < _failedAttemptsLen;_i9++)
            {
                _val9 = (uint)reader.ReadVarUhShort();
                if (_val9 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val9 + ") on elements of failedAttempts.");
                }

                failedAttempts[_i9] = (short)_val9;
            }

        }


    }
}








