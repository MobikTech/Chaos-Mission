using System;
using System.Globalization;
using System.Net;
using Serializer;
using Unity.VisualScripting;
using UnityEngine;

namespace ChaosMission.Networking
{
    public class NetworkSystem : MonoBehaviour, IDisposable
    {
        [SerializeField] private GameObject _player;
        private ServerConnection _serverConnection;
        
        private void Awake()
        {
            _serverConnection = new ServerConnection();
            _serverConnection.ServerMessageReceived += OnReceivedMessage;
        }

        private void Update()
        {
            UpdateMessage();
        }

        private void OnDestroy() => Dispose();

        
        public void TryConnectToAddress(string serverAddress)
        {
            try
            {
                SplitAddress(serverAddress, out IPAddress ipAddress, out int port);
                _serverConnection.ConnectToServer(ipAddress, port);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void OnReceivedMessage(MessageInfo messageInfo)
        {
            Debug.Log($"[{DateTime.Now.ToString(CultureInfo.InvariantCulture)}]: {messageInfo}");
        }
        
        private void SplitAddress(string address, out IPAddress ipAddress, out int port)
        {
            string[] addressParts = address.Split(':');
            ipAddress = IPAddress.Parse(addressParts[0]);
            port = Convert.ToInt32(addressParts[1]);
        }

        private void UpdateMessage()
        {
            _serverConnection.SendableMessage = new MessageInfo()
            {
                X = _player.transform.position.x
            };
        }

        public void Dispose()
        {
            _serverConnection?.Dispose();
        }
    }
}