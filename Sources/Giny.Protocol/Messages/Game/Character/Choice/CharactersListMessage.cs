using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class CharactersListMessage : NetworkMessage
    {
        public const ushort Id = 7446;
        public override ushort MessageId => Id;

        public CharacterBaseInformations[] characters;

        public CharactersListMessage()
        {
        }
        public CharactersListMessage(CharacterBaseInformations[] characters)
        {
            this.characters = characters;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)characters.Length);
            for (uint _i1 = 0; _i1 < characters.Length; _i1++)
            {
                writer.WriteShort((short)(characters[_i1] as CharacterBaseInformations).TypeId);
                (characters[_i1] as CharacterBaseInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            CharacterBaseInformations _item1 = null;
            uint _charactersLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _charactersLen; _i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<CharacterBaseInformations>((short)_id1);
                _item1.Deserialize(reader);
                characters[_i1] = _item1;
            }

        }

    }
}


