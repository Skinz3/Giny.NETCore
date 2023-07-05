using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class PrismInformation
    {
        public const ushort Id = 85;
        public virtual ushort TypeId => Id;

        public byte state;
        public int placementDate;
        public int nuggetsCount;
        public int durability;
        public double nextEvolutionDate;

        public PrismInformation()
        {
        }
        public PrismInformation(byte state, int placementDate, int nuggetsCount, int durability, double nextEvolutionDate)
        {
            this.state = state;
            this.placementDate = placementDate;
            this.nuggetsCount = nuggetsCount;
            this.durability = durability;
            this.nextEvolutionDate = nextEvolutionDate;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)state);
            if (placementDate < 0)
            {
                throw new System.Exception("Forbidden value (" + placementDate + ") on element placementDate.");
            }

            writer.WriteInt((int)placementDate);
            if (nuggetsCount < 0)
            {
                throw new System.Exception("Forbidden value (" + nuggetsCount + ") on element nuggetsCount.");
            }

            writer.WriteVarInt((int)nuggetsCount);
            if (durability < 0)
            {
                throw new System.Exception("Forbidden value (" + durability + ") on element durability.");
            }

            writer.WriteInt((int)durability);
            if (nextEvolutionDate < 0 || nextEvolutionDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + nextEvolutionDate + ") on element nextEvolutionDate.");
            }

            writer.WriteDouble((double)nextEvolutionDate);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of PrismInformation.state.");
            }

            placementDate = (int)reader.ReadInt();
            if (placementDate < 0)
            {
                throw new System.Exception("Forbidden value (" + placementDate + ") on element of PrismInformation.placementDate.");
            }

            nuggetsCount = (int)reader.ReadVarUhInt();
            if (nuggetsCount < 0)
            {
                throw new System.Exception("Forbidden value (" + nuggetsCount + ") on element of PrismInformation.nuggetsCount.");
            }

            durability = (int)reader.ReadInt();
            if (durability < 0)
            {
                throw new System.Exception("Forbidden value (" + durability + ") on element of PrismInformation.durability.");
            }

            nextEvolutionDate = (double)reader.ReadDouble();
            if (nextEvolutionDate < 0 || nextEvolutionDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + nextEvolutionDate + ") on element of PrismInformation.nextEvolutionDate.");
            }

        }


    }
}


