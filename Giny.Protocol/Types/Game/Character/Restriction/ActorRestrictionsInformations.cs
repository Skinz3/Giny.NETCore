using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ActorRestrictionsInformations  
    { 
        public const ushort Id = 4301;
        public virtual ushort TypeId => Id;

        public bool cantBeAggressed;
        public bool cantBeChallenged;
        public bool cantTrade;
        public bool cantBeAttackedByMutant;
        public bool cantRun;
        public bool forceSlowWalk;
        public bool cantMinimize;
        public bool cantMove;
        public bool cantAggress;
        public bool cantChallenge;
        public bool cantExchange;
        public bool cantAttack;
        public bool cantChat;
        public bool cantBeMerchant;
        public bool cantUseObject;
        public bool cantUseTaxCollector;
        public bool cantUseInteractive;
        public bool cantSpeakToNPC;
        public bool cantChangeZone;
        public bool cantAttackMonster;

        public ActorRestrictionsInformations()
        {
        }
        public ActorRestrictionsInformations(bool cantBeAggressed,bool cantBeChallenged,bool cantTrade,bool cantBeAttackedByMutant,bool cantRun,bool forceSlowWalk,bool cantMinimize,bool cantMove,bool cantAggress,bool cantChallenge,bool cantExchange,bool cantAttack,bool cantChat,bool cantBeMerchant,bool cantUseObject,bool cantUseTaxCollector,bool cantUseInteractive,bool cantSpeakToNPC,bool cantChangeZone,bool cantAttackMonster)
        {
            this.cantBeAggressed = cantBeAggressed;
            this.cantBeChallenged = cantBeChallenged;
            this.cantTrade = cantTrade;
            this.cantBeAttackedByMutant = cantBeAttackedByMutant;
            this.cantRun = cantRun;
            this.forceSlowWalk = forceSlowWalk;
            this.cantMinimize = cantMinimize;
            this.cantMove = cantMove;
            this.cantAggress = cantAggress;
            this.cantChallenge = cantChallenge;
            this.cantExchange = cantExchange;
            this.cantAttack = cantAttack;
            this.cantChat = cantChat;
            this.cantBeMerchant = cantBeMerchant;
            this.cantUseObject = cantUseObject;
            this.cantUseTaxCollector = cantUseTaxCollector;
            this.cantUseInteractive = cantUseInteractive;
            this.cantSpeakToNPC = cantSpeakToNPC;
            this.cantChangeZone = cantChangeZone;
            this.cantAttackMonster = cantAttackMonster;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,cantBeAggressed);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,cantBeChallenged);
            _box0 = BooleanByteWrapper.SetFlag(_box0,2,cantTrade);
            _box0 = BooleanByteWrapper.SetFlag(_box0,3,cantBeAttackedByMutant);
            _box0 = BooleanByteWrapper.SetFlag(_box0,4,cantRun);
            _box0 = BooleanByteWrapper.SetFlag(_box0,5,forceSlowWalk);
            _box0 = BooleanByteWrapper.SetFlag(_box0,6,cantMinimize);
            _box0 = BooleanByteWrapper.SetFlag(_box0,7,cantMove);
            writer.WriteByte((byte)_box0);
            byte _box1 = 0;
            _box1 = BooleanByteWrapper.SetFlag(_box1,0,cantAggress);
            _box1 = BooleanByteWrapper.SetFlag(_box1,1,cantChallenge);
            _box1 = BooleanByteWrapper.SetFlag(_box1,2,cantExchange);
            _box1 = BooleanByteWrapper.SetFlag(_box1,3,cantAttack);
            _box1 = BooleanByteWrapper.SetFlag(_box1,4,cantChat);
            _box1 = BooleanByteWrapper.SetFlag(_box1,5,cantBeMerchant);
            _box1 = BooleanByteWrapper.SetFlag(_box1,6,cantUseObject);
            _box1 = BooleanByteWrapper.SetFlag(_box1,7,cantUseTaxCollector);
            writer.WriteByte((byte)_box1);
            byte _box2 = 0;
            _box2 = BooleanByteWrapper.SetFlag(_box2,0,cantUseInteractive);
            _box2 = BooleanByteWrapper.SetFlag(_box2,1,cantSpeakToNPC);
            _box2 = BooleanByteWrapper.SetFlag(_box2,2,cantChangeZone);
            _box2 = BooleanByteWrapper.SetFlag(_box2,3,cantAttackMonster);
            writer.WriteByte((byte)_box2);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            cantBeAggressed = BooleanByteWrapper.GetFlag(_box0,0);
            cantBeChallenged = BooleanByteWrapper.GetFlag(_box0,1);
            cantTrade = BooleanByteWrapper.GetFlag(_box0,2);
            cantBeAttackedByMutant = BooleanByteWrapper.GetFlag(_box0,3);
            cantRun = BooleanByteWrapper.GetFlag(_box0,4);
            forceSlowWalk = BooleanByteWrapper.GetFlag(_box0,5);
            cantMinimize = BooleanByteWrapper.GetFlag(_box0,6);
            cantMove = BooleanByteWrapper.GetFlag(_box0,7);
            byte _box1 = reader.ReadByte();
            cantAggress = BooleanByteWrapper.GetFlag(_box1,0);
            cantChallenge = BooleanByteWrapper.GetFlag(_box1,1);
            cantExchange = BooleanByteWrapper.GetFlag(_box1,2);
            cantAttack = BooleanByteWrapper.GetFlag(_box1,3);
            cantChat = BooleanByteWrapper.GetFlag(_box1,4);
            cantBeMerchant = BooleanByteWrapper.GetFlag(_box1,5);
            cantUseObject = BooleanByteWrapper.GetFlag(_box1,6);
            cantUseTaxCollector = BooleanByteWrapper.GetFlag(_box1,7);
            byte _box2 = reader.ReadByte();
            cantUseInteractive = BooleanByteWrapper.GetFlag(_box2,0);
            cantSpeakToNPC = BooleanByteWrapper.GetFlag(_box2,1);
            cantChangeZone = BooleanByteWrapper.GetFlag(_box2,2);
            cantAttackMonster = BooleanByteWrapper.GetFlag(_box2,3);
        }


    }
}








