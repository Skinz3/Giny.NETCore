using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Giny.Core.IO
{
    public static class Protobuf
    {
        public static byte[] Serialize(object record)
        {
            if (null == record) throw new Exception();

            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, record);
                return stream.ToArray();
            }
        }
        public static T Deserialize<T>(byte[] buffer) where T : class
        {
            using (var stream = new MemoryStream(buffer))
            {
                return Serializer.Deserialize<T>(stream);
            }
        }
        public static object Deserialize(byte[] buffer, Type type)
        {
            using (var stream = new MemoryStream(buffer))
            {
                return Serializer.Deserialize(type, stream);
            }
        }
    }
}
