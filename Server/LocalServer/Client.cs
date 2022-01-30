using System.Net.Sockets;
using Serializer;

namespace LocalServer
{
    public sealed class Client
    {
        public readonly Socket Socket;
        public readonly int ID;
        
        private static int _lastId = 0;

        public Client(Socket client)
        {
            Socket = client;
            ID = _lastId++;
        }
        
        public void SendMessageToThis(string message)
        { 
    
        }
        
        public MessageInfo ReceiveFromThis()
        {
            byte[] receivingData = new byte[512];

            Socket.Receive(receivingData);

            return MessageSerializer.Deserialize(receivingData);
        }
    }
}