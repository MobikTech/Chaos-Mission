using Google.Protobuf;

namespace Serializer
{
    public static class MessageSerializer 
    {
        public static byte[] Serialize(MessageInfo message) => message.ToByteArray();
        public static MessageInfo Deserialize(byte[] data) => MessageInfo.Parser.ParseFrom(data);
    }
}