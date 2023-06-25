using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class StartupActionAddObject  
    { 
        public const ushort Id = 6129;
        public virtual ushort TypeId => Id;

        public int uid;
        public string title;
        public string text;
        public string descUrl;
        public string pictureUrl;
        public ObjectItemInformationWithQuantity[] items;

        public StartupActionAddObject()
        {
        }
        public StartupActionAddObject(int uid,string title,string text,string descUrl,string pictureUrl,ObjectItemInformationWithQuantity[] items)
        {
            this.uid = uid;
            this.title = title;
            this.text = text;
            this.descUrl = descUrl;
            this.pictureUrl = pictureUrl;
            this.items = items;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (uid < 0)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element uid.");
            }

            writer.WriteInt((int)uid);
            writer.WriteUTF((string)title);
            writer.WriteUTF((string)text);
            writer.WriteUTF((string)descUrl);
            writer.WriteUTF((string)pictureUrl);
            writer.WriteShort((short)items.Length);
            for (uint _i6 = 0;_i6 < items.Length;_i6++)
            {
                (items[_i6] as ObjectItemInformationWithQuantity).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            ObjectItemInformationWithQuantity _item6 = null;
            uid = (int)reader.ReadInt();
            if (uid < 0)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element of StartupActionAddObject.uid.");
            }

            title = (string)reader.ReadUTF();
            text = (string)reader.ReadUTF();
            descUrl = (string)reader.ReadUTF();
            pictureUrl = (string)reader.ReadUTF();
            uint _itemsLen = (uint)reader.ReadUShort();
            for (uint _i6 = 0;_i6 < _itemsLen;_i6++)
            {
                _item6 = new ObjectItemInformationWithQuantity();
                _item6.Deserialize(reader);
                items[_i6] = _item6;
            }

        }


    }
}








