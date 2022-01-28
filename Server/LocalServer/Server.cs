using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace LocalServer
{
    public sealed class Server : IServer, IDisposable
    {
        private readonly IPEndPoint _serverIpPoint; 
        private readonly Socket _serverSocket; 
        private readonly int _maxPlayers;

        private readonly Dictionary<int, Client> _clients;
        private bool _accepting = true;

        public Server(string ip, int port, int maxPlayers)
        {
            _maxPlayers = maxPlayers;
            _serverIpPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new Dictionary<int, Client>();
        }

#region IServer Methods

        public async void RunServer()
        {
            Console.WriteLine("Starting server...");
                    
            _serverSocket.Bind(_serverIpPoint);
            _serverSocket.Listen(_maxPlayers);
                    
            Console.WriteLine($"Server started on {_serverIpPoint.Address}:{_serverIpPoint.Port}.");
            
            Task t = StartClientsAccepting();
            
            while (true) ;
            // StartReceiving();
        }

        public void CloseServer()
        {
            foreach (Client client in _clients.Values)
            {
                DisconnectClient(client.ID);
            }
            _clients.Clear();
            
            _serverSocket.Shutdown(SocketShutdown.Both);
            _serverSocket.Close();
        }

#endregion
       
        private async Task StartClientsAccepting()
        {
            StopClientsAccepting();
            while (_accepting)
            {
                Socket clientSocket = await _serverSocket.AcceptAsync();
                Client client = AddClient(clientSocket);
                StartReceiving(client);
            }
        }

        private void StopClientsAccepting() => _accepting = false;



        // private void StartReceiving()
        // {
        //     while (true)
        //     {
        //         foreach (Client client in _clients.Values)
        //         {
        //             // if (lastNumber != _clients.Count)
        //             // {
        //             //     lastNumber = _clients.Count;
        //             //     Console.WriteLine(lastNumber);
        //             // }
        //             if (client.Socket.Available > 0)
        //             {
        //                 Console.WriteLine($"[{DateTime.Now.ToString()}] {client.ID}: {client.ReceiveFromThis<Vector3>().ToString()}");
        //             }
        //         }
        //         // if (!_serverSocket.)
        //         // {
        //         //     continue;
        //         // }
        //         
        //     }
        // }

        private Client AddClient(Socket clientSocket)
        {
            Client newClient = new Client(clientSocket);
            _clients.Add(newClient.ID, newClient);
            
            Console.WriteLine("New client connected!");
            Console.WriteLine($"Current clients amount: {_clients.Count}");

            return newClient;
        }

        private async Task StartReceiving(Client client)
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
                        $"[{DateTime.Now.ToString()}] {client.ID}: {client.ReceiveFromThis().X}");
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


        public void Dispose() => CloseServer();
    }
}