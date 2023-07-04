using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class FightPhase
    {
        public const ushort Id = 5619;
        public virtual ushort TypeId => Id;

        public byte phase;
        public double phaseEndTimeStamp;

        public FightPhase()
        {
        }
        public FightPhase(byte phase, double phaseEndTimeStamp)
        {
            this.phase = phase;
            this.phaseEndTimeStamp = phaseEndTimeStamp;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)phase);
            if (phaseEndTimeStamp < -9007199254740992 || phaseEndTimeStamp > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + phaseEndTimeStamp + ") on element phaseEndTimeStamp.");
            }

            writer.WriteDouble((double)phaseEndTimeStamp);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            phase = (byte)reader.ReadByte();
            if (phase < 0)
            {
                throw new System.Exception("Forbidden value (" + phase + ") on element of FightPhase.phase.");
            }

            phaseEndTimeStamp = (double)reader.ReadDouble();
            if (phaseEndTimeStamp < -9007199254740992 || phaseEndTimeStamp > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + phaseEndTimeStamp + ") on element of FightPhase.phaseEndTimeStamp.");
            }

        }


    }
}


