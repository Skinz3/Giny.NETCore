using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;
using Version = Giny.Protocol.Types.Version;

namespace Giny.Protocol.Messages
{ 
    public class IdentificationAccountForceMessage : IdentificationMessage  
    { 
        public new const ushort Id = 247;
        public override ushort MessageId => Id;

        public string forcerAccountLogin;

        public IdentificationAccountForceMessage()
        {
        }
        public IdentificationAccountForceMessage(string forcerAccountLogin,Version version,string lang,byte[] credentials,short serverId,bool autoconnect,bool useCertificate,bool useLoginToken,long sessionOptionalSalt,short[] failedAttempts)
        {
            this.forcerAccountLogin = forcerAccountLogin;
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
            base.Serialize(writer);
            writer.WriteUTF((string)forcerAccountLogin);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            forcerAccountLogin = (string)reader.ReadUTF();
        }


    }
}








