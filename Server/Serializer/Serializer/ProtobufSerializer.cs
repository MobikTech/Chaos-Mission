using Google.Protobuf;

namespace Serializer
{
    public class ProtobufSerializer<T> : ISerializer<T> where T : IMessage<T>, new()
    {
        private readonly MessageParser<T> _parser = new MessageParser<T>(() => new T());

        public byte[] Serialize(T message) => message.ToByteArray();
        
        public T Deserialize(byte[] data) => _parser.ParseFrom(data);
    }
}