using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serializer
{
    // todo refactor using statements
    public class BinarySerializer : ISerializer
    {
        public byte[] Serialize(object message)
        {
            if (message == null)
            {
                throw new NullReferenceException("Message cannot be null");
            }
            
            IFormatter bf = new BinaryFormatter();
            
            using (MemoryStream stream = new MemoryStream())
            {
                bf.Serialize(stream, message);
                return stream.ToArray();
            }
        }

        public object Deserialize(byte[] data)
        {
            IFormatter bf = new BinaryFormatter();
            
            using (MemoryStream stream = new MemoryStream(data))
            {
                return bf.Deserialize(stream);
            }
        }
    }
}