using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace ChaosMission.Networking
{
    public class NetworkClient : MonoBehaviour
    {
        private Socket _clientSocket;

        private void Awake()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void TryConnectToAddress(string serverAddress)
        {
            try
            {
                SplitAddress(serverAddress, out IPAddress ipAddress, out int port);
                _clientSocket.ConnectAsync(ipAddress, port);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private void SplitAddress(string address, out IPAddress ipAddress, out int port)
        {
            string[] addressParts = address.Split(':');
            ipAddress = IPAddress.Parse(addressParts[0]);
            port = Convert.ToInt32(addressParts[1]);
        }
    }
}