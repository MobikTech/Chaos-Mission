using System;
using System.Net.Sockets;
using Serializer;

namespace LocalServer
{
    public sealed class Client
    {
        public readonly Socket Socket;
        public readonly int ID;
        
        private static int _lastId = 0;
        
        private readonly ISerializer<MessageInfo> _serializer = new ProtobufSerializer<MessageInfo>();

        public Client(Socket client)
        {
            Socket = client;
            ID = _lastId++;
        }
        
        public void SendToThis(MessageInfo messageInfo)
        {
            byte[] message = _serializer.Serialize(messageInfo);
            Socket.Send(message);
        }
        public MessageInfo ReceiveFromThis()
        {
            byte[] usefulData = new byte[Socket.Available];

            Socket.Receive(usefulData);
            return _serializer.Deserialize(usefulData);
        }
    }
}