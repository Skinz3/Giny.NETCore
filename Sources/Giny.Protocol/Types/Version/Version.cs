using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class Version  
    { 
        public const ushort Id = 1212;
        public virtual ushort TypeId => Id;

        public byte major;
        public byte minor;
        public byte code;
        public int build;
        public byte buildType;

        public Version()
        {
        }
        public Version(byte major,byte minor,byte code,int build,byte buildType)
        {
            this.major = major;
            this.minor = minor;
            this.code = code;
            this.build = build;
            this.buildType = buildType;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (major < 0)
            {
                throw new System.Exception("Forbidden value (" + major + ") on element major.");
            }

            writer.WriteByte((byte)major);
            if (minor < 0)
            {
                throw new System.Exception("Forbidden value (" + minor + ") on element minor.");
            }

            writer.WriteByte((byte)minor);
            if (code < 0)
            {
                throw new System.Exception("Forbidden value (" + code + ") on element code.");
            }

            writer.WriteByte((byte)code);
            if (build < 0)
            {
                throw new System.Exception("Forbidden value (" + build + ") on element build.");
            }

            writer.WriteInt((int)build);
            writer.WriteByte((byte)buildType);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            major = (byte)reader.ReadByte();
            if (major < 0)
            {
                throw new System.Exception("Forbidden value (" + major + ") on element of Version.major.");
            }

            minor = (byte)reader.ReadByte();
            if (minor < 0)
            {
                throw new System.Exception("Forbidden value (" + minor + ") on element of Version.minor.");
            }

            code = (byte)reader.ReadByte();
            if (code < 0)
            {
                throw new System.Exception("Forbidden value (" + code + ") on element of Version.code.");
            }

            build = (int)reader.ReadInt();
            if (build < 0)
            {
                throw new System.Exception("Forbidden value (" + build + ") on element of Version.build.");
            }

            buildType = (byte)reader.ReadByte();
            if (buildType < 0)
            {
                throw new System.Exception("Forbidden value (" + buildType + ") on element of Version.buildType.");
            }

        }


    }
}








