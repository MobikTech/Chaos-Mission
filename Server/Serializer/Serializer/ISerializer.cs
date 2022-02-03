namespace Serializer
{
    public interface ISerializer<T>
    {
        public byte[] Serialize(T message);
        public T Deserialize(byte[] data);
    }
    
    public interface ISerializer : ISerializer<object>
    {
     
    }
}