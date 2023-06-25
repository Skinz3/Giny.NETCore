using Giny.Core.IO;
using Giny.Zaap.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Zaap.Protocol
{
    public class SettingsGet : ZaapMessage
    {
        public enum TFieldId
        {
            GAMESESSION = 1,
            KEY = 2,
        }

        public string GameSession
        {
            get;
            set;
        }
        public string Key
        {
            get;
            private set;
        }
        public SettingsGet() 
        {
        }

        public override void Deserialize(TProtocol protocol, BigEndianReader reader)
        {
            while (true)
            {
                var field = protocol.ReadFieldBegin(reader);

                if (field.Type == TType.STOP)
                {
                    break;
                }
                switch ((TFieldId)field.Id)
                {
                    case TFieldId.GAMESESSION:
                        this.GameSession = reader.ReadUTF7BitLength();
                        break;
                    case TFieldId.KEY:
                        this.Key = reader.ReadUTF7BitLength();
                        break;
                    default:
                        break;
                }
            }

        }

        public override void Serialize(TProtocol protocol, BigEndianWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
