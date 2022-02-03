using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Serializer;
using UnityEngine;

namespace ChaosMission.Networking
{
    public sealed class ServerConnection : IDisposable
    {
        private readonly Socket _clientSocket;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly ISerializer<MessageInfo> _serializer = new ProtobufSerializer<MessageInfo>();
   
        public Action<MessageInfo> ServerMessageReceived;
        public MessageInfo SendableMessage { private get; set; }
        
        public ServerConnection()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }


        public void ConnectToServer(IPAddress ipAddress, int port)
        {
            _clientSocket.Connect(ipAddress, port);
            StartReceiving(_cancellationTokenSource.Token);
        }
        
        
        private async void StartReceiving(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                while (_clientSocket.Connected)
                {
                    if (_clientSocket.Available <= 0)
                    {
                        continue;
                    }
                    
                    ServerMessageReceived?.Invoke(ReceiveMessage());
                }
            }, cancellationToken);
        }
        
        private async void StartSending(CancellationToken cancellationToken)
        {
            while (_clientSocket.Connected && cancellationToken.IsCancellationRequested == false)
            {
                SendMessage(SendableMessage);
                await Task.Yield();
            }
        }
        
        public void SendMessage(MessageInfo messageInfo)
        {
            byte[] message = _serializer.Serialize(messageInfo);
            
            if (_clientSocket.Connected)
            {
                _clientSocket.Send(message);
            }
        }
        private MessageInfo ReceiveMessage()
        {
            byte[] usefulData = new byte[_clientSocket.Available];

            _clientSocket.Receive(usefulData);
        
            return _serializer.Deserialize(usefulData);
        }

        
        public void Dispose()
        {
            _clientSocket?.Dispose();
            _cancellationTokenSource?.Dispose();
        }
    }
}