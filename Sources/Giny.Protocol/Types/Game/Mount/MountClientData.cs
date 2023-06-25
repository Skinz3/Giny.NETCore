using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class MountClientData  
    { 
        public const ushort Id = 7643;
        public virtual ushort TypeId => Id;

        public double id;
        public int model;
        public int[] ancestor;
        public int[] behaviors;
        public string name;
        public bool sex;
        public int ownerId;
        public long experience;
        public long experienceForLevel;
        public double experienceForNextLevel;
        public byte level;
        public bool isRideable;
        public int maxPods;
        public bool isWild;
        public int stamina;
        public int staminaMax;
        public int maturity;
        public int maturityForAdult;
        public int energy;
        public int energyMax;
        public int serenity;
        public int aggressivityMax;
        public int serenityMax;
        public int love;
        public int loveMax;
        public int fecondationTime;
        public bool isFecondationReady;
        public int boostLimiter;
        public double boostMax;
        public int reproductionCount;
        public int reproductionCountMax;
        public short harnessGID;
        public bool useHarnessColors;
        public ObjectEffectInteger[] effectList;

        public MountClientData()
        {
        }
        public MountClientData(double id,int model,int[] ancestor,int[] behaviors,string name,bool sex,int ownerId,long experience,long experienceForLevel,double experienceForNextLevel,byte level,bool isRideable,int maxPods,bool isWild,int stamina,int staminaMax,int maturity,int maturityForAdult,int energy,int energyMax,int serenity,int aggressivityMax,int serenityMax,int love,int loveMax,int fecondationTime,bool isFecondationReady,int boostLimiter,double boostMax,int reproductionCount,int reproductionCountMax,short harnessGID,bool useHarnessColors,ObjectEffectInteger[] effectList)
        {
            this.id = id;
            this.model = model;
            this.ancestor = ancestor;
            this.behaviors = behaviors;
            this.name = name;
            this.sex = sex;
            this.ownerId = ownerId;
            this.experience = experience;
            this.experienceForLevel = experienceForLevel;
            this.experienceForNextLevel = experienceForNextLevel;
            this.level = level;
            this.isRideable = isRideable;
            this.maxPods = maxPods;
            this.isWild = isWild;
            this.stamina = stamina;
            this.staminaMax = staminaMax;
            this.maturity = maturity;
            this.maturityForAdult = maturityForAdult;
            this.energy = energy;
            this.energyMax = energyMax;
            this.serenity = serenity;
            this.aggressivityMax = aggressivityMax;
            this.serenityMax = serenityMax;
            this.love = love;
            this.loveMax = loveMax;
            this.fecondationTime = fecondationTime;
            this.isFecondationReady = isFecondationReady;
            this.boostLimiter = boostLimiter;
            this.boostMax = boostMax;
            this.reproductionCount = reproductionCount;
            this.reproductionCountMax = reproductionCountMax;
            this.harnessGID = harnessGID;
            this.useHarnessColors = useHarnessColors;
            this.effectList = effectList;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,sex);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,isRideable);
            _box0 = BooleanByteWrapper.SetFlag(_box0,2,isWild);
            _box0 = BooleanByteWrapper.SetFlag(_box0,3,isFecondationReady);
            _box0 = BooleanByteWrapper.SetFlag(_box0,4,useHarnessColors);
            writer.WriteByte((byte)_box0);
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteDouble((double)id);
            if (model < 0)
            {
                throw new System.Exception("Forbidden value (" + model + ") on element model.");
            }

            writer.WriteVarInt((int)model);
            writer.WriteShort((short)ancestor.Length);
            for (uint _i3 = 0;_i3 < ancestor.Length;_i3++)
            {
                if (ancestor[_i3] < 0)
                {
                    throw new System.Exception("Forbidden value (" + ancestor[_i3] + ") on element 3 (starting at 1) of ancestor.");
                }

                writer.WriteInt((int)ancestor[_i3]);
            }

            writer.WriteShort((short)behaviors.Length);
            for (uint _i4 = 0;_i4 < behaviors.Length;_i4++)
            {
                if (behaviors[_i4] < 0)
                {
                    throw new System.Exception("Forbidden value (" + behaviors[_i4] + ") on element 4 (starting at 1) of behaviors.");
                }

                writer.WriteInt((int)behaviors[_i4]);
            }

            writer.WriteUTF((string)name);
            if (ownerId < 0)
            {
                throw new System.Exception("Forbidden value (" + ownerId + ") on element ownerId.");
            }

            writer.WriteInt((int)ownerId);
            if (experience < 0 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element experience.");
            }

            writer.WriteVarLong((long)experience);
            if (experienceForLevel < 0 || experienceForLevel > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceForLevel + ") on element experienceForLevel.");
            }

            writer.WriteVarLong((long)experienceForLevel);
            if (experienceForNextLevel < -9.00719925474099E+15 || experienceForNextLevel > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceForNextLevel + ") on element experienceForNextLevel.");
            }

            writer.WriteDouble((double)experienceForNextLevel);
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteByte((byte)level);
            if (maxPods < 0)
            {
                throw new System.Exception("Forbidden value (" + maxPods + ") on element maxPods.");
            }

            writer.WriteVarInt((int)maxPods);
            if (stamina < 0)
            {
                throw new System.Exception("Forbidden value (" + stamina + ") on element stamina.");
            }

            writer.WriteVarInt((int)stamina);
            if (staminaMax < 0)
            {
                throw new System.Exception("Forbidden value (" + staminaMax + ") on element staminaMax.");
            }

            writer.WriteVarInt((int)staminaMax);
            if (maturity < 0)
            {
                throw new System.Exception("Forbidden value (" + maturity + ") on element maturity.");
            }

            writer.WriteVarInt((int)maturity);
            if (maturityForAdult < 0)
            {
                throw new System.Exception("Forbidden value (" + maturityForAdult + ") on element maturityForAdult.");
            }

            writer.WriteVarInt((int)maturityForAdult);
            if (energy < 0)
            {
                throw new System.Exception("Forbidden value (" + energy + ") on element energy.");
            }

            writer.WriteVarInt((int)energy);
            if (energyMax < 0)
            {
                throw new System.Exception("Forbidden value (" + energyMax + ") on element energyMax.");
            }

            writer.WriteVarInt((int)energyMax);
            writer.WriteInt((int)serenity);
            writer.WriteInt((int)aggressivityMax);
            if (serenityMax < 0)
            {
                throw new System.Exception("Forbidden value (" + serenityMax + ") on element serenityMax.");
            }

            writer.WriteVarInt((int)serenityMax);
            if (love < 0)
            {
                throw new System.Exception("Forbidden value (" + love + ") on element love.");
            }

            writer.WriteVarInt((int)love);
            if (loveMax < 0)
            {
                throw new System.Exception("Forbidden value (" + loveMax + ") on element loveMax.");
            }

            writer.WriteVarInt((int)loveMax);
            writer.WriteInt((int)fecondationTime);
            if (boostLimiter < 0)
            {
                throw new System.Exception("Forbidden value (" + boostLimiter + ") on element boostLimiter.");
            }

            writer.WriteInt((int)boostLimiter);
            if (boostMax < -9.00719925474099E+15 || boostMax > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + boostMax + ") on element boostMax.");
            }

            writer.WriteDouble((double)boostMax);
            writer.WriteInt((int)reproductionCount);
            if (reproductionCountMax < 0)
            {
                throw new System.Exception("Forbidden value (" + reproductionCountMax + ") on element reproductionCountMax.");
            }

            writer.WriteVarInt((int)reproductionCountMax);
            if (harnessGID < 0)
            {
                throw new System.Exception("Forbidden value (" + harnessGID + ") on element harnessGID.");
            }

            writer.WriteVarShort((short)harnessGID);
            writer.WriteShort((short)effectList.Length);
            for (uint _i34 = 0;_i34 < effectList.Length;_i34++)
            {
                (effectList[_i34] as ObjectEffectInteger).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _val3 = 0;
            uint _val4 = 0;
            ObjectEffectInteger _item34 = null;
            byte _box0 = reader.ReadByte();
            sex = BooleanByteWrapper.GetFlag(_box0,0);
            isRideable = BooleanByteWrapper.GetFlag(_box0,1);
            isWild = BooleanByteWrapper.GetFlag(_box0,2);
            isFecondationReady = BooleanByteWrapper.GetFlag(_box0,3);
            useHarnessColors = BooleanByteWrapper.GetFlag(_box0,4);
            id = (double)reader.ReadDouble();
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of MountClientData.id.");
            }

            model = (int)reader.ReadVarUhInt();
            if (model < 0)
            {
                throw new System.Exception("Forbidden value (" + model + ") on element of MountClientData.model.");
            }

            uint _ancestorLen = (uint)reader.ReadUShort();
            ancestor = new int[_ancestorLen];
            for (uint _i3 = 0;_i3 < _ancestorLen;_i3++)
            {
                _val3 = (uint)reader.ReadInt();
                if (_val3 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val3 + ") on elements of ancestor.");
                }

                ancestor[_i3] = (int)_val3;
            }

            uint _behaviorsLen = (uint)reader.ReadUShort();
            behaviors = new int[_behaviorsLen];
            for (uint _i4 = 0;_i4 < _behaviorsLen;_i4++)
            {
                _val4 = (uint)reader.ReadInt();
                if (_val4 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val4 + ") on elements of behaviors.");
                }

                behaviors[_i4] = (int)_val4;
            }

            name = (string)reader.ReadUTF();
            ownerId = (int)reader.ReadInt();
            if (ownerId < 0)
            {
                throw new System.Exception("Forbidden value (" + ownerId + ") on element of MountClientData.ownerId.");
            }

            experience = (long)reader.ReadVarUhLong();
            if (experience < 0 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element of MountClientData.experience.");
            }

            experienceForLevel = (long)reader.ReadVarUhLong();
            if (experienceForLevel < 0 || experienceForLevel > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceForLevel + ") on element of MountClientData.experienceForLevel.");
            }

            experienceForNextLevel = (double)reader.ReadDouble();
            if (experienceForNextLevel < -9.00719925474099E+15 || experienceForNextLevel > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceForNextLevel + ") on element of MountClientData.experienceForNextLevel.");
            }

            level = (byte)reader.ReadByte();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of MountClientData.level.");
            }

            maxPods = (int)reader.ReadVarUhInt();
            if (maxPods < 0)
            {
                throw new System.Exception("Forbidden value (" + maxPods + ") on element of MountClientData.maxPods.");
            }

            stamina = (int)reader.ReadVarUhInt();
            if (stamina < 0)
            {
                throw new System.Exception("Forbidden value (" + stamina + ") on element of MountClientData.stamina.");
            }

            staminaMax = (int)reader.ReadVarUhInt();
            if (staminaMax < 0)
            {
                throw new System.Exception("Forbidden value (" + staminaMax + ") on element of MountClientData.staminaMax.");
            }

            maturity = (int)reader.ReadVarUhInt();
            if (maturity < 0)
            {
                throw new System.Exception("Forbidden value (" + maturity + ") on element of MountClientData.maturity.");
            }

            maturityForAdult = (int)reader.ReadVarUhInt();
            if (maturityForAdult < 0)
            {
                throw new System.Exception("Forbidden value (" + maturityForAdult + ") on element of MountClientData.maturityForAdult.");
            }

            energy = (int)reader.ReadVarUhInt();
            if (energy < 0)
            {
                throw new System.Exception("Forbidden value (" + energy + ") on element of MountClientData.energy.");
            }

            energyMax = (int)reader.ReadVarUhInt();
            if (energyMax < 0)
            {
                throw new System.Exception("Forbidden value (" + energyMax + ") on element of MountClientData.energyMax.");
            }

            serenity = (int)reader.ReadInt();
            aggressivityMax = (int)reader.ReadInt();
            serenityMax = (int)reader.ReadVarUhInt();
            if (serenityMax < 0)
            {
                throw new System.Exception("Forbidden value (" + serenityMax + ") on element of MountClientData.serenityMax.");
            }

            love = (int)reader.ReadVarUhInt();
            if (love < 0)
            {
                throw new System.Exception("Forbidden value (" + love + ") on element of MountClientData.love.");
            }

            loveMax = (int)reader.ReadVarUhInt();
            if (loveMax < 0)
            {
                throw new System.Exception("Forbidden value (" + loveMax + ") on element of MountClientData.loveMax.");
            }

            fecondationTime = (int)reader.ReadInt();
            boostLimiter = (int)reader.ReadInt();
            if (boostLimiter < 0)
            {
                throw new System.Exception("Forbidden value (" + boostLimiter + ") on element of MountClientData.boostLimiter.");
            }

            boostMax = (double)reader.ReadDouble();
            if (boostMax < -9.00719925474099E+15 || boostMax > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + boostMax + ") on element of MountClientData.boostMax.");
            }

            reproductionCount = (int)reader.ReadInt();
            reproductionCountMax = (int)reader.ReadVarUhInt();
            if (reproductionCountMax < 0)
            {
                throw new System.Exception("Forbidden value (" + reproductionCountMax + ") on element of MountClientData.reproductionCountMax.");
            }

            harnessGID = (short)reader.ReadVarUhShort();
            if (harnessGID < 0)
            {
                throw new System.Exception("Forbidden value (" + harnessGID + ") on element of MountClientData.harnessGID.");
            }

            uint _effectListLen = (uint)reader.ReadUShort();
            for (uint _i34 = 0;_i34 < _effectListLen;_i34++)
            {
                _item34 = new ObjectEffectInteger();
                _item34.Deserialize(reader);
                effectList[_i34] = _item34;
            }

        }


    }
}








