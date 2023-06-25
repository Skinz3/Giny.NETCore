using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightSpectateMessage : NetworkMessage  
    { 
        public  const ushort Id = 6763;
        public override ushort MessageId => Id;

        public FightDispellableEffectExtendedInformations[] effects;
        public GameActionMark[] marks;
        public short gameTurn;
        public int fightStart;
        public Idol[] idols;
        public GameFightEffectTriggerCount[] fxTriggerCounts;

        public GameFightSpectateMessage()
        {
        }
        public GameFightSpectateMessage(FightDispellableEffectExtendedInformations[] effects,GameActionMark[] marks,short gameTurn,int fightStart,Idol[] idols,GameFightEffectTriggerCount[] fxTriggerCounts)
        {
            this.effects = effects;
            this.marks = marks;
            this.gameTurn = gameTurn;
            this.fightStart = fightStart;
            this.idols = idols;
            this.fxTriggerCounts = fxTriggerCounts;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)effects.Length);
            for (uint _i1 = 0;_i1 < effects.Length;_i1++)
            {
                (effects[_i1] as FightDispellableEffectExtendedInformations).Serialize(writer);
            }

            writer.WriteShort((short)marks.Length);
            for (uint _i2 = 0;_i2 < marks.Length;_i2++)
            {
                (marks[_i2] as GameActionMark).Serialize(writer);
            }

            if (gameTurn < 0)
            {
                throw new System.Exception("Forbidden value (" + gameTurn + ") on element gameTurn.");
            }

            writer.WriteVarShort((short)gameTurn);
            if (fightStart < 0)
            {
                throw new System.Exception("Forbidden value (" + fightStart + ") on element fightStart.");
            }

            writer.WriteInt((int)fightStart);
            writer.WriteShort((short)idols.Length);
            for (uint _i5 = 0;_i5 < idols.Length;_i5++)
            {
                (idols[_i5] as Idol).Serialize(writer);
            }

            writer.WriteShort((short)fxTriggerCounts.Length);
            for (uint _i6 = 0;_i6 < fxTriggerCounts.Length;_i6++)
            {
                (fxTriggerCounts[_i6] as GameFightEffectTriggerCount).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            FightDispellableEffectExtendedInformations _item1 = null;
            GameActionMark _item2 = null;
            Idol _item5 = null;
            GameFightEffectTriggerCount _item6 = null;
            uint _effectsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _effectsLen;_i1++)
            {
                _item1 = new FightDispellableEffectExtendedInformations();
                _item1.Deserialize(reader);
                effects[_i1] = _item1;
            }

            uint _marksLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _marksLen;_i2++)
            {
                _item2 = new GameActionMark();
                _item2.Deserialize(reader);
                marks[_i2] = _item2;
            }

            gameTurn = (short)reader.ReadVarUhShort();
            if (gameTurn < 0)
            {
                throw new System.Exception("Forbidden value (" + gameTurn + ") on element of GameFightSpectateMessage.gameTurn.");
            }

            fightStart = (int)reader.ReadInt();
            if (fightStart < 0)
            {
                throw new System.Exception("Forbidden value (" + fightStart + ") on element of GameFightSpectateMessage.fightStart.");
            }

            uint _idolsLen = (uint)reader.ReadUShort();
            for (uint _i5 = 0;_i5 < _idolsLen;_i5++)
            {
                _item5 = new Idol();
                _item5.Deserialize(reader);
                idols[_i5] = _item5;
            }

            uint _fxTriggerCountsLen = (uint)reader.ReadUShort();
            for (uint _i6 = 0;_i6 < _fxTriggerCountsLen;_i6++)
            {
                _item6 = new GameFightEffectTriggerCount();
                _item6.Deserialize(reader);
                fxTriggerCounts[_i6] = _item6;
            }

        }


    }
}








