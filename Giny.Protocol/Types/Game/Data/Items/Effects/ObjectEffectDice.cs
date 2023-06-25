using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectEffectDice : ObjectEffect  
    { 
        public new const ushort Id = 4234;
        public override ushort TypeId => Id;

        public int diceNum;
        public int diceSide;
        public int diceConst;

        public ObjectEffectDice()
        {
        }
        public ObjectEffectDice(int diceNum,int diceSide,int diceConst,short actionId)
        {
            this.diceNum = diceNum;
            this.diceSide = diceSide;
            this.diceConst = diceConst;
            this.actionId = actionId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (diceNum < 0)
            {
                throw new System.Exception("Forbidden value (" + diceNum + ") on element diceNum.");
            }

            writer.WriteVarInt((int)diceNum);
            if (diceSide < 0)
            {
                throw new System.Exception("Forbidden value (" + diceSide + ") on element diceSide.");
            }

            writer.WriteVarInt((int)diceSide);
            if (diceConst < 0)
            {
                throw new System.Exception("Forbidden value (" + diceConst + ") on element diceConst.");
            }

            writer.WriteVarInt((int)diceConst);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            diceNum = (int)reader.ReadVarUhInt();
            if (diceNum < 0)
            {
                throw new System.Exception("Forbidden value (" + diceNum + ") on element of ObjectEffectDice.diceNum.");
            }

            diceSide = (int)reader.ReadVarUhInt();
            if (diceSide < 0)
            {
                throw new System.Exception("Forbidden value (" + diceSide + ") on element of ObjectEffectDice.diceSide.");
            }

            diceConst = (int)reader.ReadVarUhInt();
            if (diceConst < 0)
            {
                throw new System.Exception("Forbidden value (" + diceConst + ") on element of ObjectEffectDice.diceConst.");
            }

        }


    }
}








