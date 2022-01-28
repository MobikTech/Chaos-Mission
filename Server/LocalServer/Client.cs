using System.Net.Sockets;
using LocalServer.Serializer;

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
            // NetworkStream stream = TcpClient.GetStream();
            //
            // byte[] sendingData = Encoding.UTF8.GetBytes(message);
            //
            // stream.Write(sendingData, 0, sendingData.Length);
            // Console.WriteLine("Sent message: {0}", message);
            // stream.Close();
        }
        
        // public string ReceiveFromThis()
        // {
        //     byte[] receivingData = new byte[512];
        //
        //     Socket.Receive(receivingData);
        //
        //     return Encoding.UTF8.GetString(receivingData);
        // }
        
        public MessageInfo ReceiveFromThis()
        {
            byte[] receivingData = new byte[512];

            Socket.Receive(receivingData);

            return MessageSerializer.Deserialize(receivingData);
        }
    }
}