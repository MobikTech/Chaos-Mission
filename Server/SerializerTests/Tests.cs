using System;
using Serializer;
using NUnit.Framework;

namespace SerializerTests
{
    [TestFixture]
    public class Tests
    {

#region ProtoSerializerTests

        [Test]
        public void SerializeProto_50_NoException()
        {
            ProtobufSerializer<MessageInfo> serializer = new ProtobufSerializer<MessageInfo>();
            
            MessageInfo messageInfo = new MessageInfo()
            {
                X = 50,
                Y = 50,
                Z = 50
            };
            
            Assert.DoesNotThrow(() =>
            {
                byte[] actual = serializer.Serialize(messageInfo);
            });
        }
        
        [Test]
        public void DeserializeProto_bytes_NoException()
        {
            ISerializer<MessageInfo> serializer = new ProtobufSerializer<MessageInfo>();
        
            MessageInfo messageInfo = new MessageInfo()
            {
                X = 50,
                Y = 50,
                Z = 50
            };
         
            byte[] bytes = serializer.Serialize(messageInfo);
            foreach (byte b in bytes)
            {
                Console.Write($"{b} ");
            }
            MessageInfo actualMessageInfo = serializer.Deserialize(bytes);
        
            Assert.AreEqual(actualMessageInfo.X, 50);
        }

#endregion


#region BinarySerializerTests

        [Test]
        public void SerializeBinary_50_NoException()
        {
            ISerializer serializer = new BinarySerializer();

            int message = 50;
            
            Assert.DoesNotThrow(() =>
            {
                byte[] actual = serializer.Serialize(message);
            });
        }
                
        [Test]
        public void DeserializeBinary_bytes_NoException()
        {
            ISerializer serializer = new BinarySerializer();
        
            int message = 50;

            // MessageInfo messageInfo = new MessageInfo()
            // {
            //     X = 50
            // };
         
            byte[] bytes = serializer.Serialize(message);
            int actualMessage = (int)serializer.Deserialize(bytes);
        
            Assert.AreEqual(50, actualMessage);
        }

#endregion
       
    }
}