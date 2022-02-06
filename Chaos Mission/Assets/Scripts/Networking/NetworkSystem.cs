using System;
using System.Collections;
using System.Net;
using Serializer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChaosMission.Networking
{
    public class NetworkSystem : MonoBehaviour, IDisposable
    {
        private const string PlayerTag = "Player"; 
        private const int Level1SceneIndex = 1; 
        
        private GameObject _player;
        private ServerConnection _serverConnection;
        
        private void Awake()
        {
            _serverConnection = new ServerConnection();
            _serverConnection.ServerMessageReceived += OnReceivedMessage;
        }

        private void Update()
        {
            if (_player != null)
            {
                UpdateMessage();
            }
        }

        private void OnDestroy() => Dispose();
        
        public void TryConnectToAddress(string serverAddress)
        {
            try
            {
                SplitAddress(serverAddress, out IPAddress ipAddress, out int port);
                _serverConnection.ConnectToServer(ipAddress, port);

                SceneManager.LoadScene(Level1SceneIndex);
                
                StartCoroutine(SetPlayerWhenSceneLoaded(Level1SceneIndex));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private IEnumerator SetPlayerWhenSceneLoaded(int sceneBuildIndex)
        {
            while (SceneManager.GetActiveScene().buildIndex != sceneBuildIndex)
            {
                yield return null;
            }
            
            _player = GameObject.FindWithTag(PlayerTag);
        }

        private void OnReceivedMessage(MessageInfo messageInfo)
        {
            // Debug.Log($"[{DateTime.Now.ToString(CultureInfo.InvariantCulture)}]: {messageInfo}");
        }
        
        private void SplitAddress(string address, out IPAddress ipAddress, out int port)
        {
            string[] addressParts = address.Split(':');
            ipAddress = IPAddress.Parse(addressParts[0]);
            port = Convert.ToInt32(addressParts[1]);
        }

        private void UpdateMessage()
        {
            var position = _player.transform.position;
            _serverConnection.SendableMessage = new MessageInfo()
            {
                X = position.x,
                Y = position.y,
                Z = position.z
            };
        }

        public void Dispose()
        {
            _serverConnection?.Dispose();
        }
    }
}