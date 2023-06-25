using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class StatedElement  
    { 
        public const ushort Id = 9515;
        public virtual ushort TypeId => Id;

        public int elementId;
        public short elementCellId;
        public int elementState;
        public bool onCurrentMap;

        public StatedElement()
        {
        }
        public StatedElement(int elementId,short elementCellId,int elementState,bool onCurrentMap)
        {
            this.elementId = elementId;
            this.elementCellId = elementCellId;
            this.elementState = elementState;
            this.onCurrentMap = onCurrentMap;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (elementId < 0)
            {
                throw new System.Exception("Forbidden value (" + elementId + ") on element elementId.");
            }

            writer.WriteInt((int)elementId);
            if (elementCellId < 0 || elementCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + elementCellId + ") on element elementCellId.");
            }

            writer.WriteVarShort((short)elementCellId);
            if (elementState < 0)
            {
                throw new System.Exception("Forbidden value (" + elementState + ") on element elementState.");
            }

            writer.WriteVarInt((int)elementState);
            writer.WriteBoolean((bool)onCurrentMap);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            elementId = (int)reader.ReadInt();
            if (elementId < 0)
            {
                throw new System.Exception("Forbidden value (" + elementId + ") on element of StatedElement.elementId.");
            }

            elementCellId = (short)reader.ReadVarUhShort();
            if (elementCellId < 0 || elementCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + elementCellId + ") on element of StatedElement.elementCellId.");
            }

            elementState = (int)reader.ReadVarUhInt();
            if (elementState < 0)
            {
                throw new System.Exception("Forbidden value (" + elementState + ") on element of StatedElement.elementState.");
            }

            onCurrentMap = (bool)reader.ReadBoolean();
        }


    }
}








