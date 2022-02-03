using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using LocalServer.Settings;
using Serializer;

namespace LocalServer
{
    public sealed class Server : IServer, IDisposable
    {
        private readonly IPEndPoint _serverIpPoint; 
        private readonly Socket _serverSocket; 
        private readonly int _maxPlayers;

        private readonly Dictionary<int, Client> _clients;
        private bool _accepting = true;
        private readonly GameState _currentGameState;

        public Server(string ip, int port, int maxPlayers)
        {
            _maxPlayers = maxPlayers;
            _currentGameState = new GameState();
            _serverIpPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new Dictionary<int, Client>();
        }

#region IServerMethods

        public async void RunServer()
        {
            Console.WriteLine("Starting server...");
                    
            _serverSocket.Bind(_serverIpPoint);
            _serverSocket.Listen(_maxPlayers);
                    
            Console.WriteLine($"Server started on {_serverIpPoint.Address}:{_serverIpPoint.Port}.");
            
            StartNotifyPlayers();
            StartClientsAccepting();
        }

        public void CloseServer()
        {
            StopClientsAccepting();
            foreach (Client client in _clients.Values)
            {
                DisconnectClient(client.ID);
            }
            _clients.Clear();

            _serverSocket.Shutdown(SocketShutdown.Both);
            _serverSocket.Close();
        }

#endregion
       
        private void StartClientsAccepting()
        {
            while (_accepting)
            {
                Socket clientSocket = _serverSocket.Accept();
                AddClient(clientSocket);
            }
        }
        private void StopClientsAccepting() => _accepting = false;

        private async void StartNotifyPlayers()
        {
            // while (_serverSocket.IsBound)
            // {
            //     if (_clients.Count == 0)
            //     {
            //         continue;
            //     }
            //     
            //     MessageInfo messageInfo = new MessageInfo(x: _currentGameState.CurrentNumber);
            //
            //     foreach (Client client in _clients.Values)
            //     {
            //         client.SendToThis(messageInfo);
            //     }
            //         
            //     _currentGameState.CurrentNumber++;
            //     await Task.Delay(1000 / ServerSettings.NotificationFrequencyPerSec);
            // }

            await Task.Run(async () =>
            {
                while (_serverSocket.IsBound)
                {
                    if (_clients.Count == 0)
                    {
                        continue;
                    }
                    
                    var messageInfo = new MessageInfo(x: _currentGameState.CurrentNumber);
            
                    foreach (Client client in _clients.Values)
                    {
                        // Console.WriteLine($"Local {client.ID} - {client.Socket.LocalEndPoint}");
                        // Console.WriteLine($"Remote {client.ID} - {client.Socket.RemoteEndPoint}");
                        client.SendToThis(messageInfo);
                    }
                    
                    _currentGameState.CurrentNumber++;
                    await Task.Delay(1000 / ServerSettings.NotificationFrequencyPerSec);
                }
            });
        }

        private Client AddClient(Socket clientSocket)
        {
            Client newClient = new Client(clientSocket);
            _clients.Add(newClient.ID, newClient);
            
            Console.WriteLine("New client connected!");
            Console.WriteLine($"Current clients amount: {_clients.Count}");

            StartReceiving(newClient);
            return newClient;
        }

        private async void StartReceiving(Client client)
        {
            await Task.Run(() =>
            {
                while (client.Socket.Connected)
                {
                    if (client.Socket.Available <= 0)
                    {
                        continue;
                    }
                    Console.WriteLine(
                        $"[{DateTime.Now.ToString(CultureInfo.CurrentCulture)}] {client.ID}: {client.ReceiveFromThis().ToString()}");
                }
            });
        }

        private void DisconnectClient(int id)
        {
            Client client = _clients[id];
            
            client.Socket.Shutdown(SocketShutdown.Both);
            client.Socket.Close();
            
            _clients.Remove(id);
        }

        private void ChangeGameState()
        {
            
        }

        public void Dispose() => CloseServer();
    }
}