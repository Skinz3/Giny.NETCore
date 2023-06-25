using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectEffectMount : ObjectEffect  
    { 
        public new const ushort Id = 3193;
        public override ushort TypeId => Id;

        public long id;
        public long expirationDate;
        public int model;
        public string name;
        public string owner;
        public byte level;
        public bool sex;
        public bool isRideable;
        public bool isFeconded;
        public bool isFecondationReady;
        public int reproductionCount;
        public int reproductionCountMax;
        public ObjectEffectInteger[] effects;
        public int[] capacities;

        public ObjectEffectMount()
        {
        }
        public ObjectEffectMount(long id,long expirationDate,int model,string name,string owner,byte level,bool sex,bool isRideable,bool isFeconded,bool isFecondationReady,int reproductionCount,int reproductionCountMax,ObjectEffectInteger[] effects,int[] capacities,short actionId)
        {
            this.id = id;
            this.expirationDate = expirationDate;
            this.model = model;
            this.name = name;
            this.owner = owner;
            this.level = level;
            this.sex = sex;
            this.isRideable = isRideable;
            this.isFeconded = isFeconded;
            this.isFecondationReady = isFecondationReady;
            this.reproductionCount = reproductionCount;
            this.reproductionCountMax = reproductionCountMax;
            this.effects = effects;
            this.capacities = capacities;
            this.actionId = actionId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,sex);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,isRideable);
            _box0 = BooleanByteWrapper.SetFlag(_box0,2,isFeconded);
            _box0 = BooleanByteWrapper.SetFlag(_box0,3,isFecondationReady);
            writer.WriteByte((byte)_box0);
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarLong((long)id);
            if (expirationDate < 0 || expirationDate > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + expirationDate + ") on element expirationDate.");
            }

            writer.WriteVarLong((long)expirationDate);
            if (model < 0)
            {
                throw new System.Exception("Forbidden value (" + model + ") on element model.");
            }

            writer.WriteVarInt((int)model);
            writer.WriteUTF((string)name);
            writer.WriteUTF((string)owner);
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteByte((byte)level);
            writer.WriteVarInt((int)reproductionCount);
            if (reproductionCountMax < 0)
            {
                throw new System.Exception("Forbidden value (" + reproductionCountMax + ") on element reproductionCountMax.");
            }

            writer.WriteVarInt((int)reproductionCountMax);
            writer.WriteShort((short)effects.Length);
            for (uint _i13 = 0;_i13 < effects.Length;_i13++)
            {
                (effects[_i13] as ObjectEffectInteger).Serialize(writer);
            }

            writer.WriteShort((short)capacities.Length);
            for (uint _i14 = 0;_i14 < capacities.Length;_i14++)
            {
                if (capacities[_i14] < 0)
                {
                    throw new System.Exception("Forbidden value (" + capacities[_i14] + ") on element 14 (starting at 1) of capacities.");
                }

                writer.WriteVarInt((int)capacities[_i14]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectEffectInteger _item13 = null;
            uint _val14 = 0;
            base.Deserialize(reader);
            byte _box0 = reader.ReadByte();
            sex = BooleanByteWrapper.GetFlag(_box0,0);
            isRideable = BooleanByteWrapper.GetFlag(_box0,1);
            isFeconded = BooleanByteWrapper.GetFlag(_box0,2);
            isFecondationReady = BooleanByteWrapper.GetFlag(_box0,3);
            id = (long)reader.ReadVarUhLong();
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of ObjectEffectMount.id.");
            }

            expirationDate = (long)reader.ReadVarUhLong();
            if (expirationDate < 0 || expirationDate > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + expirationDate + ") on element of ObjectEffectMount.expirationDate.");
            }

            model = (int)reader.ReadVarUhInt();
            if (model < 0)
            {
                throw new System.Exception("Forbidden value (" + model + ") on element of ObjectEffectMount.model.");
            }

            name = (string)reader.ReadUTF();
            owner = (string)reader.ReadUTF();
            level = (byte)reader.ReadByte();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of ObjectEffectMount.level.");
            }

            reproductionCount = (int)reader.ReadVarInt();
            reproductionCountMax = (int)reader.ReadVarUhInt();
            if (reproductionCountMax < 0)
            {
                throw new System.Exception("Forbidden value (" + reproductionCountMax + ") on element of ObjectEffectMount.reproductionCountMax.");
            }

            uint _effectsLen = (uint)reader.ReadUShort();
            for (uint _i13 = 0;_i13 < _effectsLen;_i13++)
            {
                _item13 = new ObjectEffectInteger();
                _item13.Deserialize(reader);
                effects[_i13] = _item13;
            }

            uint _capacitiesLen = (uint)reader.ReadUShort();
            capacities = new int[_capacitiesLen];
            for (uint _i14 = 0;_i14 < _capacitiesLen;_i14++)
            {
                _val14 = (uint)reader.ReadVarUhInt();
                if (_val14 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val14 + ") on elements of capacities.");
                }

                capacities[_i14] = (int)_val14;
            }

        }


    }
}








